using log4net;
using Truesec.Decryptors.Interfaces;

namespace Truesec.Decryptors.Providers
{
    internal class Log4NetProvider: ILogger
    {
        private readonly ILog log;
        public Log4NetProvider()
        {
            log = LogManager.GetLogger("Generic");
            log4net.Config.XmlConfigurator.Configure();
        }

        public void Log(string message)
        {
            log?.Info(message);
        }
    }
}
