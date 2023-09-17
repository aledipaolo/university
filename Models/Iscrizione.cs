using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace university.Models
{
    internal class Iscrizione
    {
        public Studente? Studente { get; set; }
        public Corso? Corso { get; set; }
        public int studenteRIF {  get; set; }
        public int corsoRIF { get; set; }

        public List<Iscrizione> ElencoIscrizioni { get; set; } = new List<Iscrizione>();

        public override string ToString()
        {
            return $"Iscrizione {studenteRIF} {corsoRIF}";
        }
    }
}
