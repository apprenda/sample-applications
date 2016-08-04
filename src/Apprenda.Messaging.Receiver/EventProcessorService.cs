using Apprenda.SaaSGrid;
using Apprenda.Services.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Apprenda.Messaging.Receiver
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EventProcessorService : IEventProcessorService
    {
        private static readonly ILogger log = LogManager.Instance().GetLogger(typeof(EventProcessorService));

        static EventProcessorService()
        {
            //Register the event handler
            ApplicationContext.Current.EventManager.Event<string>("message").OnEvent += OnMessageReceived;
            log.Info("EventHandler is registered.");
        }
        
        static void OnMessageReceived(string message)
        {
            var now = DateTime.UtcNow;
            var computerName = Environment.MachineName;
            log.InfoFormat("[{0}: {1}] Message received: {2}", now, computerName, message);

            //Insert into the application-scoped cached, the message sent by the UI. 
            ApplicationContext.Current.Cache.Insert("message", message, TimeSpan.FromSeconds(5));
        }

        public void Echo(string message)
        {
            ApplicationContext.Current.Cache.Insert("message", string.Format("WCF: {0}", message), TimeSpan.FromSeconds(5));
        }
    }
}
