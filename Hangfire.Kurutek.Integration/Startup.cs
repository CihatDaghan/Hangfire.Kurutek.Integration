using Hangfire.Kurutek.Integration.Controllers;
using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(Hangfire.Kurutek.Integration.Startup))]

namespace Hangfire.Kurutek.Integration
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            
            GlobalConfiguration.Configuration.UseSqlServerStorage(@"Server=DESKTOP-2RFMJGK\SQLEXPRESS;Database=Hangfire.Kurutek.Integration;Integrated Security=true");

           
            app.UseHangfireDashboard();

           
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate("N11_Siparis_entegrasyon", () => N11OrdersController.N11Orders(), Cron.MinuteInterval(3));

        }
    }
}
