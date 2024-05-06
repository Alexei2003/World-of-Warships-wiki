using static GeneralClasses.Data.FromServer.DB.DBListMessage;

namespace GeneralClasses.Messages.FromServer.DB.DBObjects
{
    public class DBContainerMessage : DBObjectMessage
    {
        public List<DBItemFromContainer> LootList { get; set; } = new();

        public class DBItemFromContainer
        {
            public int? LootChance { get; set; } = null;
            public string? ItemName { get; set; } = null;
            public string? TypeItemName { get; set; } = null;
        }
    }
}
