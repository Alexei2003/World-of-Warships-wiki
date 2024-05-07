namespace GeneralClasses.Messages.FromServer.DB.DBObjects
{
    public class DBShipMessage : DBObjectMessage
    {
        public int? Level { get; set; } = null;
        public int? Survivability { get; set; } = null;
        public int? Aircraft { get; set; } = null;
        public int? Artillery { get; set; } = null;
        public int? Torpedoes { get; set; } = null;
        public int? Airdefense { get; set; } = null;
        public int? Maneuverability { get; set; } = null;
        public int? Concealment { get; set; } = null;
        public int? PriceMoney { get; set; } = null;
        public int? PriceExp { get; set; } = null;

        public string? ShipClassName { get; set; } = null;
        public string? ShipClassPicturePath { get; set; } = null;

        public List<DBModules> ModulesList { get; set; } = new();

        public class DBModules
        {
            public string? Name { get; set; } = null;
            public string? Description { get; set; } = null;
            public string? PicturePath { get; set; } = null;
            public int? PriceMoney { get; set; } = null;
            public int? PriceExp { get; set; } = null;
        }
    }
}
