using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality.ViewModels
{
    public class CreateIncidentViewModel
    {
       
        public string Title { get; set; }
        public string Description { get; set; }
        public double Lng { get; set; }
        public double Lat { get; set; }
        public FormFile File { get; set; }
    }
}

