using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrainingShippingDB.Models;
using TrainingShippingSystem.Backend.BLL;

namespace TrainingShippingSystem.Backend.Controllers
{
    [RoutePrefix("api/Clients")]
    public class ClientsController : ApiController
    {
        private ClientBLL clientBLL = new ClientBLL();

        // GET api/Clients
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetClients()
        {
            try
            {
                List<Client> clients = clientBLL.GetClients();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/Clients/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetClientByID(int id)
        {
            try
            {
                Client client = clientBLL.GetClientByID(id);

                if (client == null)
                    return NotFound();

                return Ok(client);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/Clients
        [HttpPost]
        [Route("")]
        public IHttpActionResult InsertClient([FromBody] Client client)
        {
            try
            {
                int newId = clientBLL.InsertClient(client);
                return Ok(new { ID = newId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/Clients/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateClient(int id, [FromBody] Client client)
        {
            try
            {
                client.ID = id;
                int rowsAffected = clientBLL.UpdateClient(client);

                if (rowsAffected == 0)
                    return NotFound();

                return Ok(new { RowsAffected = rowsAffected });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/Clients/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteClient(int id)
        {
            try
            {
                int rowsAffected = clientBLL.DeleteClient(id);

                if (rowsAffected == 0)
                    return NotFound();

                return Ok(new { RowsAffected = rowsAffected });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/Clients/search?term=xxx
        [HttpGet]
        [Route("search")]
        public IHttpActionResult SearchClients(string term)
        {
            try
            {
                List<Client> clients = clientBLL.SearchClients(term);
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}