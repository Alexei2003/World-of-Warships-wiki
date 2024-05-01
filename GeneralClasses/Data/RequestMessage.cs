namespace GeneralClasses.Data
{
    public class RequestMessage : Message
    {
        public GeneralConstant.GeneralObjectFromDB TableName { get; set; } = GeneralConstant.GeneralObjectFromDB.None;

        public int? ObjectId { get; set; } = null;
    }
}
