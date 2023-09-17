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
    internal class IscrizioneController
    {
        private IConfiguration? configurazione;

        private static IscrizioneController? istanza;

        public static IscrizioneController getIstanza()
        {
            if (istanza == null)
            {
                istanza = new IscrizioneController();

                ConfigurationBuilder builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

                istanza.configurazione = builder.Build();
            }

            return istanza;
        }

        private IscrizioneController()
        {

        }

        public void inserisciIscrizione(int varStudente, int varCorso)
        {
            IscrizioneDAL iscrizioneDal = new IscrizioneDAL(configurazione);

            Iscrizione iscrizione = new Iscrizione()
            {
                studenteRIF = varStudente,
                corsoRIF = varCorso
            };

            if (iscrizioneDal.insert(iscrizione))
                Console.WriteLine("Successo.");
            else
                Console.WriteLine("Oh no..");
        }

        public void stampaIscrizioni()
        {
            IscrizioneDAL iscrizioneDal = new IscrizioneDAL(configurazione);
            List<Iscrizione> elenco = iscrizioneDal.findAll();

            foreach (Iscrizione oggetto in elenco)
            {
                Console.WriteLine(oggetto);
            }
        }

        public void cercaIscrizionePerCorso(int varId)
        {
            IscrizioneDAL iscrizioneDal = new IscrizioneDAL(configurazione);
            List<Iscrizione> elenco = iscrizioneDal.findByCourse(varId);

            foreach (Iscrizione oggetto in elenco)
            {
                Console.WriteLine($"ID Corso: {oggetto.corsoRIF}, Matricola Studente: {oggetto.Studente.matricola}");
            }
        }

        public void cercaIscrizionePerStudente(int varId)
        {
            IscrizioneDAL iscrizioneDal = new IscrizioneDAL(configurazione);
            List<Iscrizione> elenco = iscrizioneDal.findByStudent(varId);

            foreach (Iscrizione oggetto in elenco)
            {
                Console.WriteLine($"ID Studente: {oggetto.studenteRIF}, Codice Corso: {oggetto.Corso.codice}");
            }
        }
    }
}
