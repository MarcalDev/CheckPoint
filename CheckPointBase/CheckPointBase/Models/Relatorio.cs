using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPointBase.Models
{
    public class Relatorio
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public DateTime TempoJornada { get; set; }
        public DateTime Saldo { get; set; }
        public string Status { get; set; }
        public int IdUsuario { get; set; }
    }
}
