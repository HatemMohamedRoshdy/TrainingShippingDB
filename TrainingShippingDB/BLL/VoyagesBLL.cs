using System;
using System.Collections.Generic;
using TrainingShippingDB.Models;
using TrainingShippingSystem.Backend.DAL;

namespace TrainingShippingSystem.Backend.BLL
{
    public class VoyageBLL
    {
        private VoyageDAL voyageDAL = new VoyageDAL();

        public List<Voyage> GetVoyages()
        {
            return voyageDAL.GetVoyages();
        }

        public Voyage GetVoyageByID(int id)
        {
            return voyageDAL.GetVoyageByID(id);
        }

        public int InsertVoyage(Voyage voyage)
        {
            ValidateVoyage(voyage);
            return voyageDAL.InsertVoyage(voyage);
        }

        public int UpdateVoyage(Voyage voyage)
        {
            ValidateVoyage(voyage);
            return voyageDAL.UpdateVoyage(voyage);
        }

        public int DeleteVoyage(int id)
        {
            return voyageDAL.DeleteVoyage(id);
        }

        public List<Voyage> SearchVoyages(string search)
        {
            return voyageDAL.SearchVoyages(search);
        }

        private void ValidateVoyage(Voyage voyage)
        {
            if (string.IsNullOrWhiteSpace(voyage.VoyageNumber))
                throw new Exception("Voyage Number is required.");

            if (string.IsNullOrWhiteSpace(voyage.VesselName))
                throw new Exception("Vessel Name is required.");

            if (voyage.ETA == default(DateTime))
                throw new Exception("ETA is required.");

            if (voyage.ETD == default(DateTime))
                throw new Exception("ETD is required.");

            if (voyage.ETD < voyage.ETA)
                throw new Exception("ETD cannot be earlier than ETA.");
        }
    }
}