using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Priority
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<Incident> Incidents { get; set; }

    }
}
