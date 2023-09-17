using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using university.Models;
using static university.DAL.IDal;

namespace university.DAL
{
    internal class StudenteDAL : IDal<Studente>
    {
        private string? stringaConnessione;

        public StudenteDAL(IConfiguration? configurazione)
        {
            if (configurazione != null)
                stringaConnessione = configurazione.GetConnectionString("DatabaseLocale");
        }

        // CRUD
        public bool insert(Studente z)
        {
            using (SqlConnection connessione = new SqlConnection(stringaConnessione))
            {
                try
                {
                    connessione.Open();

                    string query = "INSERT INTO Studente (matricola, nome, cognome) VALUES (@varMatricola, @varNome, @varCognome)";
                    SqlCommand comando = new SqlCommand(query, connessione);

                    comando.Parameters.AddWithValue("@varMatricola", z.matricola);
                    comando.Parameters.AddWithValue("@varNome", z.nome);
                    comando.Parameters.AddWithValue("@varCognome", z.cognome);

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

        public List<Studente> findAll()
        {
            List<Studente> ElencoStudenti = new List<Studente>();

            using (SqlConnection connessione = new SqlConnection(stringaConnessione))
            {
                try
                {
                    connessione.Open();

                    string query = "SELECT studenteID, matricola, nome, cognome FROM Studente";
                    SqlCommand comando = new SqlCommand(query, connessione);
                    SqlDataReader reader = comando.ExecuteReader();

                    // read cycle
                    while (reader.Read())
                    {
                        Studente studente = new Studente
                        {
                            studenteID = Convert.ToInt32(reader[0]),
                            matricola = reader[1].ToString(),
                            nome = reader[2].ToString(),
                            cognome = reader[3].ToString()
                        };

                        ElencoStudenti.Add(studente);
                    }
                   
                    connessione.Close();
                    return ElencoStudenti;
                }

                catch (Exception error)
                {
                    throw error;
                }
            }
        }

        public Studente? findById(int id)
        {
            using (SqlConnection connessione = new SqlConnection(stringaConnessione))
            {
                try
                {
                    connessione.Open();

                    string query = "SELECT studenteID, matricola, nome, cognome FROM Studente WHERE studenteID = @varId;";
                    SqlCommand comando = new SqlCommand(query, connessione);
                    
                    comando.Parameters.AddWithValue("@varId", id);
                    SqlDataReader reader = comando.ExecuteReader();

                    reader.Read();

                    Studente studente = new Studente()
                    {
                        studenteID = Convert.ToInt32(reader[0]),
                        matricola = reader[1].ToString(),
                        nome = reader[2].ToString(),
                        cognome = reader[3].ToString()
                    };

                    connessione.Close();
                    return studente;
                }

                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                    return null;
                }
            }
        }

        public bool update(Studente z)
        {
            throw new NotImplementedException();

            /*
            using (SqlConnection connessione = new SqlConnection(stringaConnessione))
            {
                try
                {
                    connessione.Open();

                    string query = "UPDATE Studente SET matricola = @varMatricola, nome = @varNome, cognome = @varCognome WHERE studenteID = @varId;";
                    SqlCommand comando = new SqlCommand(query, connessione);

                    comando.Parameters.AddWithValue("@varId", z.studenteID);
                    comando.Parameters.AddWithValue("@varMatricola", z.matricola);
                    comando.Parameters.AddWithValue("@varNome", z.nome);
                    comando.Parameters.AddWithValue("@varCognome", z.cognome);

                    int affRows = comando.ExecuteNonQuery();
                    connessione.Close();
                    return affRows > 0;
                }

                catch (Exception error)
                {
                    throw error;
                }
            }
            */
        }

        public bool delete(int id)
        {
            throw new NotImplementedException();

            /*
            using (SqlConnection connessione = new SqlConnection(stringaConnessione))
            {
                try
                {
                    connessione.Open();

                    string query = "DELETE FROM Studente WHERE studenteID = @varId;";
                    SqlCommand comando = new SqlCommand(query, connessione);

                    comando.Parameters.AddWithValue("@varId", id);

                    int rowsAffected = comando.ExecuteNonQuery();
                    connessione.Close();
                    return rowsAffected > 0;
                }

                catch (Exception error)
                {
                    throw error;
                }
            }
            */
        }
    }
}
