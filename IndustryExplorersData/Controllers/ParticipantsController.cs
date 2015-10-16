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
using System.Web.Http.Cors;

namespace IndustryExplorersData.Controllers
{
    [EnableCors(origins: "http://industryexplorer.azurewebsites.net", headers: "*", methods: "*")]
    public class ParticipantsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        // GET: api/Participants
        public IQueryable<ParticipantDTO> GetParticipants()
        {
            var participants = from p in db.Participants
                select new ParticipantDTO()
                {
                    participant_id = p.participant_id,
                    first_name = p.first_name,
                    last_name = p.last_name,
                    email = p.email,
                    organization = p.organization,
                    date_created = p.date_created
                };

            return participants;
        }

        [Authorize]
        // GET: api/Participants/5
        [ResponseType(typeof(ParticipantDTO))]
        public async Task<IHttpActionResult> GetParticipant(Guid id)
        {
            Participant participant = await db.Participants.FindAsync(id);
            if (participant == null)
            {
                return NotFound();
            }

            var p = new ParticipantDTO()
            {
                first_name = participant.first_name,
                last_name = participant.last_name,
                email = participant.email,
                organization = participant.organization,
                date_created = participant.date_created
            };
            return Ok(p);
        }

        [Authorize]
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
        [ResponseType(typeof(ParticipantDTO))]
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
            var dto = new ParticipantDTO()
            {
                participant_id = participant.participant_id,
                first_name = participant.first_name,
                last_name = participant.last_name,
                email = participant.email,
                organization = participant.organization,
                date_created = participant.date_created
            };

            return CreatedAtRoute("DefaultApi", new { id = participant.participant_id }, dto);
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