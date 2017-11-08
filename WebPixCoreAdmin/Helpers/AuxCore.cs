using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace WebPixCoreAdmin.Helpers
{
    public class AuxCore
    {
        #region Contrutores
        public AuxCore()
        {
            ///Logical constructor empty
        }


        #endregion

        #region Controle de Contexto
        private static IHttpContextAccessor IhttpContextAccessor;
        public static void SetHttpContextAccessor(IHttpContextAccessor accessor)
        {
            IhttpContextAccessor = accessor;
        }
        public static string GetUrl()
        {
            var HttpContext = IhttpContextAccessor.HttpContext;
            var url = string.Format("{0}://{1}",
                HttpContext.Request.Scheme,
                HttpContext.Request.Host);

            return url;
        }
        public static string GetAbsoluteUri()
        {
            var request = IhttpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.ToString();
            uriBuilder.Path = request.Path.ToString();
            uriBuilder.Query = request.QueryString.ToString();
            return uriBuilder.Uri.AbsoluteUri;
        }
        public static void Response301(string defaultSiteUrl)
        {
            var response = IhttpContextAccessor.HttpContext.Response;
            response.Redirect(defaultSiteUrl, true);
        }
        public static void Response301(string defaultSiteUrl, string path)
        {
            var response = IhttpContextAccessor.HttpContext.Response;
            response.Redirect(defaultSiteUrl + path, true);
        }
        public static void SetCookieValue(HttpContext context,Object obj, string nome)
        {
          
            string cookievalue;
            if (context.Request.Cookies[nome] != null)
            {
                cookievalue = context.Request.Cookies[nome].ToString();
            }
            else
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddMinutes(30);
                context.Response.Cookies.Append(nome, JsonConvert.SerializeObject(obj), option);
            }
        }
        public static object GetCookie(HttpContext context,string cookie)
        {
           // var current = IhttpContextAccessor.HttpContext;
            return context.Request.Cookies[cookie];
        }
        #endregion

      

    }
}
