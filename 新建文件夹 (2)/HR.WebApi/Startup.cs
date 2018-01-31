using System;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.OAuth;
using HR.WebApi.Data.Contexts;
using HR.WebApi.Providers;
using Owin;
using Swashbuckle.Application;
using System.Xml.XPath;
using System.Linq;

[assembly: OwinStartup(typeof(HR.WebApi.Startup))]
namespace HR.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // Swagger
            SwaggerConfig.Register(config);

            //注册 log4net
            log4net.Config.XmlConfigurator.Configure(
               new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\log4net.config")
           );

            //RouteConfig.RegisterRoutes(config);
            WebApiConfig.Register(config);

            app.UseCors(CorsOptions.AllowAll);
            ConfigureOAuth(app);

            app.UseWebApi(config);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuthContext, Migrations.Configuration>());
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            //xhr.setRequestHeader('Authorization', 'Bearer ' + $.cookie("token")); 
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(10),
                Provider = new SimpleAuthorizationServerProvider(),

                //refresh token provider
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
          
        }

    }
}