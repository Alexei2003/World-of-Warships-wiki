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
using System.Data;

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
                            messageListSend.ItemList.Add(new DBListMessage.DBObjectFromList()
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
                                dataReader = mySQLConnector.GetDataByIdUseDBFunc("get_ship", messageObjectGet.ObjectId.Value);

                                var messageShipSend = new DBShipMessage();

                                if (dataReader.Read())
                                {
                                    messageShipSend.Name = dataReader.GetString("ship_name");
                                    messageShipSend.Description = dataReader.GetString("ship_description");
                                    messageShipSend.PicturePath = dataReader.GetString("ship_picturepath");

                                    messageShipSend.Level = dataReader.IsDBNull(dataReader.GetOrdinal("ship_level")) ? 0 : dataReader.GetInt32("ship_level");
                                    messageShipSend.Survivability = dataReader.IsDBNull(dataReader.GetOrdinal("ship_survivability")) ? 0 : dataReader.GetInt32("ship_survivability");
                                    messageShipSend.Aircraft = dataReader.IsDBNull(dataReader.GetOrdinal("ship_aircraft")) ? 0 : dataReader.GetInt32("ship_aircraft");
                                    messageShipSend.Artillery = dataReader.IsDBNull(dataReader.GetOrdinal("ship_artillery")) ? 0 : dataReader.GetInt32("ship_artillery");
                                    messageShipSend.Torpedoes = dataReader.IsDBNull(dataReader.GetOrdinal("ship_torpedoes")) ? 0 : dataReader.GetInt32("ship_torpedoes");
                                    messageShipSend.Airdefense = dataReader.IsDBNull(dataReader.GetOrdinal("ship_airdefense")) ? 0 : dataReader.GetInt32("ship_airdefense");
                                    messageShipSend.Maneuverability = dataReader.IsDBNull(dataReader.GetOrdinal("ship_maneuverability")) ? 0 : dataReader.GetInt32("ship_maneuverability");
                                    messageShipSend.Concealment = dataReader.IsDBNull(dataReader.GetOrdinal("ship_concealment")) ? 0 : dataReader.GetInt32("ship_concealment");
                                    messageShipSend.PriceMoney = dataReader.IsDBNull(dataReader.GetOrdinal("ship_pricemoney")) ? 0 : dataReader.GetInt32("ship_pricemoney");
                                    messageShipSend.PriceExp = dataReader.IsDBNull(dataReader.GetOrdinal("ship_priceexp")) ? 0 : dataReader.GetInt32("ship_priceexp");


                                    messageShipSend.ShipClassName = dataReader.GetString("ship_class_name");
                                    messageShipSend.ShipClassPicturePath = dataReader.GetString("ship_class_picturepath");

                                    var modulesFromShip = new DBShipMessage.DBModules();
                                    modulesFromShip.Name = dataReader.IsDBNull(dataReader.GetOrdinal("modules_name")) ? null : dataReader.GetString("modules_name");
                                    modulesFromShip.Description = dataReader.IsDBNull(dataReader.GetOrdinal("modules_description")) ? null : dataReader.GetString("modules_description");
                                    modulesFromShip.PicturePath = dataReader.IsDBNull(dataReader.GetOrdinal("modules_picturepath")) ? null : dataReader.GetString("modules_picturepath");
                                    modulesFromShip.PriceMoney = dataReader.IsDBNull(dataReader.GetOrdinal("modules_pricemoney")) ? 0 : dataReader.GetInt32("modules_pricemoney");
                                    modulesFromShip.PriceExp = dataReader.IsDBNull(dataReader.GetOrdinal("modules_priceexp")) ? 0 : dataReader.GetInt32("modules_priceexp");

                                    if(modulesFromShip.Name != null)
                                    {
                                        messageShipSend.ModulesList.Add(modulesFromShip);
                                    }

                                }

                                while (dataReader.Read())
                                {
                                    var modulesFromShip = new DBShipMessage.DBModules();
                                    modulesFromShip.Name = dataReader.IsDBNull(dataReader.GetOrdinal("modules_name")) ? null : dataReader.GetString("modules_name");
                                    modulesFromShip.Description = dataReader.IsDBNull(dataReader.GetOrdinal("modules_description")) ? null : dataReader.GetString("modules_description");
                                    modulesFromShip.PicturePath = dataReader.IsDBNull(dataReader.GetOrdinal("modules_picturepath")) ? null : dataReader.GetString("modules_picturepath");
                                    modulesFromShip.PriceMoney = dataReader.IsDBNull(dataReader.GetOrdinal("modules_pricemoney")) ? 0 : dataReader.GetInt32("modules_pricemoney");
                                    modulesFromShip.PriceExp = dataReader.IsDBNull(dataReader.GetOrdinal("modules_priceexp")) ? 0 : dataReader.GetInt32("modules_priceexp");

                                    if (modulesFromShip.Name != null)
                                    {
                                        messageShipSend.ModulesList.Add(modulesFromShip);
                                    }
                                }

                                dataReader.Close();

                                if (!publishers.TryGetValue(basePartOfMessage.TopicFromServer, out publisher))
                                {
                                    break;
                                }

                                json = messageShipSend.ToJson();

                                Console.WriteLine("Send: " + json);
                                Console.WriteLine();

                                publisher.SendMessage(json);
                                break;

                            case GeneralConstant.GeneralObjectFromDB.Commander:
                                dataReader = mySQLConnector.GetDataByIdUseDBFunc("get_commander", messageObjectGet.ObjectId.Value);

                                var messageCommanderSend = new DBCommanderMessage();

                                if (dataReader.Read())
                                {
                                    messageCommanderSend.Name = dataReader.GetString("special_commanders_name");
                                    messageCommanderSend.Description = dataReader.GetString("special_commanders_description");
                                    messageCommanderSend.PicturePath = dataReader.GetString("special_commanders_picturepath");
                                    messageCommanderSend.Origins = dataReader.GetString("special_commanders_origins");

                                    var talent = new DBCommanderMessage.DBTalent();
                                    talent.Name = dataReader.IsDBNull(dataReader.GetOrdinal("talents_name")) ? null : dataReader.GetString("talents_name");
                                    talent.Description = dataReader.IsDBNull(dataReader.GetOrdinal("talents_description")) ? null : dataReader.GetString("talents_description");
                                    talent.PicturePath = dataReader.IsDBNull(dataReader.GetOrdinal("talents_picturepath")) ? null : dataReader.GetString("talents_picturepath");

                                    if(talent.Name != null)
                                    {
                                        messageCommanderSend.TalentList.Add(talent);
                                    }
                                }

                                while (dataReader.Read())
                                {
                                    var talent = new DBCommanderMessage.DBTalent();
                                    talent.Name = dataReader.IsDBNull(dataReader.GetOrdinal("talents_name")) ? null : dataReader.GetString("talents_name");
                                    talent.Description = dataReader.IsDBNull(dataReader.GetOrdinal("talents_description")) ? null : dataReader.GetString("talents_description");
                                    talent.PicturePath = dataReader.IsDBNull(dataReader.GetOrdinal("talents_picturepath")) ? null : dataReader.GetString("talents_picturepath");

                                    if (talent.Name != null)
                                    {
                                        messageCommanderSend.TalentList.Add(talent);
                                    }
                                }

                                dataReader.Close();

                                if (!publishers.TryGetValue(basePartOfMessage.TopicFromServer, out publisher))
                                {
                                    break;
                                }

                                json = messageCommanderSend.ToJson();

                                Console.WriteLine("Send: " + json);
                                Console.WriteLine();

                                publisher.SendMessage(json);
                                break;

                            case GeneralConstant.GeneralObjectFromDB.Map:

                                dataReader = mySQLConnector.GetDataByIdUseDBFunc("get_map", messageObjectGet.ObjectId.Value);

                                var messageMapSend = new DBMapMessage();

                                while (dataReader.Read())
                                {
                                    messageMapSend.Name = dataReader.GetString("name");
                                    messageMapSend.Description = dataReader.GetString("description");
                                    messageMapSend.PicturePath = dataReader.GetString("picturepath");
                                    messageMapSend.Battletiers = dataReader.GetString("battletiers");
                                    messageMapSend.Size = dataReader.GetString("size");
                                    messageMapSend.Replyfilename = dataReader.GetString("replyfilename");
                                }

                                dataReader.Close();

                                if (!publishers.TryGetValue(basePartOfMessage.TopicFromServer, out publisher))
                                {
                                    break;
                                }

                                json = messageMapSend.ToJson();

                                Console.WriteLine("Send: " + json);
                                Console.WriteLine();

                                publisher.SendMessage(json);
                                break;

                            case GeneralConstant.GeneralObjectFromDB.PlayerLevel:

                                dataReader = mySQLConnector.GetDataByIdUseDBFunc("get_player_level", messageObjectGet.ObjectId.Value);

                                var messagePlayerLevelSend = new DBPlayerLevelMessage();

                                while (dataReader.Read())
                                {
                                    messagePlayerLevelSend.Name = dataReader.GetString("name");
                                    messagePlayerLevelSend.Description = dataReader.GetString("description");
                                    messagePlayerLevelSend.PicturePath = dataReader.GetString("picturepath");
                                    messagePlayerLevelSend.BattleNeed = dataReader.GetInt32("battleneed");
                                    messagePlayerLevelSend.BattleTotal = dataReader.GetInt32("battletotal");
                                }

                                dataReader.Close();

                                if (!publishers.TryGetValue(basePartOfMessage.TopicFromServer, out publisher))
                                {
                                    break;
                                }

                                json = messagePlayerLevelSend.ToJson();

                                Console.WriteLine("Send: " + json);
                                Console.WriteLine();

                                publisher.SendMessage(json);
                                break;

                            case GeneralConstant.GeneralObjectFromDB.Achievement:
                                dataReader = mySQLConnector.GetDataByIdUseDBFunc("get_achievement", messageObjectGet.ObjectId.Value);

                                var messageAchievementSend = new DBAchievementMessage();

                                while (dataReader.Read())
                                {
                                    messageAchievementSend.Name = dataReader.GetString("achievement_name");
                                    messageAchievementSend.Description = dataReader.GetString("description");
                                    messageAchievementSend.PicturePath = dataReader.GetString("picturepath");
                                    messageAchievementSend.TypeAchievementName = dataReader.GetString("type_achievement_name");
                                }

                                dataReader.Close();

                                if (!publishers.TryGetValue(basePartOfMessage.TopicFromServer, out publisher))
                                {
                                    break;
                                }

                                json = messageAchievementSend.ToJson();

                                Console.WriteLine("Send: " + json);
                                Console.WriteLine();

                                publisher.SendMessage(json);
                                break;

                            case GeneralConstant.GeneralObjectFromDB.Container:
                                dataReader = mySQLConnector.GetDataByIdUseDBFunc("get_container", messageObjectGet.ObjectId.Value);

                                var messageContainerSend = new DBContainerMessage();

                                if (dataReader.Read())
                                {
                                    messageContainerSend.Name = dataReader.GetString("container_name");
                                    messageContainerSend.Description = dataReader.GetString("container_description");
                                    messageContainerSend.PicturePath = dataReader.GetString("container_picturepath");

                                    var itemFromContainer = new DBContainerMessage.DBItem();
                                    itemFromContainer.LootChance = dataReader.GetInt32("loot_chance");
                                    itemFromContainer.ItemName = dataReader.GetString("item_name");
                                    itemFromContainer.TypeItemName = dataReader.GetString("type_item_name");

                                    messageContainerSend.LootList.Add(itemFromContainer);
                                }

                                while (dataReader.Read())
                                {
                                    var itemFromContainer = new DBContainerMessage.DBItem();
                                    itemFromContainer.LootChance = dataReader.GetInt32("loot_chance");
                                    itemFromContainer.ItemName = dataReader.GetString("item_name");
                                    itemFromContainer.TypeItemName = dataReader.GetString("type_item_name");

                                    messageContainerSend.LootList.Add(itemFromContainer);
                                }

                                dataReader.Close();

                                if (!publishers.TryGetValue(basePartOfMessage.TopicFromServer, out publisher))
                                {
                                    break;
                                }

                                json = messageContainerSend.ToJson();

                                Console.WriteLine("Send: " + json);
                                Console.WriteLine();

                                publisher.SendMessage(json);
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