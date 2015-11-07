using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using IndustryExplorersData.Models;

namespace IndustryExplorersData.Controllers
{
    public class PartnersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        // GET: api/Partners
        public IQueryable<Partner> GetPartners()
        {
            return db.Partners;
        }

        [Authorize]
        // GET: api/Partners/5
        [ResponseType(typeof(Partner))]
        public async Task<IHttpActionResult> GetPartner(Guid id)
        {
            Partner partner = await db.Partners.FindAsync(id);
            if (partner == null)
            {
                return NotFound();
            }

            return Ok(partner);
        }

        // PUT: api/Partners/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPartner(Guid id, Partner partner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != partner.Id)
            {
                return BadRequest();
            }

            db.Entry(partner).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartnerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        //// POST: api/Partners
        //[ResponseType(typeof(Partner))]
        //public async Task<IHttpActionResult> PostPartner(Partner partner)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    partner.Id = Guid.NewGuid();
        //    partner.validation_id = Guid.NewGuid();
        //    partner.activated = false;
        //    partner.date_created = DateTime.Today;
        //    db.Partners.Add(partner);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (PartnerExists(partner.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = partner.Id }, partner);
        //}

        // POST: api/Partners
        [ResponseType(typeof(Partner))]
        public async Task<HttpResponseMessage> PostPartner(Partner partner)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
               // return BadRequest(ModelState);
            }
            partner.Id = Guid.NewGuid();
            partner.validation_id = Guid.NewGuid();
            partner.activated = false;
            partner.date_created = DateTime.Today;
            db.Partners.Add(partner);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PartnerExists(partner.Id))
                {
                    throw new Exception();
                    //return Conflict();
                }
                else
                {
                    throw;
                }
            }

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("You have successfully submitted your information to Industry Explorers program.")
            };

            response.Headers.Add("Access-Control-Allow-Origin", "http://industryexplorer.azurewebsites.net");

            return response;
        }

        [Authorize]
        // DELETE: api/Partners/5
        [ResponseType(typeof(Partner))]
        public async Task<IHttpActionResult> DeletePartner(Guid id)
        {
            Partner partner = await db.Partners.FindAsync(id);
            if (partner == null)
            {
                return NotFound();
            }

            db.Partners.Remove(partner);
            await db.SaveChangesAsync();

            return Ok(partner);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PartnerExists(Guid id)
        {
            return db.Partners.Count(e => e.Id == id) > 0;
        }
    }
}