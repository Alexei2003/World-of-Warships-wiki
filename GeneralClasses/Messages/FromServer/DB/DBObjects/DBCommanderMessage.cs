using static GeneralClasses.Messages.FromServer.DB.DBObjects.DBContainerMessage;

namespace GeneralClasses.Messages.FromServer.DB.DBObjects
{
    public class DBCommanderMessage : DBObjectMessage
    {
        public string? Origins { get; set; } = null;

        public List<DBTalent> TalentList { get; set; } = new();

        public class DBTalent : DBObjectMessage
        {
            public string? Name { get; set; } = null;
            public string? Description { get; set; } = null;
            public string? PicturePath { get; set; } = null;
        }
    }
}
