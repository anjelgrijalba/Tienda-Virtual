using System.Web.Mvc;

namespace PresentacionAspNetMvc.Areas.Backend
{
    public class BackendAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Backend";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Backend_default",
                "Backend/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                    new string[] { "PresentacionAspNetMvc.Areas.Backend.Controllers" }
            );

            //new string[] {"EjemploASPNETMVCAreas.Areas.Backend.Controllers"}

            //Añadido para que no haya problemas con controladores que se llaman
            //igual dentro y fuera del área.
        }
    }
}