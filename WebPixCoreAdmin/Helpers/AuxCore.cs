using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPixCoreAdmin.Models;

namespace WebPixCoreAdmin.Helpers
{
    public class AuxCore
    {

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

        public static void SetCookieValue(LoginViewModel user)
        {
            var current = IhttpContextAccessor.HttpContext;
            string cookievalue;
            if (current.Request.Cookies["UsuarioLogado"] != null)
            {
                cookievalue = current.Request.Cookies["UsuarioLogado"].ToString();
            }
            else
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddMinutes(30);
                current.Response.Cookies.Append("UsuarioLogado", JsonConvert.SerializeObject(user), option);
            }
        }



        public static object GetCookie(string cookie)
        {
            var current = IhttpContextAccessor.HttpContext;
            return current.Request.Cookies[cookie];
        }
    }
}
