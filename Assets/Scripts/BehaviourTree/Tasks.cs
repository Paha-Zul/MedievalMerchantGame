using Assets.Scripts.BehaviourTree.Decorator;
using Assets.Scripts.BehaviourTree.Leaf;
using Assets.Scripts.Util;
using BehaviourTree.Leaf;

namespace BehaviourTree
{
    public static class Tasks {

        public static Task HaulTask(BlackBoard bb, string itemName, bool idleOnFail = false) {
            var task = new Selector(bb, "Haul or Idle");

            var mainSeq = new Sequence(bb, "Haul Sequence");

            var setItemTarget = new SetItemTarget(bb, itemName, 10); //TODO Don't hardcode this
            var getClosestStockpile = new GetClosestStockpileWithItem(bb);
            var setTargetInventoryToStockpile = new SetTargetInventoryFromTargetBuilding(bb);
            var getWalkSpotToStockpile = new GetSpotOfBuilding(bb, SpotType.Delivery);
            var moveToStockpile = new MoveToNavmesh(bb);
            var takeItem = new TakeItemFromInventory(bb);
            var setTarget = new SetMyBuildingAsTarget(bb);
            var getWalkSpotOfWorkshop = new GetSpotOfBuilding(bb, SpotType.Delivery);
            var moveToMyBuilding = new MoveToNavmesh(bb);
            var setTargetInventoryToMyBuilding = new SetTargetInventoryFromTargetBuilding(bb);
            var giveItem = new GiveItemToInventory(bb);

            mainSeq.controller.AddTask(setItemTarget);
            mainSeq.controller.AddTask(getClosestStockpile);
            mainSeq.controller.AddTask(setTargetInventoryToStockpile);
            mainSeq.controller.AddTask(getWalkSpotToStockpile);
            mainSeq.controller.AddTask(moveToStockpile);
            mainSeq.controller.AddTask(takeItem);
            mainSeq.controller.AddTask(setTarget);
            mainSeq.controller.AddTask(getWalkSpotOfWorkshop);
            mainSeq.controller.AddTask(moveToMyBuilding);
            mainSeq.controller.AddTask(setTargetInventoryToMyBuilding);
            mainSeq.controller.AddTask(giveItem);

            var idle = new IdleTask(bb);

            task.controller.AddTask(mainSeq);
            if(idleOnFail) task.controller.AddTask(idle);

            return task;
        }

        public static Task LeaveBuilding(BlackBoard bb)
        {
            var mainSequence = new Sequence(bb, "Leave Building");

            var setTargetBuilding = new SetMyBuildingAsTarget(bb);
            var checkInsideBuilding = new CheckInsideBuilding(bb);
            var getPathToOutside = new GetPathInBuilding(bb, PathType.Entrance, PathType.Curr);
            var moveTo = new MoveTo(bb);

            mainSequence.controller.AddTask(setTargetBuilding);
            mainSequence.controller.AddTask(checkInsideBuilding);
            mainSequence.controller.AddTask(getPathToOutside);
            mainSequence.controller.AddTask(moveTo);

            return mainSequence;
        }

        public static Task WorkAtWorkshop(BlackBoard bb, bool handleHauling = false, bool handleSelling = false)
        {
            var mainSequence = new Sequence(bb, "Work At Workshop");

            var leaveBuilding = LeaveBuilding(bb);
            var haulSequence = HaulTask(bb, "Wood Log", false);
            var produceSequence = ProduceItemAtWorkshop(bb, !handleHauling && !handleSelling);
            var sellSequence = SellItemFromStore(bb);

            var leaveAndHaul = new Sequence(bb);

            var alwaysSucceedHaul = new AlwaysSucceed(bb, leaveAndHaul);
            var alwaysSucceedProduce = new AlwaysSucceed(bb, produceSequence);
            var alwaysSucceedSell = new AlwaysSucceed(bb, sellSequence);

            if (handleHauling)
            {
                //Set up the leave building and haul sequence
                leaveAndHaul.controller.AddTask(new NegateDecorator(bb, new CheckWorkshopHasInputItem(bb))); //We only need to haul if we need the input item.
                leaveAndHaul.controller.AddTask(new AlwaysSucceed(bb, leaveBuilding)); //This will leave the building (if needed)
                leaveAndHaul.controller.AddTask(new AlwaysSucceed(bb, haulSequence)); //This will haul the item.

                //Add it to the main sequence
                mainSequence.controller.AddTask(alwaysSucceedHaul);
            }

            mainSequence.controller.AddTask(alwaysSucceedProduce);

            if(handleSelling)
                mainSequence.controller.AddTask(alwaysSucceedSell);

            return mainSequence;
        }

        public static Task ProduceItemAtWorkshop(BlackBoard bb, bool repeatProduce = false) {
            var task = new Selector(bb, "Produce or Idle");

            var mainSeq = new Sequence(bb);
            var optionalMoveToBuildingSequence = new Sequence(bb, "Optional Move To Building Seq");
            var optionalSequenceAlwaysSucceed = new AlwaysSucceed(bb, optionalMoveToBuildingSequence);

            var checkInventoryForInput = new CheckWorkshopHasInputItem(bb);
            var setTargetAsMyBuilding = new SetMyBuildingAsTarget(bb);

            //Optional such
            var checkInsideBuilding = new CheckInsideBuilding(bb);
            var checkNotInsideBuilding = new NegateDecorator(bb, checkInsideBuilding);
            var getEntranceOfBuilding = new GetSpotOfBuilding(bb, SpotType.Entrance);
            var moveToBuilding = new MoveToNavmesh(bb);
            var setInsideBuilding = new SetFootUnitCurrSpotFromBuilding(bb, SpotType.Entrance);

            //Normal
            var getWorkSpot = new GetPathInBuilding(bb, PathType.Work, PathType.Curr);
            var moveToWork = new MoveTo(bb);
            var setAtWork = new SetAtWorkStation(bb);

            //If we are handling selling items as well, only produce an item once. If we are not handling it, produce items indefinitely
            var produceOutput = !repeatProduce
                ? (Task) new ProduceItemAtWorkshop(bb) : new RepeatUntilFail(bb, new ProduceItemAtWorkshop(bb));

            mainSeq.controller.AddTask(checkInventoryForInput);
            mainSeq.controller.AddTask(setTargetAsMyBuilding);

            //This falls under the optionalSequence decorator
            optionalMoveToBuildingSequence.controller.AddTask(checkNotInsideBuilding);
            optionalMoveToBuildingSequence.controller.AddTask(getEntranceOfBuilding);
            optionalMoveToBuildingSequence.controller.AddTask(moveToBuilding);
            optionalMoveToBuildingSequence.controller.AddTask(setInsideBuilding);

            mainSeq.controller.AddTask(optionalSequenceAlwaysSucceed);
            mainSeq.controller.AddTask(getWorkSpot);
            mainSeq.controller.AddTask(moveToWork);
            mainSeq.controller.AddTask(setAtWork);
            mainSeq.controller.AddTask(produceOutput);

            var idle = new IdleTask(bb);

            task.controller.AddTask(mainSeq);
            task.controller.AddTask(idle);

            return task;
        }

        public static Task SellItemFromStore(BlackBoard bb)
        {
            var mainTask = new Sequence(bb, "Sell Item From Store");

            var moveToSelector = new Selector(bb, "Move To Building Or Inside Building");
            var moveInsideBuilding = new Sequence(bb, "Move Inside Building");
            var moveToAndInsideBuilding = new Sequence(bb, "Move To and Inside Building");

            /*
                Sequence --
                    Clear targets
                    Set the target building
                    Get the unit target from the building queue
                    Selector --
                        Sequence --
                            Check if we currently are inside the building
                            Get path to selling spot
                            Move to selling spot
                        Sequence --
                            Get entrance of building
                            Move to entrance
                            Get path to selling spot
                            Move inside building to selling spot
                    Some delay
                    Sell item
                    Some delay
            */

            var clearAllTargets = new ClearTarget(bb, ClearTarget.TargetType.All);
            var setWorkshopAsTarget = new SetMyBuildingAsTarget(bb);
            var checkBuildingHasItem = new CheckTargetBuildingHasItem(bb, "Wood Plank"); //TODO This is hardcoded. DONT FORGET THIS
            var getUnitFromQueue = new GetUnitTargetFromQueue(bb);

            var getEntrance = new GetSpotOfBuilding(bb, SpotType.Entrance);
            var moveToEntrance = new MoveToNavmesh(bb);

            var checkInsideBuilding = new CheckInsideBuilding(bb);
            var getPathToSell = new GetPathInBuilding(bb, PathType.Sell, PathType.Curr);
            var moveToSpot = new MoveToNavmesh(bb);
            var moveToSellSpot = new MoveTo(bb);

            var firstDelay = new IdleTask(bb);
            var sellItem = new SellItem(bb);
            var secondDelay = new IdleTask(bb, 0f, 0f);

            mainTask.controller.AddTask(clearAllTargets);
            mainTask.controller.AddTask(setWorkshopAsTarget);
            mainTask.controller.AddTask(checkBuildingHasItem);
            mainTask.controller.AddTask(getUnitFromQueue);

            moveToSelector.controller.AddTask(moveInsideBuilding);
            moveInsideBuilding.controller.AddTask(checkInsideBuilding);
            moveInsideBuilding.controller.AddTask(getPathToSell);
            moveInsideBuilding.controller.AddTask(moveToSellSpot);

            moveToSelector.controller.AddTask(moveToAndInsideBuilding);
            moveToAndInsideBuilding.controller.AddTask(getEntrance);
            moveToAndInsideBuilding.controller.AddTask(moveToEntrance);
            moveToAndInsideBuilding.controller.AddTask(getPathToSell);
            moveToAndInsideBuilding.controller.AddTask(moveToSellSpot);

            mainTask.controller.AddTask(moveToSelector);

            mainTask.controller.AddTask(firstDelay);
            mainTask.controller.AddTask(sellItem);
            mainTask.controller.AddTask(secondDelay);

            return mainTask;
        }

        public static Task BuyItem(BlackBoard bb, string itemName, int itemAmount) {
            var task = new Selector(bb, "Buy or Idle");

            var mainSeq = new Sequence(bb);

            var setTargetItem = new SetItemTarget(bb, itemName, itemAmount);
            var getClosestSelling = new GetClosestSellingItem(bb);
            var getWalkSpotOfWorkshop = new GetSpotOfBuilding(bb, SpotType.Walk);
            var moveToBuilding = new MoveToNavmesh(bb);
            var enterQueue = new EnterTargetBuildingQueue(bb);
            var waitToBuy = new WaitTimeOrCondition(bb, 20f, () => bb.QueueFlag);
            var getExit = new GetWorldExit(bb);
            var moveToExit = new MoveToNavmesh(bb);
            var destroyMyself = new DestroySelf(bb);

            mainSeq.controller.AddTask(setTargetItem);
            mainSeq.controller.AddTask(getClosestSelling);
            mainSeq.controller.AddTask(getWalkSpotOfWorkshop);
            mainSeq.controller.AddTask(moveToBuilding);
            mainSeq.controller.AddTask(enterQueue);
            mainSeq.controller.AddTask(waitToBuy);
            mainSeq.controller.AddTask(getExit);
            mainSeq.controller.AddTask(moveToExit);
            mainSeq.controller.AddTask(destroyMyself);

            var idle = new IdleTask(bb);

            task.controller.AddTask(mainSeq);
            task.controller.AddTask(idle);

            return task;
        }
    }
}
