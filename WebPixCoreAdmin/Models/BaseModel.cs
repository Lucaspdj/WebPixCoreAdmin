using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPixCoreAdmin.Models
{
    public class BaseModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DateAlteracao { get; set; }
        public int UsuarioCriacao { get; set; }
        public int UsuarioEdicao { get; set; }
        public string Ativo { get; set; }
        public int Status { get; set; }
        public int idCliente { get; set; }
    }
}