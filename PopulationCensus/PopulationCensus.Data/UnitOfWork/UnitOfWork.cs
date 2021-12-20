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
        private readonly IAsyncRepository<Gender> _gendersRepository;
        private readonly IAsyncRepository<Year> _yearsRepository;

        public UnitOfWork(PopulationContext context,
            IAsyncRepository<Age> agesRepository,
            IAsyncRepository<Area> areasRepository,
            IAsyncRepository<Ethnicity> ethnicitiesRepository,
            IAsyncRepository<Gender> gendersRepository,
            IAsyncRepository<Year> yearsRepository)
        {
            _context = context;
            _agesRepository = agesRepository;
            _areasRepository = areasRepository;
            _ethnicitiesRepository = ethnicitiesRepository;
            _gendersRepository = gendersRepository;
            _yearsRepository = yearsRepository;
        }

        public IAsyncRepository<Age> AgesRepository => this._agesRepository;

        public IAsyncRepository<Area> AreasRepository => this._areasRepository;

        public IAsyncRepository<Ethnicity> EthnicitiesRepository => this._ethnicitiesRepository;

        public IAsyncRepository<Gender> GendersRepository => this._gendersRepository;

        public IAsyncRepository<Year> YearsRepository => this._yearsRepository;

        public Task<int> SaveChangesAsync() => this._context.SaveChangesAsync();
    }
}
