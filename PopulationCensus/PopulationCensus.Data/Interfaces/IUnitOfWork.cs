using PopulationCensus.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationCensus.Data.Interfaces
{
    internal interface IUnitOfWork
    {
        IAsyncRepository<Age> AgeRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
