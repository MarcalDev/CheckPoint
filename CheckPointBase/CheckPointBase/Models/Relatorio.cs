using CheckPointBase.Models.Base;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPointBase.Models
{
    [Table("RELATORIO")]
    public class Relatorio : CoreEntity
    {        
        [Column("DT_RELATORIO")]
        public DateTime Data { get; set; }

        [Column("TEMPO_JORNADA")]
        public TimeSpan TempoJornada { get; set; }

        [Column("SALDO")]
        public TimeSpan Saldo { get; set; }

        [Column("STATUS")]
        public string Status { get; set; }

        [Column("USUARIO_ID")]
        public Guid Fk_IdUsuario { get; set; }
    }
}
