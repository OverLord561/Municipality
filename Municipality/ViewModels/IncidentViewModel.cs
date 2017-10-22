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
    }
}
