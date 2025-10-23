using Hospital.Core;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data
{
    public class MySqlDrugRepository : IDrugRepository
    {
        private readonly string _connectionString;

        public MySqlDrugRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool CreateDrug(Drug drug, out int createdId)
        {
            createdId = 0;
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO Drug (name, form, dosage, expiration_date) 
                                            VALUES (@name, @form, @dosage, @expiration_date);
                                            SELECT LAST_INSERT_ID();";
                        cmd.Parameters.AddWithValue("@name", drug.Name);
                        cmd.Parameters.AddWithValue("@form", drug.Form ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@dosage", drug.Dosage ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@expiration_date", drug.ExpirationDate);

                        // Выполнение: получим ID новой записи
                        object res = cmd.ExecuteScalar();
                        if (res != null && int.TryParse(res.ToString(), out int id))
                        {
                            createdId = id;
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки: ex.Message
                return false;
            }
        }

        public Drug GetDrugById(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT drug_id, name, form, dosage, expiration_date FROM Drug WHERE drug_id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Drug
                            {
                                Id = reader.GetInt32("drug_id"),
                                Name = reader.GetString("name"),
                                Form = reader.IsDBNull(reader.GetOrdinal("form")) ? null : reader.GetString("form"),
                                Dosage = reader.IsDBNull(reader.GetOrdinal("dosage")) ? null : reader.GetString("dosage"),
                                ExpirationDate = reader.IsDBNull(reader.GetOrdinal("expiration_date")) ? DateTime.MinValue : reader.GetDateTime("expiration_date")
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
