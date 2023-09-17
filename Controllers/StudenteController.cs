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
    internal class StudenteController
    {
        private IConfiguration? configurazione;

        private static StudenteController? istanza;

        public static StudenteController getIstanza()
        {
            if (istanza == null)
            {
                istanza = new StudenteController();

                ConfigurationBuilder builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

                istanza.configurazione = builder.Build();
            }

            return istanza;
        }

        private StudenteController()
        {
        
        }

        public void inserisciStudente(string varMatricola, string varNome, string varCognome)
        {
            StudenteDAL stuDal = new StudenteDAL(configurazione);

            Studente stu = new Studente()
            {
                matricola = varMatricola,
                nome = varNome,
                cognome = varCognome
            };

            if (stuDal.insert(stu))
                Console.WriteLine("Successo.");
            else
                Console.WriteLine("Oh no..");
        }

        public void stampaStudenti()
        {
            StudenteDAL stuDal = new StudenteDAL(configurazione);
            List<Studente> elenco = stuDal.findAll();
        
            foreach (Studente oggetto in elenco)
            {
                Console.WriteLine(oggetto);
            }
        }
        
        public void cercaStudente(int varId)
        {
            StudenteDAL stuDal = new StudenteDAL(configurazione);
        
            Console.WriteLine(stuDal.findById(varId));
        }
    }
}
