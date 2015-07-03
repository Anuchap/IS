using Data.Repositories;

namespace Data
{
    public class UnitOfWork
    {
        private readonly Context _context;
        private MonitorRepo _monitorRepo;

        public UnitOfWork(Context context)
        {
            _context = context;
        }

        public MonitorRepo MonitorRepo
        {
            get { return _monitorRepo ?? (_monitorRepo = new MonitorRepo(_context)); }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}