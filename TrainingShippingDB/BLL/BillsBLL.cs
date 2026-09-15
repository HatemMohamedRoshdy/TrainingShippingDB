using System;
using System.Collections.Generic;
using TrainingShippingDB.Models;
using TrainingShippingSystem.Backend.DAL;

namespace TrainingShippingSystem.Backend.BLL
{
    public class BillBLL
    {
        private BillDAL billDAL = new BillDAL();

        public List<Bill> GetBills()
        {
            return billDAL.GetBills();
        }

        public Bill GetBillByID(int id)
        {
            return billDAL.GetBillByID(id);
        }

        public int InsertBill(Bill bill)
        {
            ValidateBill(bill);
            return billDAL.InsertBill(bill);
        }

        public int UpdateBill(Bill bill)
        {
            ValidateBill(bill);
            return billDAL.UpdateBill(bill);
        }

        public int DeleteBill(int id)
        {
            return billDAL.DeleteBill(id);
        }

        public List<Bill> SearchBills(string search)
        {
            return billDAL.SearchBills(search);
        }

        private void ValidateBill(Bill bill)
        {
            if (string.IsNullOrWhiteSpace(bill.BillNumber))
                throw new Exception("Bill Number is required.");

            if (bill.ClientID <= 0)
                throw new Exception("Client is required.");

            if (bill.VoyageID <= 0)
                throw new Exception("Voyage is required.");

            if (bill.GrossWeight <= 0)
                throw new Exception("Gross Weight is required.");

            if (bill.NetWeight <= 0)
                throw new Exception("Net Weight is required.");

            if (bill.NetWeight > bill.GrossWeight)
                throw new Exception("Net Weight cannot be greater than Gross Weight.");
        }
    }
}