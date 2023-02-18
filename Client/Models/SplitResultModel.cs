namespace AsanRegEx.Client.Models;

public class SplitResultModel
{
    public string Result { get; set; } = "";
    public int ItemCount { get; set; }

    public SplitResultModel(string result, int itemCount)
    {
        Result = result;
        ItemCount = itemCount;
    }
}