namespace Assets.Scripts.BehaviourTree.Leaf
{
    public class ClearTarget : LeafTask
    {
        public enum TargetType
        {
            Building, FootUnit, Inventory, Position, Item, All
        }

        public ClearTarget(BlackBoard blackboard, TargetType type) : base(blackboard)
        {
            switch (type)
            {
                case TargetType.Building:
                    this.bb.targetBuilding = null;
                    break;
                case TargetType.FootUnit:
                    this.bb.targetFootUnit = null;
                    break;
                case TargetType.Inventory:
                    this.bb.targetInventory = null;
                    break;
                case TargetType.Position:
                    this.bb.targetPosition.Set(0, 0, 0);
                    break;
                case TargetType.Item:
                    this.bb.targetItem.Set("", 0);
                    break;
                case TargetType.All:
                    this.bb.targetItem.Set("", 0);
                    this.bb.targetBuilding = null;
                    this.bb.targetFootUnit = null;
                    this.bb.targetInventory = null;
                    this.bb.targetPosition.Set(0, 0, 0);
                    break;
            }

            this.controller.FinishWithSuccess();
        }
    }
}