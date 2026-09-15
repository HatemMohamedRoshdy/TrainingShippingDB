using System;
using System.Collections.Generic;
using System.Web.Http;
using TrainingShippingDB.Models;
using TrainingShippingSystem.Backend.BLL;

namespace TrainingShippingSystem.Backend.Controllers
{
    [RoutePrefix("api/Bills")]
    public class BillsController : ApiController
    {
        private BillBLL billBLL = new BillBLL();

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetBills()
        {
            try
            {
                List<Bill> bills = billBLL.GetBills();
                return Ok(bills);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetBillByID(int id)
        {
            try
            {
                Bill bill = billBLL.GetBillByID(id);

                if (bill == null)
                    return NotFound();

                return Ok(bill);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult InsertBill([FromBody] Bill bill)
        {
            try
            {
                int newId = billBLL.InsertBill(bill);
                return Ok(new { ID = newId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateBill(int id, [FromBody] Bill bill)
        {
            try
            {
                bill.Id = id;
                int rowsAffected = billBLL.UpdateBill(bill);

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
        public IHttpActionResult DeleteBill(int id)
        {
            try
            {
                int rowsAffected = billBLL.DeleteBill(id);

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
        public IHttpActionResult SearchBills(string term)
        {
            try
            {
                List<Bill> bills = billBLL.SearchBills(term);
                return Ok(bills);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}