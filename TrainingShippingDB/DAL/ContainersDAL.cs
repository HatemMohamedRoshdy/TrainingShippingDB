using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using TrainingShippingDB.Models;

namespace TrainingShippingSystem.Backend.DAL
{
    public class ContainerDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["TrainingShippingDBConnection"].ConnectionString;
        //------------------------------------------
        public List<Container> GetContainers()
        {
            List<Container> containers = new List<Container>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("GetContainers", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        containers.Add(MapReaderToContainer(reader));
                }
            }

            return containers;
        }
        //------------------------------------------
        public Container GetContainerByID(int id)
        {
            Container container = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("GetContainerByID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        container = MapReaderToContainer(reader);
                }
            }

            return container;
        }
        //------------------------------------------
        public int InsertContainer(Container container)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("InsertContainer", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ContainerNumber", container.ContainerNumber);
                cmd.Parameters.AddWithValue("@ContainerType", container.ContainerType);
                cmd.Parameters.AddWithValue("@BillID", container.BillID);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        //------------------------------------------
        public int UpdateContainer(Container container)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("UpdateContainer", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", container.Id);
                cmd.Parameters.AddWithValue("@ContainerNumber", container.ContainerNumber);
                cmd.Parameters.AddWithValue("@ContainerType", container.ContainerType);
                cmd.Parameters.AddWithValue("@BillID", container.BillID);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        //------------------------------------------
        public int DeleteContainer(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("DeleteContainer", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        //------------------------------------------
        public List<Container> SearchContainers(string search)
        {
            List<Container> containers = new List<Container>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SearchContainers", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Search", search);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        containers.Add(MapReaderToContainer(reader));
                }
            }

            return containers;
        }
        //------------------------------------------
        private Container MapReaderToContainer(SqlDataReader reader)
        {
            return new Container
            {
                Id = Convert.ToInt32(reader["ID"]),
                ContainerNumber = reader["ContainerNumber"].ToString(),
                ContainerType = reader["ContainerType"].ToString(),
                BillID = Convert.ToInt32(reader["BillID"])
            };
        }
    }
}