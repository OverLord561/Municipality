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

        //relationships
        public int IncidentStatusId { get; set; }
        public virtual IncidentStatus IncidentStatus { get; set; }

    }
}
