using Microsoft.EntityFrameworkCore;
using PersonSevice.Data;
using PersonSevice.Intefaces;
using PersonService.Models;

namespace PersonSevice.Repositories
{
    
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonContext _context;

        public PersonRepository(PersonContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Person person)
        {
            try
            {
                await _context.Persons.AddAsync(person);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<Person> FindByEmail(string email) => await _context.Persons.FirstOrDefaultAsync(c => c.Email == email);

        public async Task<Person> FindById(int id) => await _context.Persons.FirstOrDefaultAsync(c => c.Id == id);

        public async Task<IList<Person>> GetAll() => await _context.Persons.ToListAsync();

        public bool Remove(Person person)
        {
            try
            {
                _context.Remove(person);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Person person)
        {
            try
            {
                _context.Update(person);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
