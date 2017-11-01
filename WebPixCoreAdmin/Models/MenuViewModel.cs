using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPixCoreAdmin.Models
{
    public class MenuViewModel : BaseModel
    {
        public string Url { get; set; }
        public int Pai { get; set; }
        public int Tipo { get; set; }
    }
}