using CheckPointBase.Models.Base;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPointBase.Models
{
    [Table("USUARIO")]
    public class Usuario : CoreEntity
    {
        [Column("ID_USUARIO")]
        public int UsuarioId { get; set; }

        [Column("NOME_USUARIO")]
        public string NomeUsuario { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("SENHA")]
        public string Senha { get; set; }

        [Column("NOME_EMPRESA")]
        public string NomeEmpresa { get; set; }

    }
}
