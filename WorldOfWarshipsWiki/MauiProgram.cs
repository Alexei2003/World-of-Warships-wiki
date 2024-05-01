
/* Необъединенное слияние из проекта "WorldOfWarshipsWiki (net8.0-ios)"
До:
using GeneralClasses.Data;
using GeneralClasses;
После:
using GeneralClasses;
using GeneralClasses.Data;
using GoogleGson.Annotations;
*/

/* Необъединенное слияние из проекта "WorldOfWarshipsWiki (net8.0-maccatalyst)"
До:
using GeneralClasses.Data;
using GeneralClasses;
После:
using GeneralClasses;
using GeneralClasses.Data;
using GoogleGson.Annotations;
*/

/* Необъединенное слияние из проекта "WorldOfWarshipsWiki (net8.0-windows10.0.19041.0)"
До:
using GeneralClasses.Data;
using GeneralClasses;
После:
using GeneralClasses;
using GeneralClasses.Data;
using GoogleGson.Annotations;
*/
using Microsoft.Extensions.Logging;
/* Необъединенное слияние из проекта "WorldOfWarshipsWiki (net8.0-ios)"
До:
using static Java.Util.Concurrent.Flow;
using GoogleGson.Annotations;
После:
using static Java.Util.Concurrent.Flow;
*/

/* Необъединенное слияние из проекта "WorldOfWarshipsWiki (net8.0-maccatalyst)"
До:
using static Java.Util.Concurrent.Flow;
using GoogleGson.Annotations;
После:
using static Java.Util.Concurrent.Flow;
*/

/* Необъединенное слияние из проекта "WorldOfWarshipsWiki (net8.0-windows10.0.19041.0)"
До:
using static Java.Util.Concurrent.Flow;
using GoogleGson.Annotations;
После:
using static Java.Util.Concurrent.Flow;
*/


namespace WorldOfWarshipsWiki
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            RabbitMQ.Start();

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
