

[System.Serializable]
public class ItemProduction{
    public string inputItemName = "", outputItemName = "";
    public int inputItemAmount = 1, outputItemAmount = 1;
    public float baseProdTime = 1f;

    public ItemProduction(string inputItemName, string outputItemName, int inputItemAmount, int outputItemAmount, float baseProdTime) {
        this.inputItemName = inputItemName;
        this.outputItemName = outputItemName;
        this.inputItemAmount = inputItemAmount;
        this.outputItemAmount = outputItemAmount;
        this.baseProdTime = baseProdTime;
    }
}
