using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using GuessResult.Repositories;
using GuessResult.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace GuessResult
{
    public class AutofacConfig
    {
        public static IContainer Container { get; set; }

        public static void ConfigureContainer(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterWebApiModelBinderProvider();

            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly)
               //.Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces()
               .SingleInstance();

            Container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}