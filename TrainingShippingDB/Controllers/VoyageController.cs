using System;
using System.Collections.Generic;
using System.Web.Http;
using TrainingShippingDB.Models;
using TrainingShippingSystem.Backend.BLL;

namespace TrainingShippingSystem.Backend.Controllers
{
    [RoutePrefix("api/Voyages")]
    public class VoyagesController : ApiController
    {
        private VoyageBLL voyageBLL = new VoyageBLL();

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetVoyages()
        {
            try
            {
                List<Voyage> voyages = voyageBLL.GetVoyages();
                return Ok(voyages);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetVoyageByID(int id)
        {
            try
            {
                Voyage voyage = voyageBLL.GetVoyageByID(id);

                if (voyage == null)
                    return NotFound();

                return Ok(voyage);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult InsertVoyage([FromBody] Voyage voyage)
        {
            try
            {
                int newId = voyageBLL.InsertVoyage(voyage);
                return Ok(new { ID = newId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateVoyage(int id, [FromBody] Voyage voyage)
        {
            try
            {
                voyage.Id = id;
                int rowsAffected = voyageBLL.UpdateVoyage(voyage);

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
        public IHttpActionResult DeleteVoyage(int id)
        {
            try
            {
                int rowsAffected = voyageBLL.DeleteVoyage(id);

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
        public IHttpActionResult SearchVoyages(string term)
        {
            try
            {
                List<Voyage> voyages = voyageBLL.SearchVoyages(term);
                return Ok(voyages);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}