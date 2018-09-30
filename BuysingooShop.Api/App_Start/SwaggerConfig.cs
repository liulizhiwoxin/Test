using System.Web.Http;
using WebActivatorEx;
using BuysingooShop.Api;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace BuysingooShop.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "BuysingooShop.Api");
                c.IncludeXmlComments(GetXmlCommentsPath());
                // c.OperationFilter<HttpHeaderFilter>();  // 权限过滤

            }).EnableSwaggerUi(c => {
                c.DocumentTitle("系统开发接口");
                // 使用中文
                c.InjectJavaScript(thisAssembly, "Com.App.SysApi.Scripts.Swagger.swagger_lang.js");
            });
        }

        private static string GetXmlCommentsPath()
        {
            return string.Format("{0}/bin/BuysingooShop.Api.xml", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
