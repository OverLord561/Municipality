using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality.ViewModels
{
    public class AttachedFileViewModel
    {
        public string Name { get; set; }

        public string ContentType { get; set; }
        [JsonIgnore]
        public IFormFile FormFile { get; set; }
    }
}
