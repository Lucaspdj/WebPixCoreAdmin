using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebPixCoreAdmin.Helpers.Config;
using WebPixCoreAdmin.Models;

namespace WebPixCoreAdmin.Helpers
{

    public class PixCore
    {
        
        #region Propiedades

        private LoginViewModel usuarioLogado;
        public static LoginViewModel UsuarioLogado
        {
            get { return VerificaLogado(); }
        }

        private static int IdCliente;
        public static int IDCliente
        {
            get
            {
                var urlDoCliente = AuxCore.GetUrl();
                var DefaultSiteUrl = AuxCore.GetAbsoluteUri();
                
                if (AuxCore.GetCookie("IdCliente") != null)
                {
                    var cookiesValido = AuxCore.GetCookie("IdCliente");
                    IdCliente = JsonConvert.DeserializeObject<int>(cookiesValido.ToString());
                    return IdCliente;
                }
                else
                {
                   AuxCore.Response301(DefaultSiteUrl);
                    return 0;
                }

            }

        }

        private static string DefaultSiteUrl;
        public static string defaultSiteUrl
        {
            get
            {
                //string url = HttpContext.Current.Request.Url.Host;
                //int porta = HttpContext.Current.Request.Url.Port;
                //string protocolo = HttpContext.Current.Request.Url.Scheme;
                //if (porta != 80)
                //{
                //    DefaultSiteUrl = aux
                //}
                //else
                //{
                //    DefaultSiteUrl = protocolo + "://" + url + "/";
                //}
                return AuxCore.GetUrl();
            }
        }
        static IOptions<Configuration> _Configuration;

        #endregion
        #region Contructors inferiores
        public PixCore(IOptions<Configuration> configs)
        {
            _Configuration = configs;
        }
        #endregion

        public static async Task<int> VerificaUrlClienteAsync(string urlDoCliente)
        {
            var keyUrlIn = _Configuration.Value.UrlAPI; // _Configuration.UrlAPIIn;
            var urlAPIIn = keyUrlIn + "cliente";

            RestClient client = new RestClient(keyUrlIn);
            RestRequest request = null;
            request = new RestRequest(urlAPIIn, Method.GET);
            var response = await client.ExecuteTaskAsync(request);
            ClienteViewModel[] Cliente = JsonConvert.DeserializeObject<ClienteViewModel[]>(response.Content);

            var clienteLol = Cliente.Where(x => urlDoCliente.Contains(x.Url)).FirstOrDefault();
            if (clienteLol != null)
            {
                return clienteLol.ID;
            }
            else
            {
                return 0;
            }
        }
        public static async Task RenderUrlPageAsync()
        {

            var keyUrlIn = _Configuration.Value.UrlAPI;
            var urlAPIIn = keyUrlIn + "Seguranca/Principal/buscarEstilo/" + IDCliente + "/" + 999;

            RestClient client = new RestClient(keyUrlIn);
            RestRequest request = null;
            request = new RestRequest(urlAPIIn, Method.GET);
            var response = await client.ExecuteTaskAsync(request);
            PageViewModel[] Cliente = JsonConvert.DeserializeObject<PageViewModel[]>(response.Content);

            PageViewModel page = Cliente.Where(y => y.Url == AuxCore.GetUrl()).FirstOrDefault();
            if (page != null)
            {
                if ( AuxCore.GetAbsoluteUri() != (AuxCore.GetUrl() + "page/index/" + page.ID.ToString()))
                {
                    AuxCore.Response301(DefaultSiteUrl, page.ID.ToString());                  
                }
            }
            else
            {
                // HttpContext.Current.Response.StatusCode = 404;
            }
            
        }
        //Controle de login deus me ajuda OMG :O
        public static async Task<bool> LoginAsync(LoginViewModel user)
        {
            user.idCliente = IDCliente;
            
            var keyUrlIn = _Configuration.Value.UrlAPI;
            var urlAPIIn = keyUrlIn + "Seguranca/Principal/loginUsuario/" + IDCliente + "/" + 999;

            RestClient client = new RestClient(keyUrlIn);
            RestRequest request = null;
            request.AddHeader("ContentType","application /json");
            object envio = new { ObjLogin = user };
            request.AddBody(envio);
            request = new RestRequest(urlAPIIn, Method.POST);
            var response = await client.ExecuteTaskAsync(request);
            UsuarioViewModel Usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(response.Content);
            
            if (Usuario.ID != 0)
            {
                if (Convert.ToBoolean(Usuario.VAdmin))
                {
                    user.idCliente = 1;
                    user.idPerfil = Usuario.PerfilUsuario;
                    user.IdUsuario = Usuario.ID;

                    AuxCore.SetCookieValue(user,"UsuarioLogado");
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return false;
            }



        }
        public static LoginViewModel VerificaLogado()
        {
            var usuariologado = AuxCore.GetCookie("UsuarioLogado");
            if (usuariologado != null)
            {
                var cookiesValido = usuariologado;
                LoginViewModel Usuario = JsonConvert.DeserializeObject<LoginViewModel>(cookiesValido.ToString());
                return Usuario;
            }
            else
            {
                //current.Response.Redirect("http://localhost:49983/login/login");
                return new LoginViewModel();
            }
        }
    }
}
