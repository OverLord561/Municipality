using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class IncidentFile
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public string FilePath { get; set; }

        public long IncidentId { get; set; }

        public virtual Incident Incident { get; set; }

        public int UploadedById { get; set; }

        public virtual ApplicationUser UploadedBy { get; set; }

        public DateTime Date { get; set; }
    }
}
