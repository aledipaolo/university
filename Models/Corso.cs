using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace university.Models
{
    internal class Corso
    {
        public int corsoID { get; set; }
        public string? codice { get; set; }
        public string? nome { get; set; }
        public int? ore { get; set; }

        public List<Corso> ElencoCorsi { get; set; } = new List<Corso>();

        public override string ToString()
        {
            return $"Corso {corsoID} {codice} {nome} {ore}";
        }
    }
}
