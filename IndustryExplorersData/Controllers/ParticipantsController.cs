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

        // POST: api/Participants/apply             
        [HttpPost]
        public async Task<HttpResponseMessage> Apply(HttpRequestMessage request)
        {
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var data = await Request.Content.ParseMultipartAsync();


            IndustryExplorersData.Models.Participant participant = new IndustryExplorersData.Models.Participant();

            participant.ParticipantID = Guid.NewGuid();
            Guid valid_id = Guid.NewGuid();
            participant.ValidationID = valid_id;
            participant.Activated = false;
            participant.DateCreated = DateTime.Today;


            if (data.Fields.ContainsKey("AuthorizedToWork"))
            {
                string authorized = data.Fields["AuthorizedToWork"].Value;
                participant.AuthorizedToWork = Convert.ToBoolean(authorized);                
            }
            if (data.Fields.ContainsKey("FirstName"))
            {
                participant.FirstName = data.Fields["FirstName"].Value;
            }
            if (data.Fields.ContainsKey("LastName"))
            {
                participant.LastName = data.Fields["LastName"].Value;
            }
            if (data.Fields.ContainsKey("Email"))
            {
                participant.Email = data.Fields["Email"].Value;
            }
            if (data.Fields.ContainsKey("Phone"))
            {
                participant.Phone = data.Fields["Phone"].Value;
            }

            if (data.Fields.ContainsKey("StreetAddress"))
            {
                participant.StreetAddress = data.Fields["StreetAddress"].Value;
            }
            if (data.Fields.ContainsKey("City"))
            {
                participant.City = data.Fields["City"].Value;
            }
            if (data.Fields.ContainsKey("State"))
            {              
                participant.State = data.Fields["State"].Value;
            }
            if (data.Fields.ContainsKey("Postalcode"))
            {
                participant.Postalcode = data.Fields["Postalcode"].Value;
            }

            if (data.Fields.ContainsKey("Question1"))
            {
                participant.Question1 = data.Fields["Question1"].Value;
            }
            if (data.Fields.ContainsKey("Question2"))
            {
                participant.Question2 = data.Fields["Question2"].Value;
            }
            if (data.Fields.ContainsKey("Question3"))
            {
                participant.Question3 = data.Fields["Question3"].Value;
            }

            if (data.Files.ContainsKey("Resume"))
            {
                participant.Resume = data.Files["Resume"].File;
                participant.ResumeName = data.Files["Resume"].Filename;
            }

            try
            {
                db.Participants.Add(participant);
            }
            catch (Exception)
            {                 
                throw new HttpResponseException(HttpStatusCode.NotModified);
            }

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ParticipantExists(participant.ParticipantID))
                {
                    return new HttpResponseMessage(HttpStatusCode.Conflict)
                    {
                        Content = new StringContent("Applicant already exists in the system.")
                    }; 
                }
                else
                {
                    throw;
                }
            }
            catch(Exception)
            {                
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            //TODO: Send confirmation email with proper credentials
            //SendConfirmationEMail(participant.Email);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Thank you for applying. You have successfully submitted your application to Industry Explorers program.")
            };

            response.Headers.Add("Access-Control-Allow-Origin", "http://industryexplorer.azurewebsites.net");
            return response;
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