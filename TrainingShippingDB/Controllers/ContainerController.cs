using System;
using System.Collections.Generic;
using System.Web.Http;
using TrainingShippingDB.Models;
using TrainingShippingSystem.Backend.BLL;

namespace TrainingShippingSystem.Backend.Controllers
{
    [RoutePrefix("api/Containers")]
    public class ContainersController : ApiController
    {
        private ContainerBLL containerBLL = new ContainerBLL();

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetContainers()
        {
            try
            {
                List<Container> containers = containerBLL.GetContainers();
                return Ok(containers);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetContainerByID(int id)
        {
            try
            {
                Container container = containerBLL.GetContainerByID(id);

                if (container == null)
                    return NotFound();

                return Ok(container);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult InsertContainer([FromBody] Container container)
        {
            try
            {
                int newId = containerBLL.InsertContainer(container);
                return Ok(new { ID = newId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateContainer(int id, [FromBody] Container container)
        {
            try
            {
                container.Id = id;
                int rowsAffected = containerBLL.UpdateContainer(container);

                if (rowsAffected == 0)
                    return NotFound();

                return Ok(new { RowsAffected = rowsAffected });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteContainer(int id)
        {
            try
            {
                int rowsAffected = containerBLL.DeleteContainer(id);

                if (rowsAffected == 0)
                    return NotFound();

                return Ok(new { RowsAffected = rowsAffected });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("search")]
        public IHttpActionResult SearchContainers(string term)
        {
            try
            {
                List<Container> containers = containerBLL.SearchContainers(term);
                return Ok(containers);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}   