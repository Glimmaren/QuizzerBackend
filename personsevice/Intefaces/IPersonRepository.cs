using PersonService.Models;

namespace PersonSevice.Intefaces
{
    public interface IPersonRepository
    {
        Task<bool> Add(Person person);
        bool Update(Person person);
        bool Remove(Person person);
        Task<IList<Person>> GetAll();
        Task<Person> FindById(int id);
        Task<Person> FindByEmail(string Email);

    }
}
