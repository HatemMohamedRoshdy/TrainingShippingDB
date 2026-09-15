using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using TrainingShippingDB.Models;

namespace TrainingShippingSystem.Backend.DAL
{
    public class ClientDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["TrainingShippingDBConnection"].ConnectionString;

        //-----------------------------------------
        public List<Client> GetClients()
        {
            List<Client> clients = new List<Client>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("GetClients", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients.Add(MapReaderToClient(reader));
                    }
                }
            }

            return clients;
        }

        //---------------------------------------
        public Client GetClientByID(int id)
        {
            Client client = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("GetClientByID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = MapReaderToClient(reader);
                    }
                }
            }

            return client;
        }

        // -------------------------------------
        public int InsertClient(Client client)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("InsertClient", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", client.Name);
                cmd.Parameters.AddWithValue("@Email", client.Email);
                cmd.Parameters.AddWithValue("@Phone", (object)client.Phone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", (object)client.Address ?? DBNull.Value);

                conn.Open();
                object newId = cmd.ExecuteScalar();
                return Convert.ToInt32(newId);
            }
        }

        // ---------------------------------
        public int UpdateClient(Client client)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("UpdateClient", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", client.ID);
                cmd.Parameters.AddWithValue("@Name", client.Name);
                cmd.Parameters.AddWithValue("@Email", client.Email);
                cmd.Parameters.AddWithValue("@Phone", (object)client.Phone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", (object)client.Address ?? DBNull.Value);

                conn.Open();
                object rowsAffected = cmd.ExecuteScalar();
                return Convert.ToInt32(rowsAffected);
            }
        }

       //-------------------------------
        public int DeleteClient(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("DeleteClient", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                object rowsAffected = cmd.ExecuteScalar();
                return Convert.ToInt32(rowsAffected);
            }
        }

     //--------------------------------------
        public List<Client> SearchClients(string search)
        {
            List<Client> clients = new List<Client>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SearchClients", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Search", search);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients.Add(MapReaderToClient(reader));
                    }
                }
            }

            return clients;
        }

        //----------------------------------
        private Client MapReaderToClient(SqlDataReader reader)
        {
            return new Client
            {
                ID = Convert.ToInt32(reader["ID"]),
                Name = reader["Name"].ToString(),
                Email = reader["Email"].ToString(),
                Phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : null,
                Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : null
            };
        }
    }
}