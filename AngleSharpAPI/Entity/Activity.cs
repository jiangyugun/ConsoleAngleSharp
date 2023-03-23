namespace AngleSharpAPI.Entity
{
    /// <summary>
    /// 活動-觀光資訊
    /// </summary>
    public class Activity
    {
        public XML_Head XML_Head { get; set; }
    }

    public class XML_Head
    {
        public string Listname { get; set; }
        public string Language { get; set; }
        public string Orgname { get; set; }
        public DateTime Updatetime { get; set; }
        public Infos Infos { get; set; }
    }

    public class Infos
    {
        public Info[] Info { get; set; }
    }

    public class Info
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Participation { get; set; }
        public string Location { get; set; }
        public string Add { get; set; }
        public string Region { get; set; }
        public string Town { get; set; }
        public string Tel { get; set; }
        public string Org { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Cycle { get; set; }
        public string Noncycle { get; set; }
        public string Website { get; set; }
        public string Picture1 { get; set; }
        public string Picdescribe1 { get; set; }
        public string Picture2 { get; set; }
        public string Picdescribe2 { get; set; }
        public string Picture3 { get; set; }
        public string Picdescribe3 { get; set; }
        public float Px { get; set; }
        public float Py { get; set; }
        public string Class1 { get; set; }
        public string Class2 { get; set; }
        public string Map { get; set; }
        public string Travellinginfo { get; set; }
        public string Parkinginfo { get; set; }
        public string Charge { get; set; }
        public string Remarks { get; set; }
    }

}
