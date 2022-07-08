using CheckPointBase.Models.Base;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPointBase.Models
{
    [Table("PONTO")]
    public class Ponto : CoreEntity
    {
        
        [Column("DT_INICIO")]
        public DateTime DataInicio { get; set; }

        [Column("DT_FIM")]
        public DateTime DataFim { get; set; }

        [Column("LOCAL")]
        public string Local { get; set; }

        [Column("USUARIO_ID")]
        public Guid Fk_IdUsuario { get; set; }

        [Column("RELATORIO_ID")]
        public Guid Fk_IdRelatorio { get; set; }

    }
}
