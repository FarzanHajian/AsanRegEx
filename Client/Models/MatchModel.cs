namespace AsanRegEx.Client.Models
{
    public class MatchModel
    {
        public string Name { get; set; } = "";
        public string Value { get; set; } = "";
        public int Index { get; set; }
        public int Length { get; set; }
        public List<CaptureModel> Captues { get; private set; } = new List<CaptureModel>();
        public List<GroupModel> Groups { get; private set; } = new List<GroupModel>();

        public MatchModel(string name, string value, int index, int legnth)
        {
            Name = name;
            Value = value;
            Index = index;
            Length = legnth;
        }
    }

    public class CaptureModel
    {
        public string Value { get; set; } = "";
        public int Index { get; set; }
        public int Length { get; set; }

        public CaptureModel(string value, int index, int legnth)
        {
            Value = value;
            Index = index;
            Length = legnth;
        }
    }

    public class GroupModel
    {
        public string Name { get; set; } = "";
        public string Value { get; set; } = "";
        public int Index { get; set; }
        public int Length { get; set; }

        public GroupModel(string name, string value, int index, int length)
        {
            Name = name;
            Value = value;
            Index = index;
            Length = length;
        }
    }
}