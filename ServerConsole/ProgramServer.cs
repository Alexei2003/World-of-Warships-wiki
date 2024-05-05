using GeneralClasses;
using GeneralClasses.Data.FromServer.DB;
using GeneralClasses.Data.ToServer;
using GeneralClasses.Data.ToServer.Request;
using GeneralClasses.Messages.FromServer.DB;
using GeneralClasses.Messages.FromServer.DB.DBObjects;
using MySqlConnector;
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
            Console.WriteLine("GET: " + json);
            Console.WriteLine();

            var basePartOfMessage = JsonConvert.DeserializeObject<MessageToServer>(json);

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

                    var messageGet = JsonConvert.DeserializeObject<RequestMessage>(json);
                    RequestListMessage messageListGet;

                    MySqlDataReader dataReader = null;

                    // Логика списков
                    switch (messageGet.ObjectName)
                    {
                        case GeneralConstant.GeneralObjectFromDB.Countries:
                            dataReader = mySQLConnector.GetAllDataUseDBFunc("get_countries");
                            break;
                        case GeneralConstant.GeneralObjectFromDB.Ships:
                            messageListGet = JsonConvert.DeserializeObject<RequestListMessage>(json);
                            dataReader = mySQLConnector.GetAllDataByCountryIdUseDBFunc("get_ships_by_country_id", messageListGet.CountryId.Value);
                            break;
                        case GeneralConstant.GeneralObjectFromDB.Commanders:
                            messageListGet = JsonConvert.DeserializeObject<RequestListMessage>(json);
                            dataReader = mySQLConnector.GetAllDataByCountryIdUseDBFunc("get_commanders_by_country_id", messageListGet.CountryId.Value);
                            break;
                        case GeneralConstant.GeneralObjectFromDB.Maps:
                            dataReader = mySQLConnector.GetAllDataUseDBFunc("get_maps");
                            break;
                        case GeneralConstant.GeneralObjectFromDB.PlayerLevels:
                            dataReader = mySQLConnector.GetAllDataUseDBFunc("get_player_levels");
                            break;
                        case GeneralConstant.GeneralObjectFromDB.Achievements:
                            dataReader = mySQLConnector.GetAllDataUseDBFunc("get_achievements");
                            break;
                        case GeneralConstant.GeneralObjectFromDB.Containers:
                            dataReader = mySQLConnector.GetAllDataUseDBFunc("get_containers");
                            break;

                        default:
                            break;
                    }

                    if (dataReader != null)
                    {
                        var messageListSend = new DBListMessage();

                        while (dataReader.Read())
                        {
                            messageListSend.List.Add(new DBObjectOfList()
                            {
                                Id = dataReader.GetInt32("id"),
                                Name = dataReader.GetString("name"),
                                PicturePath = dataReader.GetString("picturepath"),
                            });
                        }

                        dataReader.Close();


                        if (!publishers.TryGetValue(basePartOfMessage.TopicFromServer, out publisher))
                        {
                            break;
                        }

                        json = messageListSend.ToJson();

                        Console.WriteLine("Send: " + json);
                        Console.WriteLine();

                        publisher.SendMessage(json);

                        continue;
                    }
                    else
                    {
                        // Логика объектов
                        var messageObjectGet = JsonConvert.DeserializeObject<RequestObjectMessage>(json);

                        switch (messageGet.ObjectName)
                        {
                            case GeneralConstant.GeneralObjectFromDB.Country:

                                dataReader = mySQLConnector.GetDataByIdUseDBFunc("get_country", messageObjectGet.ObjectId.Value);

                                var messageCountrySend = new DBCountryMessage();

                                while (dataReader.Read())
                                {
                                    messageCountrySend.Name = dataReader.GetString("name");
                                    messageCountrySend.Description = dataReader.GetString("description");
                                    messageCountrySend.PicturePath = dataReader.GetString("picturepath");
                                }

                                dataReader.Close();

                                if (!publishers.TryGetValue(basePartOfMessage.TopicFromServer, out publisher))
                                {
                                    break;
                                }

                                json = messageCountrySend.ToJson();

                                Console.WriteLine("Send: " + json);
                                Console.WriteLine();

                                publisher.SendMessage(json);
                                break;

                            case GeneralConstant.GeneralObjectFromDB.Ship:
                                Console.WriteLine("Send: " + json);
                                Console.WriteLine();
                                break;

                            case GeneralConstant.GeneralObjectFromDB.Commander:
                                Console.WriteLine("Send: " + json);
                                Console.WriteLine();
                                break;

                            case GeneralConstant.GeneralObjectFromDB.Map:

                                dataReader = mySQLConnector.GetDataByIdUseDBFunc("get_map", messageObjectGet.ObjectId.Value);

                                var messageMaptSend = new DBMapMessage();

                                while (dataReader.Read())
                                {
                                    messageMaptSend.Name = dataReader.GetString("name");
                                    messageMaptSend.Description = dataReader.GetString("description");
                                    messageMaptSend.PicturePath = dataReader.GetString("picturepath");
                                    messageMaptSend.Battletiers = dataReader.GetString("battletiers");
                                    messageMaptSend.Size = dataReader.GetString("size");
                                    messageMaptSend.Replyfilename = dataReader.GetString("replyfilename");
                                }

                                dataReader.Close();

                                if (!publishers.TryGetValue(basePartOfMessage.TopicFromServer, out publisher))
                                {
                                    break;
                                }

                                json = messageMaptSend.ToJson();

                                Console.WriteLine("Send: " + json);
                                Console.WriteLine();

                                publisher.SendMessage(json);
                                break;

                            case GeneralConstant.GeneralObjectFromDB.PlayerLevel:
                                Console.WriteLine("Send: " + json);
                                Console.WriteLine();
                                break;

                            case GeneralConstant.GeneralObjectFromDB.Achievement:
                                Console.WriteLine("Send: " + json);
                                Console.WriteLine();
                                break;

                            case GeneralConstant.GeneralObjectFromDB.Container:
                                Console.WriteLine("Send: " + json);
                                Console.WriteLine();
                                break;
                        }
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