using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebPixCoreAdmin.Models;

namespace WebPixCoreAdmin.Helpers
{
    public class Middleware
    {
        private RequestDelegate _next;
        public Middleware(RequestDelegate next)
        {
            _next = next.Invoke;
        }
        public async Task Invoke(HttpContext context)
        {
            await BeginRequest(context);

            await _next.Invoke(context);

            await EndRequest(context);
        }

        public async Task BeginRequest(HttpContext context)
        {
            string url = context.Request.Host.Value;         
            int porta = context.Request.Host.Port.Value;
            string protocolo = context.Request.Scheme;

            var urlDoCliente = "";
            urlDoCliente = protocolo + "://" + url + "/";

            int idCliente = await PixCore.VerificaUrlClienteAsync(urlDoCliente);
            if (idCliente != 0)
            {
                string cookievalue;
                if (AuxCore.GetCookie(context,"IdCliente") != null)
                {
                    cookievalue = AuxCore.GetCookie(context,"IdCliente").ToString();
                }
                else
                {
                    AuxCore.SetCookieValue(context,idCliente.ToString(), " IdCliente");                   
                }
                await PixCore.RenderUrlPageAsync(idCliente, urlDoCliente,context);
            }
            else
            {
                context.Response.StatusCode = 404;
            }
            
            LoginViewModel usuariologado = PixCore.UsuarioLogado;
            if (usuariologado == null || usuariologado.IdUsuario == 0)
            {
                if (!urlDoCliente.Contains("login/login"))
                {

                    //Verfica login
                    if (usuariologado == null || usuariologado.IdUsuario == 0)
                    {
                        context.Response.Redirect(urlDoCliente + "login/login");
                    }
                    else
                        context.Response.Redirect(urlDoCliente);

                }
            }
            
        }
        public async Task EndRequest(HttpContext context)
        {

        }
    }
}
