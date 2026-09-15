using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using TrainingShippingDB.Models;

namespace TrainingShippingSystem.Backend.DAL
{
    public class VoyageDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["TrainingShippingDBConnection"].ConnectionString;
        //------------------------------------------
        public List<Voyage> GetVoyages()
        {
            List<Voyage> voyages = new List<Voyage>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("GetVoyages", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        voyages.Add(MapReaderToVoyage(reader));
                }
            }

            return voyages;
        }
        //---------------------------------------
        public Voyage GetVoyageByID(int id)
        {
            Voyage voyage = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("GetVoyageByID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        voyage = MapReaderToVoyage(reader);
                }
            }

            return voyage;
        }
        //---------------------------------------
        public int InsertVoyage(Voyage voyage)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("InsertVoyage", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VoyageNumber", voyage.VoyageNumber);
                cmd.Parameters.AddWithValue("@VesselName", voyage.VesselName);
                cmd.Parameters.AddWithValue("@ETA", voyage.ETA);
                cmd.Parameters.AddWithValue("@ETD", voyage.ETD);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        //----------------------------------------
        public int UpdateVoyage(Voyage voyage)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("UpdateVoyage", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", voyage.Id);
                cmd.Parameters.AddWithValue("@VoyageNumber", voyage.VoyageNumber);
                cmd.Parameters.AddWithValue("@VesselName", voyage.VesselName);
                cmd.Parameters.AddWithValue("@ETA", voyage.ETA);
                cmd.Parameters.AddWithValue("@ETD", voyage.ETD);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        //------------------------------------
        public int DeleteVoyage(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("DeleteVoyage", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        //---------------------------------
        public List<Voyage> SearchVoyages(string search)
        {
            List<Voyage> voyages = new List<Voyage>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SearchVoyages", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Search", search);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        voyages.Add(MapReaderToVoyage(reader));
                }
            }

            return voyages;
        }
        //------------------------------------------
        private Voyage MapReaderToVoyage(SqlDataReader reader)
        {
            return new Voyage
            {
                Id = Convert.ToInt32(reader["ID"]),
                VoyageNumber = reader["VoyageNumber"].ToString(),
                VesselName = reader["VesselName"].ToString(),
                ETA = Convert.ToDateTime(reader["ETA"]),
                ETD = Convert.ToDateTime(reader["ETD"])
            };
        }
    }
}