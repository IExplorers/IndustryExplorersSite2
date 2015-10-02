using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IndustryExplorersData.Models
{
    public enum Role { admin, user}

    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string alias { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public Role role { get; set; }
        public bool active { get; set; }
        public Guid validation_token { get; set; }
        public DateTime date_created { get; set; }
        public DateTime date_updated { get; set; }

    }
}