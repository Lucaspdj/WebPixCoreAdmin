using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPixCoreAdmin.Models
{
    public class ConfiguracaoViewModel : BaseModel
    {
        public string Chave { get; set; }
        public string Valor { get; set; }
    }
}
