using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndustryExplorersData.Models
{
    public class PartnerDTO
    {
        public Guid Id { get; set; }        
        public string organization_name { get; set; }        
        public string contact_name { get; set; }        
        public string email { get; set; }
        public string website { get; set; }
        public DateTime date_created { get; set; }
    }
}