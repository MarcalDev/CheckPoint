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
        [Column("ID_RELATORIO")]
        public int RelatorioId { get; set; }

        [Column("DT_RELATORIO")]
        public DateTime Data { get; set; }

        [Column("TEMPO_JORNADA")]
        public DateTime TempoJornada { get; set; }

        [Column("SALDO")]
        public DateTime Saldo { get; set; }

        [Column("STATUS")]
        public string Status { get; set; }

        [Column("ID_USUARIO")]
        public int UsuarioId { get; set; }
    }
}
