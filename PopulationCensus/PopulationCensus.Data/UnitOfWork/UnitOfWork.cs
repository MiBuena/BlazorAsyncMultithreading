using PopulationCensus.Data.DB;
using PopulationCensus.Data.Entities;
using PopulationCensus.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationCensus.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PopulationContext _context;
        private readonly IAsyncRepository<Age> _agesRepository;

        public UnitOfWork(PopulationContext context,
            IAsyncRepository<Age> agesRepository)
        {
            _context = context;
            _agesRepository = agesRepository;
        }

        public IAsyncRepository<Age> AgeRepository => this._agesRepository;

        public Task<int> SaveChangesAsync() => this._context.SaveChangesAsync();
    }
}
