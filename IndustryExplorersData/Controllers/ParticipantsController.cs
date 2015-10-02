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
  
    public class ParticipantsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        // GET: api/Participants
        public IQueryable<Participant> GetParticipants()
        {
            return db.Participants;
        }

        [Authorize]
        // GET: api/Participants/5
        [ResponseType(typeof(Participant))]
        public async Task<IHttpActionResult> GetParticipant(Guid id)
        {
            Participant participant = await db.Participants.FindAsync(id);
            if (participant == null)
            {
                return NotFound();
            }

            return Ok(participant);
        }

        // PUT: api/Participants/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutParticipant(Guid id, Participant participant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != participant.participant_id)
            {
                return BadRequest();
            }

            db.Entry(participant).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(id))
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

        // POST: api/Participants
        [ResponseType(typeof(Participant))]
        public async Task<IHttpActionResult> PostParticipant(Participant participant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            participant.participant_id = Guid.NewGuid();
            Guid valid_id = Guid.NewGuid();
            participant.validation_id = valid_id;
            participant.activated = false;
            participant.date_created = DateTime.Today;

            db.Participants.Add(participant);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ParticipantExists(participant.participant_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = participant.participant_id }, participant);
        }

        [Authorize]
        // DELETE: api/Participants/5
        [ResponseType(typeof(Participant))]
        public async Task<IHttpActionResult> DeleteParticipant(Guid id)
        {
            Participant participant = await db.Participants.FindAsync(id);
            if (participant == null)
            {
                return NotFound();
            }

            db.Participants.Remove(participant);
            await db.SaveChangesAsync();

            return Ok(participant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParticipantExists(Guid id)
        {
            return db.Participants.Count(e => e.participant_id == id) > 0;
        }
    }
}