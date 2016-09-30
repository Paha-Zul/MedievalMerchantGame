using UnityEngine;
using System.Collections;

public static class Tasks {

	public static Task HaulTask(BlackBoard bb) {
        Selector task = new Selector(bb, "Haul or Idle");

        Sequence mainSeq = new Sequence(bb, "Haul Sequence");

        Task setItemTarget = new SetItemTarget(bb, "Wood Log", 10);
        Task getClosestStockpile = new GetClosestStockpileWithItem(bb);
        Task setTargetInventoryToStockpile = new SetTargetInventoryFromTargetBuilding(bb);
        Task getWalkSpotToStockpile = new GetWalkSpotOfTargetBuilding(bb);
        Task moveToStockpile = new MoveTo(bb);
        Task takeItem = new TakeItemFromInventory(bb);
        Task setTarget = new SetMyBuildingAsTarget(bb);
        Task getWalkSpotOfWorkshop = new GetWalkSpotOfTargetBuilding(bb);
        Task moveToMyBuilding = new MoveTo(bb);
        Task setTargetInventoryToMyBuilding = new SetTargetInventoryFromTargetBuilding(bb);
        Task giveItem = new GiveItemToInventory(bb);

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

        Task idle = new IdleTask(bb);

        task.controller.AddTask(mainSeq);
        task.controller.AddTask(idle);

        return task;
    }

    public static Task ProduceItemAtWorkshop(BlackBoard bb, bool getInputIfNotEnough) {
        Selector task = new Selector(bb, "Produce or Idle");

        Sequence mainSeq = new Sequence(bb);

        Selector sel = new Selector(bb, "Check or Haul");
        Task checkInventoryForInput = new CheckWorkshopHasInputItem(bb);
        Task haulInputToWorkship = HaulTask(bb);

        sel.controller.AddTask(checkInventoryForInput);
        if(getInputIfNotEnough) sel.controller.AddTask(haulInputToWorkship);

        Task setTargetAsMyBuilding = new SetMyBuildingAsTarget(bb);
        Task moveToBuilding = new MoveToBuilding(bb);
        Task produceOutput = new ProduceItemAtWorkshop(bb);

        mainSeq.controller.AddTask(sel);
        mainSeq.controller.AddTask(setTargetAsMyBuilding);
        mainSeq.controller.AddTask(moveToBuilding);
        mainSeq.controller.AddTask(produceOutput);

        Task idle = new IdleTask(bb);

        task.controller.AddTask(mainSeq);
        task.controller.AddTask(idle);

        return task;
    }
}
