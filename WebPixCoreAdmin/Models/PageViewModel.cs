using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPixCoreAdmin.Models
{
    public class PageViewModel : BaseModel
    {
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string Url { get; set; }
        public int idMenu { get; set; }
    }
}