using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality.ViewModels
{
    public class IncidentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Lng { get; set; }
        public double Lat { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public string Adress { get; set; }
        public bool InFocus { get; set; }
        public bool Approved { get; set; }
        public string FilePath { get; set; }
        public int PriorityId { get; set; }
        public string Priority { get; set; }
        public double Estimate { get; set; }
        public string TimeLeft { get; set; }

    }
}
