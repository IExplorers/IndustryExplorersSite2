using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using IndustryExplorersData.Extensions;
using IndustryExplorersData.Models;
using System.Data.Entity.Infrastructure;
using IndustryExplorersData.Controllers;

namespace IndustryExplorersData.Controllers
{
    public class FileUploadController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/FileUpload
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/FileUpload/5
        public string Get(int id)
        {
            return "value";
        }

     


        //POST: api/FileUpload/UploadFile
       //[HttpPost]
       // public void UploadFile()
       // {

       //     HttpRequestMessage request = this.Request;
       //     if (!request.Content.IsMimeMultipartContent())
       //     {
       //         throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
       //     }

       //     string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/uploads");
       //     var provider = new MultipartFormDataStreamProvider(root);

       //     //var task = request.Content.ReadAsMultipartAsync(provider).
       //     //    ContinueWith<HttpResponseMessage>(o =>
       //     //    {

       //     //        string file1 = provider.BodyPartFileNames.First().Value;
       //     //// this is the file name on the server where the file was saved 

       //     //return new HttpResponseMessage()
       //     //        {
       //     //            Content = new StringContent("File uploaded.")
       //     //        };
       //     //    }
       //     //);
           


       //     //if (HttpContext.Current.Request.Files.AllKeys.Any())
       //     //{
       //     //    // Get the uploaded image from the Files collection
       //     //    var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];

       //     //    if (httpPostedFile != null)
       //     //    {
       //     //        // Validate the uploaded image(optional)

       //     //        // Get the complete file path
       //     //        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), httpPostedFile.FileName);

       //     //        // Save the uploaded file to "UploadedFiles" folder
       //     //        httpPostedFile.SaveAs(fileSavePath);
       //     //    }
       //     //}
       // }


        
        [HttpPost]
        public async void UploadFile(HttpRequestMessage request)
        {
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var data = await Request.Content.ParseMultipartAsync();


            Participant participant = new Participant();
          
            participant.ParticipantID = Guid.NewGuid();
            Guid valid_id = Guid.NewGuid();
            participant.ValidationID = valid_id;
            participant.Activated = false;
            participant.DateCreated = DateTime.Today;


            //if (data.Fields.ContainsKey("AuthorizedToWork"))
            //{
            //    participant.AuthorizedToWork = data.Fields["AuthorizedToWork"].Value;
            //}
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
                participant.LastName = data.Fields["Phone"].Value;
            }

            if (data.Fields.ContainsKey("StreetAddress"))
            {
                participant.FirstName = data.Fields["StreetAddress"].Value;
            }
            if (data.Fields.ContainsKey("City"))
            {
                participant.LastName = data.Fields["City"].Value;
            }
            if (data.Fields.ContainsKey("Postalcode"))
            {
                participant.Email = data.Fields["Postalcode"].Value;
            }

            if (data.Fields.ContainsKey("Question1"))
            {
                participant.LastName = data.Fields["Question1"].Value;
            }
            if (data.Fields.ContainsKey("Question2"))
            {
                participant.LastName = data.Fields["Question2"].Value;
            }
            if (data.Fields.ContainsKey("Question3"))
            {
                participant.LastName = data.Fields["Question3"].Value;
            }

            if (data.Files.ContainsKey("Resume"))
            {
                //participant.Resume = data.Files["Resume"].File;
                //participant.ResumeName = data.Files["Resume"].Filename;
            }

            db.Participants.Add(participant);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //if (ParticipantExists(participant.ParticipantID))
                //{
                //    return Conflict();
                //}
                //else
                //{
                //    throw;
                //}
            }
            //return new HttpResponseMessage(HttpStatusCode.OK)
            //{
            //    Content = new StringContent("Thank you for uploading the file...")
            //};
        }    


        // PUT: api/FileUpload/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FileUpload/5
        public void Delete(int id)
        {
        }
    }
}
