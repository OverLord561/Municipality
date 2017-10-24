using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Incident
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public string Adress { get; set; }

        public bool Approved { get; set; }

        public string FilePath { get; set; }

        //relationships
        public int IncidentStatusId { get; set; }
        public virtual IncidentStatus IncidentStatus { get; set; }


        public int UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
