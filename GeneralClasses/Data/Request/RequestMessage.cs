namespace GeneralClasses.Data.Request
{
    public class RequestMessage : Message
    {
        public GeneralConstant.GeneralObjectFromDB ObjectName { get; set; } = GeneralConstant.GeneralObjectFromDB.None;
    }
}
