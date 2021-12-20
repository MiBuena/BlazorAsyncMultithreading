﻿using PopulationCensus.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationCensus.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IAsyncRepository<Age> AgesRepository { get; }
        IAsyncRepository<Area> AreasRepository { get; }
        IAsyncRepository<Ethnicity> EthnicitiesRepository { get; }

        IAsyncRepository<Gender> GenderRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
