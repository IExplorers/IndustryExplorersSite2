using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndustryExplorersData.Models
{
    public class ParticipantDTO
    {
            public Guid participant_id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string email { get; set; }
            public string organization { get; set; }
            public DateTime date_created { get; set; }
    
    }
}