using Assets.Scripts.BehaviourTree.Leaf;
using Assets.Scripts.Util;
using BehaviourTree.Leaf;

namespace BehaviourTree
{
    public static class Tasks {

        public static Task HaulTask(BlackBoard bb) {
            var task = new Selector(bb, "Haul or Idle");

            var mainSeq = new Sequence(bb, "Haul Sequence");

            var setItemTarget = new SetItemTarget(bb, "Wood Log", 10);
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
            task.controller.AddTask(idle);

            return task;
        }

        public static Task ProduceItemAtWorkshop(BlackBoard bb, bool getInputIfNotEnough = false, bool handleSellingItems = false) {
            var task = new Selector(bb, "Produce or Idle");

            var mainSeq = new Sequence(bb);

            var checkOrHaulSelector = new Selector(bb, "Check or Haul");
            var checkInventoryForInput = new CheckWorkshopHasInputItem(bb);
            var haulInputToWorkship = HaulTask(bb);

            checkOrHaulSelector.controller.AddTask(checkInventoryForInput);
            if(getInputIfNotEnough) checkOrHaulSelector.controller.AddTask(haulInputToWorkship);

            var setTargetAsMyBuilding = new SetMyBuildingAsTarget(bb);
            var getEntranceOfBuilding = new GetSpotOfBuilding(bb, SpotType.Entrance);
            var moveToBuilding = new MoveToNavmesh(bb);
            var getWorkSpot = new GetPathInBuilding(bb, PathType.Work, PathType.Entrance);
            var moveToWork = new MoveTo(bb);
            var setAtWork = new SetAtWorkStation(bb);

            //If we are handling selling items as well, only produce an item once. If we are not handling it, produce items indefinitely
            var produceOutput = handleSellingItems
                ? (Task) new ProduceItemAtWorkshop(bb) : new RepeatUntilFail(bb, new ProduceItemAtWorkshop(bb));

            mainSeq.controller.AddTask(checkOrHaulSelector);
            mainSeq.controller.AddTask(setTargetAsMyBuilding);
            mainSeq.controller.AddTask(getEntranceOfBuilding);
            mainSeq.controller.AddTask(moveToBuilding);
            mainSeq.controller.AddTask(getWorkSpot);
            mainSeq.controller.AddTask(moveToWork);
            mainSeq.controller.AddTask(setAtWork);
            mainSeq.controller.AddTask(produceOutput);

            if (handleSellingItems)
            {
                var getPathToSellingSpot = new GetPathInBuilding(bb, PathType.Sell, PathType.Curr);
                var moveToSell = new MoveTo(bb);

                mainSeq.controller.AddTask(getPathToSellingSpot);
                mainSeq.controller.AddTask(moveToSell);
                //Sell item
            }

            var idle = new IdleTask(bb);

            task.controller.AddTask(mainSeq);
            task.controller.AddTask(idle);

            return task;
        }

        public static Task SellItemFromStore(BlackBoard bb)
        {
            var mainTask = new Sequence(bb, "Sell Item From Store");
            var MoveInsideBuilding = new Sequence(bb, "Move Inside Building");
            var MoveToAndInsideBuilding = new Sequence(bb, "Move To and Inside Building");

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
            var getUnitFromQueue = new GetUnitTargetFromQueue(bb);

            var getEntrance = new GetSpotOfBuilding(bb, SpotType.Entrance);
            var moveToEntrance = new MoveToNavmesh(bb);

            var checkInsideBuilding = new CheckInsideBuilding(bb);
            var getPathToSell = new GetPathInBuilding(bb, PathType.Sell, PathType.Curr);
            var moveToSpot = new MoveToNavmesh(bb);
            var moveToSellSpot = new MoveTo(bb);

            var firstDelay = new IdleTask(bb);
            var sellItem = new SellItem(bb);
            var secondDelay = new IdleTask(bb);

            mainTask.controller.AddTask(clearAllTargets);
            mainTask.controller.AddTask(setWorkshopAsTarget);
            mainTask.controller.AddTask(getUnitFromQueue);

            mainTask.controller.AddTask(MoveInsideBuilding);
            MoveInsideBuilding.controller.AddTask(checkInsideBuilding);
            MoveInsideBuilding.controller.AddTask(getPathToSell);
            MoveInsideBuilding.controller.AddTask(moveToSellSpot);

            mainTask.controller.AddTask(MoveToAndInsideBuilding);
            MoveInsideBuilding.controller.AddTask(getEntrance);
            MoveInsideBuilding.controller.AddTask(moveToEntrance);
            MoveInsideBuilding.controller.AddTask(getPathToSell);
            MoveInsideBuilding.controller.AddTask(moveToSellSpot);

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
            var idleBeforeBuy = new IdleTask(bb);
            var buyItem = new BuyItem(bb);
            var idleAfterBuy = new IdleTask(bb);
            var getExit = new GetWorldExit(bb);
            var moveToExit = new MoveToNavmesh(bb);
            var destroyMyself = new DestroySelf(bb);

            mainSeq.controller.AddTask(setTargetItem);
            mainSeq.controller.AddTask(getClosestSelling);
            mainSeq.controller.AddTask(getWalkSpotOfWorkshop);
            mainSeq.controller.AddTask(moveToBuilding);
            mainSeq.controller.AddTask(idleBeforeBuy);
            mainSeq.controller.AddTask(buyItem);
            mainSeq.controller.AddTask(idleAfterBuy);
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
