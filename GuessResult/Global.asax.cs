using GuessResult.Jobs;
using GuessResult.Repositories;
using GuessResult.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GuessResult
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.ConfigureContainer(GlobalConfiguration.Configuration);
            JobsConfiguration.Configure();


            //EventRepository eventRepository = new EventRepository();
            //var allEvents = eventRepository.GetAll(null, false, 0);
            //UserEventRepository userEventRepository = new UserEventRepository();
            //foreach (var item in allEvents)
            //{
            //    userEventRepository.UpdateIsPredictionCorrect(item.Id);
            //}

            //new FootballDataApiService().ImportEvents();
        }
    }
}
