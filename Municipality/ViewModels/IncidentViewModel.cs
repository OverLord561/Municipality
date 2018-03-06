using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality.ViewModels
{
    public class IncidentViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Lng { get; set; }
        public double Lat { get; set; }
        public long StatusId { get; set; }
        public string Status { get; set; }
        public string Adress { get; set; }
        public bool InFocus { get; set; }
        public bool Approved { get; set; }
        public long PriorityId { get; set; }
        public string Priority { get; set; }
        public double Estimate { get; set; }
        public string TimeLeft { get; set; }
        public ICollection<AttachedFileViewModel> AttachedFiles { get; set; }


    }
}
