using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TrainingShippingDB.Models;
using TrainingShippingSystem.Backend.DAL;

namespace TrainingShippingSystem.Backend.BLL
{
    public class ClientBLL
    {
        private ClientDAL clientDAL = new ClientDAL();

        public List<Client> GetClients()
        {
            return clientDAL.GetClients();
        }

        public Client GetClientByID(int id)
        {
            return clientDAL.GetClientByID(id);
        }

        public int InsertClient(Client client)
        {
            ValidateClient(client);
            return clientDAL.InsertClient(client);
        }

        public int UpdateClient(Client client)
        {
            ValidateClient(client);
            return clientDAL.UpdateClient(client);
        }

        public int DeleteClient(int id)
        {
            return clientDAL.DeleteClient(id);
        }

        public List<Client> SearchClients(string search)
        {
            return clientDAL.SearchClients(search);
        }

        private void ValidateClient(Client client)
        {
            if (string.IsNullOrWhiteSpace(client.Name))
                throw new Exception("Name is required.");

            if (string.IsNullOrWhiteSpace(client.Email))
                throw new Exception("Email is required.");

            if (!Regex.IsMatch(client.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new Exception("Email format is invalid.");

            if (string.IsNullOrWhiteSpace(client.TaxNumber))
                throw new Exception("Tax Number is required.");
        }
    }
}