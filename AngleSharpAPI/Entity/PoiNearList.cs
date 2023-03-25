namespace AngleSharpAPI.Entity
{
    public class PoiNearList
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public Pois[] pois { get; set; }
    }

    public class Pois
    {
        public int aid { get; set; }
        public string name { get; set; }
        public string pinyin { get; set; }
        public string region { get; set; }
        public string town { get; set; }
        public int distance { get; set; }
        public string picture { get; set; }
        public string picdescribe { get; set; }
        public float px { get; set; }
        public float py { get; set; }
    }

}
