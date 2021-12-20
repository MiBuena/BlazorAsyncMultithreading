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
        private readonly IAsyncRepository<Area> _areasRepository;
        private readonly IAsyncRepository<Ethnicity> _ethnicitiesRepository;

        public UnitOfWork(PopulationContext context,
            IAsyncRepository<Age> agesRepository,
            IAsyncRepository<Area> areasRepository,
            IAsyncRepository<Ethnicity> ethnicityRepository)
        {
            _context = context;
            _agesRepository = agesRepository;
            _areasRepository = areasRepository;
            _ethnicitiesRepository = ethnicityRepository;
        }

        public IAsyncRepository<Age> AgesRepository => this._agesRepository;

        public IAsyncRepository<Area> AreasRepository => this._areasRepository;

        public IAsyncRepository<Ethnicity> EthnicitiesRepository => this._ethnicitiesRepository;

        public Task<int> SaveChangesAsync() => this._context.SaveChangesAsync();
    }
}
