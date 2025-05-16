using POS_API.BusinessObjects;
using POS_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Unity.Policy;

namespace POS_API.Services
{
    public class SQLService
    {
        private readonly string _connectionString;
        public SQLService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<int> GetMaxId(string tableName, string columnName)
        {
            int nextId = 1;
            string query = $"SELECT ISNULL(MAX({columnName}), 0) + 1 AS max_id FROM {tableName}";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        object result = await command.ExecuteScalarAsync();
                        nextId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching next ID from {tableName}: {ex.Message}", ex);
            }

            return nextId;
        }
        public List<Level2> GetLevel2()
        {
            List<Level2> level2s = new List<Level2>();
            string query = "Select l2.*,l1.varLevel1Name from tblLevel2 l2 inner join tblLevel1 l1 on l2.intLevel1Id = l1.intLevel1Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Level2 level = new Level2
                                {
                                    intLevel2Id = reader.GetInt32(reader.GetOrdinal("intLevel2Id")),
                                    varLevel2Name = reader.GetString(reader.GetOrdinal("varLevel2Name")),
                                    intLevel1Id = reader.GetInt32(reader.GetOrdinal("intLevel1Id")),
                                    varLevel1Name = reader.GetString(reader.GetOrdinal("varLevel1Name")),
                                    intCreatedBy = reader.GetInt32(reader.GetOrdinal("intCreatedBy")),
                                    //dtCreationDate = reader.GetDateTime(reader.GetOrdinal("dtCreationDate")),
                                };
                                level2s.Add(level);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching levels", ex);
            }
            return level2s;
        }

        public Level2 GetLevel2ById(int id)
        {
            Level2 level2s = new Level2();
            string query = "Select l2.*, l1.varLevel1Name from tblLevel2 l2 inner join tblLevel1 l1 on l2.intLevel1Id = l1.intLevel1Id where l2.intLevel2Id = "+ id +" ";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Level2 level = new Level2
                                {
                                    intLevel2Id = reader.GetInt32(reader.GetOrdinal("intLevel2Id")),
                                    varLevel2Name = reader.GetString(reader.GetOrdinal("varLevel2Name")),
                                    intLevel1Id = reader.GetInt32(reader.GetOrdinal("intLevel1Id")),
                                    varLevel1Name = reader.GetString(reader.GetOrdinal("varLevel1Name")),
                                    intCreatedBy = reader.GetInt32(reader.GetOrdinal("intCreatedBy"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching levels", ex);
            }
            return level2s;
        }

        public List<Level3> GetLevel3()
        {
            List<Level3> level3s = new List<Level3>();
            string query = "Select l3.*,l2.varLevel2Name from tblLevel3 l3 inner join tblLevel2 l2 on l3.intLevel2Id = l2.intLevel2Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Level3 level = new Level3
                                {
                                    intLevel3Id = reader.GetInt32(reader.GetOrdinal("intLevel3Id")),
                                    varLevel3Name = reader.GetString(reader.GetOrdinal("varLevel3Name")),
                                    intLevel2Id = reader.GetInt32(reader.GetOrdinal("intLevel2Id")),
                                    varLevel2Name = reader.GetString(reader.GetOrdinal("varLevel2Name")),
                                    intCreatedBy = reader.GetInt32(reader.GetOrdinal("intCreatedBy")),
                                    //dtCreationDate = reader.GetDateTime(reader.GetOrdinal("dtCreationDate")),
                                };
                                level3s.Add(level);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching levels", ex);
            }
            return level3s;
        }
        public Level3 GetLevel3ById(int id)
        {
            Level3 level3s = new Level3();
            string query = "Select l3.*, l2.varLevel2Name from tblLevel3 l3 inner join tblLevel2 l2 on l3.intLevel2Id = l2.intLevel2Id where l3.intLevel3Id = "+ id +" ";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Level3 level = new Level3
                                {
                                    intLevel3Id = reader.GetInt32(reader.GetOrdinal("intLevel3Id")),
                                    varLevel3Name = reader.GetString(reader.GetOrdinal("varLevel3Name")),
                                    intLevel2Id = reader.GetInt32(reader.GetOrdinal("intLevel2Id")),
                                    varLevel2Name = reader.GetString(reader.GetOrdinal("varLevel2Name")),
                                    intCreatedBy = reader.GetInt32(reader.GetOrdinal("intCreatedBy"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching levels", ex);
            }
            return level3s;
        }

        public async Task<int> GetMaxId(string tableName, string columnName, string etype, DateTime? vrdate)
        {
            int nextId = 1;
            string query = $"SELECT ISNULL(MAX({columnName}), 0) + 1 AS max_id FROM {tableName} where dtVrDate = '{vrdate}' and varVrType = '{etype}'";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        object result = await command.ExecuteScalarAsync();
                        nextId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching next ID from {tableName}: {ex.Message}", ex);
            }

            return nextId;
        }

    }
}