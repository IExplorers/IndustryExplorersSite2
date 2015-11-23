using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace IndustryExplorersData.Models
{
    public class Participant
    {
        [Key]
        public Guid ParticipantID { get; set; }     
          
        public Guid ValidationID { get; set; }   
            
        public bool Activated { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }    
            
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(120)]
        public string Organization { get; set; }

        [Required]
        [MaxLength(25)]
        public string Phone { get; set; }

        [Required]
        public bool AuthorizedToWork { get; set; }

        [Required]
        [MaxLength(120)]
        public string StreetAddress { get; set; }

        [Required]
        [MaxLength(30)]
        public string City { get; set; }

        [Required]
        [MaxLength(2), MinLength(2)]
        public string State { get; set; }

        [Required]
        [MaxLength(15)]
        public string Postalcode { get; set; }

        [Required]       
        public string Question1 { get; set; }

        [Required]
        public string Question2 { get; set; }        

        [Required]
        public string ResumeUrl { get; set; }        

    }
}