using GeneralClasses;
using GeneralClasses.Data;
using GeneralClasses.Data.Request;
using Newtonsoft.Json;
using RabbitMQ;
using ServerConsole;

internal class ProgramServer
{
    private static void Main(string[] args)
    {
        var consumer = new RabbitMQConsumer(GeneralConstant.SERVER_IP,
                                            GeneralConstant.RABBTI_MQ_PORT,
                                            GeneralConstant.RABBIT_MQ_LOGIN,
                                            GeneralConstant.RABBIT_MQ_PASSWORD,
                                            GeneralConstant.RABBTI_MQ_TOPIC_TO_SERVER);

        var publishers = new Dictionary<string, RabbitMQPublisher>();

        var mySQLConnector = new MySQLConnector($"server={GeneralConstant.SERVER_IP};database={GeneralConstant.DB_NAME};uid={GeneralConstant.DB_LOGIN};password={GeneralConstant.DB_PASSWORD};");

        while (true)
        {
            var json = consumer.GetMessage();
            Console.WriteLine(json);

            var basePartOfMessage = JsonConvert.DeserializeObject<Message>(json);

            RabbitMQPublisher publisher;
            switch (basePartOfMessage.Action)
            {
                case GeneralConstant.GeneralServerActions.Start:
                    publishers.TryAdd(basePartOfMessage.TopicFromServer, new RabbitMQPublisher(GeneralConstant.SERVER_IP,
                                                                                               GeneralConstant.RABBTI_MQ_PORT,
                                                                                               GeneralConstant.RABBIT_MQ_LOGIN,
                                                                                               GeneralConstant.RABBIT_MQ_PASSWORD,
                                                                                               basePartOfMessage.TopicFromServer));

                    if (!publishers.TryGetValue(basePartOfMessage.TopicFromServer, out publisher))
                    {
                        break;
                    }

                    publisher.SendMessage(GeneralConstant.SUCCESS);

                    break;

                case GeneralConstant.GeneralServerActions.Get:

                    var message = JsonConvert.DeserializeObject<RequestListMessage>(json);

                    switch (message.ObjectName)
                    {
                        case GeneralConstant.GeneralObjectFromDB.Country:
                            break;

                        case GeneralConstant.GeneralObjectFromDB.Countries:
                            var a = mySQLConnector.GetDataUseDBFunc("get_countries_ship");

                            break;

                        case GeneralConstant.GeneralObjectFromDB.Ship:
                            break;

                        case GeneralConstant.GeneralObjectFromDB.Ships:
                            break;

                        case GeneralConstant.GeneralObjectFromDB.SpecialCommander:
                            break;

                        case GeneralConstant.GeneralObjectFromDB.SpecialCommanders:
                            break;
                    }

                    break;

                case GeneralConstant.GeneralServerActions.Finish:
                    if (!publishers.TryGetValue(basePartOfMessage.TopicFromServer, out publisher))
                    {
                        break;
                    }
                    publisher.DeleteQueue();
                    publisher.CloseConnection();

                    publishers.Remove(basePartOfMessage.TopicFromServer);
                    break;
                case GeneralConstant.GeneralServerActions.None:
                default:
                    break;
            };
        }
        mySQLConnector.Finish();
    }
}