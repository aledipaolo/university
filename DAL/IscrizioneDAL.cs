using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static university.DAL.IDal;
using university.Models;

namespace university.DAL
{
    internal class IscrizioneDAL : IDal<Iscrizione>
    {
        private string? stringaConnessione;

        public IscrizioneDAL(IConfiguration? configurazione)
        {
            if (configurazione != null)
                stringaConnessione = configurazione.GetConnectionString("DatabaseLocale");
        }

        // CRUD
        public bool insert(Iscrizione z)
        {
            using (SqlConnection connessione = new SqlConnection(stringaConnessione))
            {
                try
                {
                    connessione.Open();

                    string query = "INSERT INTO Iscrizione (studenteRIF, corsoRIF) VALUES (@varStudente, @varCorso)";
                    SqlCommand comando = new SqlCommand(query, connessione);

                    comando.Parameters.AddWithValue("@varStudente", z.studenteRIF);
                    comando.Parameters.AddWithValue("@varCorso", z.corsoRIF);

                    int affRows = comando.ExecuteNonQuery();
                    connessione.Close();
                    return affRows > 0;
                }

                catch (Exception error)
                {
                    throw error;
                }
            }
        }

        public List<Iscrizione> findAll()
        {
            List<Iscrizione> ElencoIscrizioni = new List<Iscrizione>();

            using (SqlConnection connessione = new SqlConnection(stringaConnessione))
            {
                try
                {
                    connessione.Open();

                    string query = "SELECT studenteRIF, corsoRIF FROM Iscrizione";
                    SqlCommand comando = new SqlCommand(query, connessione);
                    SqlDataReader reader = comando.ExecuteReader();

                    // read cycle
                    while (reader.Read())
                    {
                        Iscrizione iscrizione = new Iscrizione
                        {
                            studenteRIF = Convert.ToInt32(reader[0]),
                            corsoRIF = Convert.ToInt32(reader[1])
                        };

                        ElencoIscrizioni.Add(iscrizione);
                    }

                    connessione.Close();
                    return ElencoIscrizioni;
                }

                catch (Exception error)
                {
                    throw error;
                }
            }
        }

        public List<Iscrizione> findByCourse(int corso)
        {
            List<Iscrizione> CorsoIscrizioni = new List<Iscrizione>();

            using (SqlConnection connessione = new SqlConnection(stringaConnessione))
            {
                try
                {
                    connessione.Open();

                    string query = "SELECT corsoRIF, studente.matricola FROM Iscrizione JOIN Studente ON studenteID = studenteRIF JOIN Corso ON corsoID = corsoRIF WHERE CorsoRIF = @varCorso";
                    SqlCommand comando = new SqlCommand(query, connessione);

                    comando.Parameters.AddWithValue("@varCorso", corso);
                    SqlDataReader reader = comando.ExecuteReader();

                    // read cycle
                    while (reader.Read())
                    {
                        Iscrizione iscrizione = new Iscrizione
                        {
                            corsoRIF = Convert.ToInt32(reader[0]),
                            Studente = new Studente { matricola = reader[1].ToString() }
                        };

                        CorsoIscrizioni.Add(iscrizione);
                    }

                    connessione.Close();
                    return CorsoIscrizioni;
                }

                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                    return null;
                }
            }
        }

        public List<Iscrizione> findByStudent(int studente)
        {
            List<Iscrizione> StudenteIscrizioni = new List<Iscrizione>();
            using (SqlConnection connessione = new SqlConnection(stringaConnessione))
            {
                try
                {
                    connessione.Open();

                    string query = "SELECT studenteRIF, corso.codice FROM Iscrizione JOIN Studente ON studenteID = studenteRIF JOIN Corso ON corsoID = corsoRIF WHERE studenteRIF = @varStudente";
                    SqlCommand comando = new SqlCommand(query, connessione);

                    comando.Parameters.AddWithValue("@varStudente", studente);
                    SqlDataReader reader = comando.ExecuteReader();

                    // read cycle
                    while (reader.Read())
                    {
                        Iscrizione iscrizione = new Iscrizione
                        {
                            studenteRIF = Convert.ToInt32(reader[0]),
                            Corso = new Corso { codice = reader[1].ToString() },
                        };

                        StudenteIscrizioni.Add(iscrizione);
                    }

                    connessione.Close();
                    return StudenteIscrizioni;
                }

                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                    return null;
                }
            }
        }

        public Iscrizione? findById(int studente, int corso)
        {
            using (SqlConnection connessione = new SqlConnection(stringaConnessione))
            {
                try
                {
                    connessione.Open();

                    string query = "SELECT studenteRIF, corsoRIF FROM Iscrizione WHERE studenteRIF = @varStudente, corsoRIF = @varCorso";
                    SqlCommand comando = new SqlCommand(query, connessione);

                    comando.Parameters.AddWithValue("@varStudente", studente);
                    comando.Parameters.AddWithValue("@varCorso", corso);
                    SqlDataReader reader = comando.ExecuteReader();

                    reader.Read();

                    Iscrizione iscrizione = new Iscrizione()
                    {
                        studenteRIF = Convert.ToInt32(reader[0]),
                        corsoRIF = Convert.ToInt32(reader[1])
                    };

                    connessione.Close();
                    return iscrizione;
                }

                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                    return null;
                }
            }
        }

        public bool update(Iscrizione z)
        {
            throw new NotImplementedException();
        }

        public bool delete(int id)
        {
            throw new NotImplementedException();
        }

        public Iscrizione? findById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
