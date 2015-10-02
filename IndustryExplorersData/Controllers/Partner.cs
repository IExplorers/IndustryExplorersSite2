using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IndustryExplorersData.Models
{
    public class Partner
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string organization_name { get; set; }
        [Required]
        public string contact_name { get; set; }
        [Required]
        public string email { get; set; }
        public string website { get; set; }
        public DateTime date_created { get; set; }
        public Guid validation_id { get; set; }
        public Boolean activated { get; set; }
    }

}