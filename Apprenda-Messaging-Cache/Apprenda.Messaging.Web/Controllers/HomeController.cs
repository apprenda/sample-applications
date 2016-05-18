using Apprenda.Messaging.Web.Models;
using Apprenda.SaaSGrid;
using Apprenda.Services.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apprenda.Messaging.Web.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILogger log = LogManager.Instance().GetLogger(typeof(HomeController));

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            string message;
            try
            {
                message = ApplicationContext.Current.Cache.Find<string>("message");

                if (string.IsNullOrEmpty(message))
                {
                    log.InfoFormat("A cached message was not found: {0}", message);

                    message = "No message was cached yet.";
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Failed to load the Index page: {0}", ex.Message);

                throw ex;
            }

            return View(new MessageModel { CachedMessage = message, ComputerName = Environment.MachineName});
        }

        [HttpPost]
        public ActionResult Send(MessageModel model)
        {
            try 
	        {	        
		        var now = DateTime.UtcNow;
                log.InfoFormat("{0}: Message is about to be sent: {1}", now, model.Message);

                ApplicationContext.Current.EventManager.Event<string>("message").Invoke(model.Message);

                log.InfoFormat("{0}: Message is sent: {1}", now, model.Message);
	        }
	        catch (Exception ex)
	        {
                log.ErrorFormat("Failed to process the Send action: {0}", ex.Message);

                throw ex;
	        }

            return RedirectToAction("Index");
        }
    }
}
