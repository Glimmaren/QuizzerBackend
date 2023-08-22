using PersonSevice.Intefaces;
using PersonSevice.Repositories;

namespace PersonSevice.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PersonContext _context;
        public UnitOfWork(PersonContext context)
        {
            _context = context;
        }

        public IPersonRepository PersonRepository => new PersonRepository(_context);
        public PersonContext Context => _context;

        public async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            throw new NotImplementedException();
        }
    }
}
