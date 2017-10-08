using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Projeto.Armazenamento.Contratos;
using Projeto.Util.Contratos;
using Projeto.Util;
using Projeto.Armazenamento.Repositorios;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using System.Reflection;
using SimpleInjector.Integration.Web.Mvc;

namespace Projeto.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();



            // Register your types, for instance:
            container.Register<IUsuarioRepository, UsuarioRepository>(Lifestyle.Scoped);
            container.Register<ICriptografiaUtil, CriptografiaUtil>(Lifestyle.Scoped);



            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));


            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
