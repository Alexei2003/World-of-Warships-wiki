﻿using GeneralClasses;
using GeneralClasses.Data.FromServer.DB;
using GeneralClasses.Data.ToServer;
using GeneralClasses.Data.ToServer.Request;
using GeneralClasses.Messages.FromServer.DB;
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

                    var messageGet = JsonConvert.DeserializeObject<RequestListMessage>(json);
                    var messageListSend = new DBListMessage();

                    MySqlDataReader dataReader;

                    switch (messageGet.ObjectName)
                    {
                        case GeneralConstant.GeneralObjectFromDB.Country:
                            Console.WriteLine("Send: " + json);
                            Console.WriteLine();
                            break;

                        case GeneralConstant.GeneralObjectFromDB.Countries:
                            dataReader = mySQLConnector.GetDataUseDBFunc("get_countries");

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

                            break;

                        case GeneralConstant.GeneralObjectFromDB.Ship:
                            Console.WriteLine("Send: " + json);
                            Console.WriteLine();
                            break;

                        case GeneralConstant.GeneralObjectFromDB.Ships:
                            Console.WriteLine("Send: " + json);
                            Console.WriteLine();
                            break;

                        case GeneralConstant.GeneralObjectFromDB.SpecialCommander:
                            Console.WriteLine("Send: " + json);
                            Console.WriteLine();
                            break;

                        case GeneralConstant.GeneralObjectFromDB.SpecialCommanders:
                            Console.WriteLine("Send: " + json);
                            Console.WriteLine();
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