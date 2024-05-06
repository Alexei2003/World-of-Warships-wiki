namespace GeneralClasses.Messages.FromServer.DB.DBObjects
{
    public class DBPlayerLevelMessage : DBObjectMessage
    {
        public int? BattleNeed { get; set; } = null;
        public int? BattleTotal { get; set; } = null;
    }
}
