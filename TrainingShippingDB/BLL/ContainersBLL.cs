using System;
using System.Collections.Generic;
using TrainingShippingDB.Models;
using TrainingShippingSystem.Backend.DAL;

namespace TrainingShippingSystem.Backend.BLL
{
    public class ContainerBLL
    {
        private ContainerDAL containerDAL = new ContainerDAL();

        public List<Container> GetContainers()
        {
            return containerDAL.GetContainers();
        }

        public Container GetContainerByID(int id)
        {
            return containerDAL.GetContainerByID(id);
        }

        public int InsertContainer(Container container)
        {
            ValidateContainer(container);
            return containerDAL.InsertContainer(container);
        }

        public int UpdateContainer(Container container)
        {
            ValidateContainer(container);
            return containerDAL.UpdateContainer(container);
        }

        public int DeleteContainer(int id)
        {
            return containerDAL.DeleteContainer(id);
        }

        public List<Container> SearchContainers(string search)
        {
            return containerDAL.SearchContainers(search);
        }

        private void ValidateContainer(Container container)
        {
            if (string.IsNullOrWhiteSpace(container.ContainerNumber))
                throw new Exception("Container Number is required.");

            if (string.IsNullOrWhiteSpace(container.ContainerType))
                throw new Exception("Container Type is required.");

            if (container.BillID <= 0)
                throw new Exception("Bill is required.");
        }
    }
}