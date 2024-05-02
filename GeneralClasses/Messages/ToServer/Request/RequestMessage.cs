namespace GeneralClasses.Data.ToServer.Request
{
    public class RequestMessage : MessageToServer
    {
        public GeneralConstant.GeneralObjectFromDB ObjectName { get; set; } = GeneralConstant.GeneralObjectFromDB.None;
    }
}
