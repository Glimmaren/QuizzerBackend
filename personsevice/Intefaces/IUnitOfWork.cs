using PersonSevice.Data;

namespace PersonSevice.Intefaces
{
    public interface IUnitOfWork
    {
        IPersonRepository PersonRepository { get; }
        PersonContext Context { get; }
        Task<bool> CompleteAsync();
        bool HasChanges();
    }
}
