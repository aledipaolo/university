using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using university.Models;
using static university.DAL.IDal;

namespace university.DAL
{
    internal class CorsoDAL : IDal<Corso>
    {
        private string? stringaConnessione;

        public CorsoDAL(IConfiguration? configurazione)
        {
            if (configurazione != null)
                stringaConnessione = configurazione.GetConnectionString("DatabaseLocale");
        }

        // CRUD
        public bool insert(Corso z)
        {
            using (SqlConnection connessione = new SqlConnection(stringaConnessione))
            {
                try
                {
                    connessione.Open();

                    string query = "INSERT INTO Corso (codice, nome, ore) VALUES (@varCodice, @varNome, @varOre)";
                    SqlCommand comando = new SqlCommand(query, connessione);

                    comando.Parameters.AddWithValue("@varCodice", z.codice);
                    comando.Parameters.AddWithValue("@varNome", z.nome);
                    comando.Parameters.AddWithValue("@varOre", z.ore);

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

        public List<Corso> findAll()
        {
            List<Corso> ElencoCorsi = new List<Corso>();

            using (SqlConnection connessione = new SqlConnection(stringaConnessione))
            {
                try
                {
                    connessione.Open();

                    string query = "SELECT corsoID, codice, nome, ore FROM Corso";
                    SqlCommand comando = new SqlCommand(query, connessione);
                    SqlDataReader reader = comando.ExecuteReader();

                    // read cycle
                    while (reader.Read())
                    {
                        Corso corso = new Corso
                        {
                            corsoID = Convert.ToInt32(reader[0]),
                            codice = reader[1].ToString(),
                            nome = reader[2].ToString(),
                            ore = Convert.ToInt32(reader[3])
                        };

                        ElencoCorsi.Add(corso);
                    }

                    connessione.Close();
                    return ElencoCorsi;
                }

                catch (Exception error)
                {
                    throw error;
                }
            }
        }

        public Corso? findById(int id)
        {
            using (SqlConnection connessione = new SqlConnection(stringaConnessione))
            {
                try
                {
                    connessione.Open();
                    
                    string query = "SELECT corsoID, codice, nome, ore FROM Corso WHERE corsoID = @varId;";
                    SqlCommand comando = new SqlCommand(query, connessione);

                    comando.Parameters.AddWithValue("@varId", id);
                    SqlDataReader reader = comando.ExecuteReader();

                    reader.Read();

                    Corso corso = new Corso()
                    {
                        corsoID = Convert.ToInt32(reader[0]),
                        codice = reader[1].ToString(),
                        nome = reader[2].ToString(),
                        ore = Convert.ToInt32(reader[3])
                    };

                    connessione.Close();
                    return corso;
                }

                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                    return null;
                }
            }
        }

        public bool update(Corso z)
        {

            throw new NotImplementedException();

            /*
            using (SqlConnection connessione = new SqlConnection(stringaConnessione))
            {
                try
                {
                    connessione.Open();

                    string query = "UPDATE Corso SET codice = @varCodice, nome = @varNome, ore = @varOre WHERE corsoID = @varId;";
                    SqlCommand comando = new SqlCommand(query, connessione);

                    comando.Parameters.AddWithValue("@varId", z.corsoID);
                    comando.Parameters.AddWithValue("@varCodice", z.codice);
                    comando.Parameters.AddWithValue("@varNome", z.nome);
                    comando.Parameters.AddWithValue("@varOre", z.ore);

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

                    string query = "DELETE FROM Corso WHERE corsoID = @varId;";
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
