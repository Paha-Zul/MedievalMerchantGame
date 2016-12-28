using UnityEngine;
using System.Collections;

public class SetItemTarget : LeafTask {
    private readonly string _itemName;
    private readonly int _itemAmount;

    public SetItemTarget(BlackBoard blackboard, string itemName, int itemAmount) : base(blackboard)
    {
        _itemName = itemName;
        _itemAmount = itemAmount;
    }

    public override void Start() {
        base.Start();

        this.bb.targetItem.Name = _itemName;
        this.bb.targetItem.Amount = _itemAmount;

        this.controller.FinishWithSuccess();
    }
}
