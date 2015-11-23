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
using IndustryExplorersData.Extensions;
using System.Net.Mail;
using System.Web.Http.Cors;

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

            if (id != participant.ParticipantID)
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
            participant.ParticipantID = Guid.NewGuid();
            Guid valid_id = Guid.NewGuid();
            participant.ValidationID = valid_id;
            participant.Activated = false;
            participant.DateCreated = DateTime.Today;

            db.Participants.Add(participant);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ParticipantExists(participant.ParticipantID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = participant.ParticipantID }, participant);
        }


        private void SendConfirmationEMail(string toEmailAddress)
        {

            string smtpAddress = "smtp.gmail.com";
            int portNumber = 587;
            bool enableSSL = true;

            string emailFrom = "explorers@microsoft.com";          
            string password = "abcdefg";
            string emailTo = toEmailAddress;
            string subject = "Industry Explorers Program";
            string body = "Please do not respond to this email. This is an automated response to thank you "
                + "for applying to the Industry Explorers’ Program.  We will review your application and "
                + "contact you regarding the status of your submission.<br/>"
                + "The Industry Explorers Team";          

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Bcc.Add(emailFrom);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                            

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {                    
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                   
                    try
                    {
                        smtp.Send(mail);
                    }
                    catch (SmtpFailedRecipientsException ex)
                    {
                        for (int i = 0; i < ex.InnerExceptions.Length; i++)
                        {
                            SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                            if (status == SmtpStatusCode.MailboxBusy ||
                                status == SmtpStatusCode.MailboxUnavailable)
                            {
                                System.Threading.Thread.Sleep(5000);
                                smtp.Send(mail);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }          
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
            return db.Participants.Count(e => e.ParticipantID == id) > 0;
        }
    }
}