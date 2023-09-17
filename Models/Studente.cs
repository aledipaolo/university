using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace university.Models
{
    internal class Studente
    {
        public int studenteID { get; set; }
        public string? matricola {  get; set; }
        public string? nome { get; set; }
        public string? cognome { get; set; }

        public List<Studente> ElencoStudenti { get; set; } = new List<Studente>();

        public override string ToString()
        {
            return $"Studente {studenteID} {matricola} {nome} {cognome}";
        }
    }
}
