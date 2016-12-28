namespace Assets.Scripts.Util
{
    [System.Serializable]
    public struct ItemCostPair
    {
        public string Name;
        public int Cost;

        public ItemCostPair(string name, int cost)
        {
            this.Name = name;
            this.Cost = cost;
        }

        public ItemCostPair Copy()
        {
            return new ItemCostPair(Name, Cost);
        }
    }
}