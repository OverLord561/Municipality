﻿using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories.EntityFramework.Repositories
{

    public class IncidentFilesRepository : Repository<IncidentFile>, IIncidentFilesRepository
    {
        public IncidentFilesRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        protected override IQueryable<IncidentFile> Include()
        {
            return base.Include()
                .Include(x => x.Incident);
        }
    }
}
