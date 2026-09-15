using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using TrainingShippingDB.Models;

namespace TrainingShippingSystem.Backend.DAL
{
    public class BillDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["TrainingShippingDBConnection"].ConnectionString;
        //------------------------------------
        public List<Bill> GetBills()
        {
            List<Bill> bills = new List<Bill>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("GetBills", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        bills.Add(MapReaderToBill(reader));
                }
            }

            return bills;
        }
        //---------------------------------
        public Bill GetBillByID(int id)
        {
            Bill bill = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("GetBillByID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        bill = MapReaderToBill(reader);
                }
            }

            return bill;
        }
        //------------------------------------
        public int InsertBill(Bill bill)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("InsertBill", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BillNumber", bill.BillNumber);
                cmd.Parameters.AddWithValue("@ClientID", bill.ClientID);
                cmd.Parameters.AddWithValue("@VoyageID", bill.VoyageID);
                cmd.Parameters.AddWithValue("@GrossWeight", bill.GrossWeight);
                cmd.Parameters.AddWithValue("@NetWeight", bill.NetWeight);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        //------------------------------------
        public int UpdateBill(Bill bill)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("UpdateBill", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", bill.Id);
                cmd.Parameters.AddWithValue("@BillNumber", bill.BillNumber);
                cmd.Parameters.AddWithValue("@ClientID", bill.ClientID);
                cmd.Parameters.AddWithValue("@VoyageID", bill.VoyageID);
                cmd.Parameters.AddWithValue("@GrossWeight", bill.GrossWeight);
                cmd.Parameters.AddWithValue("@NetWeight", bill.NetWeight);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        //------------------------------------
        public int DeleteBill(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("DeleteBill", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        //------------------------------------
        public List<Bill> SearchBills(string search)
        {
            List<Bill> bills = new List<Bill>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SearchBills", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Search", search);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        bills.Add(MapReaderToBill(reader));
                }
            }

            return bills;
        }
        //------------------------------------
        private Bill MapReaderToBill(SqlDataReader reader)
        {
            return new Bill
            {
                Id = Convert.ToInt32(reader["ID"]),
                BillNumber = reader["BillNumber"].ToString(),
                ClientID = Convert.ToInt32(reader["ClientID"]),
                VoyageID = Convert.ToInt32(reader["VoyageID"]),
                GrossWeight = Convert.ToDecimal(reader["GrossWeight"]),
                NetWeight = Convert.ToDecimal(reader["NetWeight"])
            };
        }
    }
}