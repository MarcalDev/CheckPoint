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
        [Column("ID_PONTO")]
        public int PontoId { get; set; }

        [Column("DT_PONTO")]
        public DateTime Data { get; set; }

        [Column("LOCAL")]
        public string Local { get; set; }

    }
}
