using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace IndustryExplorersData.Models
{
    public class Participant
    {
        [Key]
        public Guid participant_id { get; set; }       
        public Guid validation_id { get; set; }       
        public bool activated { get; set; }
        public DateTime date_created { get; set; }
        [Required]
        public string first_name { get; set; }        
        [Required]
        public string last_name { get; set; }
        [Required]
        public string email { get; set; }
        public string organization { get; set; }

    }
}