using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPointBase.Models.Base
{
    public class CoreEntity
    {
        /// <summary>
        /// Primary key da tabela
        /// </summary>
        [PrimaryKey]
        //public int Id { get; set; } 
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Codigo externo do registro (vindo de integrações)
        /// </summary>
        public string CodigoExterno { get; set; }

        /// <summary>
        /// Data de inclusão do registro
        /// </summary>
        public DateTime? Alteracao { get; set; }

        /// <summary>
        /// Data de inclusão do registro
        /// </summary>
        public DateTime Inclusao { get; set; }

        /// <summary>
        /// Flag de ativo
        /// </summary>
        public int Ativo { get; set; }
    }
}
