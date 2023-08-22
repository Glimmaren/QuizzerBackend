using System.ComponentModel.DataAnnotations;

namespace PersonService.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        //public int[]? FrindsId { get; set; }
        // TODO: Lägg till vänner lista
    }
}
