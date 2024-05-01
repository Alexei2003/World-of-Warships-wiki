namespace GeneralClasses
{
    public static class GeneralConstant
    {
        public const string RABBIT_MQ_LOGIN = "Web";
        public const string RABBIT_MQ_PASSWORD = "Web";
        public const int RABBTI_MQ_PORT = 5672;
        public const string RABBTI_MQ_TOPIC_TO_SERVER = "ToServer";
        public const string RABBTI_MQ_TOPIC_TO_CLIENT = "ToClient";

        public const string SERVER_IP = "192.168.148.22";

        public const string DB_NAME = "mydb";
        public const string DB_LOGIN = "Admin";
        public const string DB_PASSWORD = "1a5B3f97";

        public const string SUCCESS = "SUCCESS";

        public enum GeneralServerActions
        {
            None, Start, Finish,
        }

        public enum GeneralObjectFromDB
        {
            None, Country, Countries, Ship, Ships, SpecialCommander, SpecialCommanders
        }
    }
}
