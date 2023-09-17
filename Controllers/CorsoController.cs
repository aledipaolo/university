using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using university.DAL;
using university.Models;

namespace university.Controllers
{
    internal class CorsoController
    {
        private IConfiguration? configurazione;

        private static CorsoController? istanza;

        public static CorsoController getIstanza()
        {
            if (istanza == null)
            {
                istanza = new CorsoController();

                ConfigurationBuilder builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

                istanza.configurazione = builder.Build();
            }

            return istanza;
        }

        private CorsoController()
        {

        }

        public void inserisciCorso(string varCodice, string varNome, int varOre)
        {
            CorsoDAL corsoDal = new CorsoDAL(configurazione);

            Corso corso = new Corso()
            {
                codice = varCodice,
                nome = varNome,
                ore = varOre
            };

            if (corsoDal.insert(corso))
                Console.WriteLine("Successo.");
            else
                Console.WriteLine("Oh no..");
        }

        public void stampaCorsi()
        {
            CorsoDAL corsoDal = new CorsoDAL(configurazione);
            List<Corso> elenco = corsoDal.findAll();

            foreach (Corso oggetto in elenco)
            {
                Console.WriteLine(oggetto);
            }
        }

        public void cercaCorso(int varId)
        {
            CorsoDAL corsoDal = new CorsoDAL(configurazione);

            Console.WriteLine(corsoDal.findById(varId));
        }
    }
}
