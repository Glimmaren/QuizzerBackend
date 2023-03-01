using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonSevice.Intefaces;
using PersonSevice.ViewModels.PersonViewModels;
using PersonService.Models;

namespace PersonSevice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson(PostPersonViewModel model)
        {
            var person = _mapper.Map<Person>(model);
            if (await _unitOfWork.PersonRepository.Add(person))
                if (await _unitOfWork.CompleteAsync())
                    return StatusCode(201, person);

            return StatusCode(500, "Something went wrong adding new user");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var persons = await _unitOfWork.PersonRepository.GetAll();
            if (persons.Count < 1)
                return NotFound("Could not find any users");

            return StatusCode(201, persons);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var person = await _unitOfWork.PersonRepository.FindById(id);
            if (person == null)
                return NotFound($"Could not find any user with id: {id}");

            return StatusCode(201, person);
        }

        [HttpGet("email")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var person = await _unitOfWork.PersonRepository.FindByEmail(email);
            if(person == null)
                return NotFound($"Could not find any user with email: {email}");

            return StatusCode(201, person);
        }

        [HttpPatch("id")]
        public async Task<IActionResult> Update(int id, PatchPersonViewModel model)
        {
            var personToUpdate = await _unitOfWork.PersonRepository.FindById(id);
            if (personToUpdate == null)
                return NotFound($"Could not find a person with id: {id} to update");

            personToUpdate.FirstName = model.FirstName;
            personToUpdate.LastName = model.LastName;
            personToUpdate.Email = model.Email;
            personToUpdate.Password = model.Password;
            //personToUpdate.FrindsId = model.FrindsId;

            if (_unitOfWork.PersonRepository.Update(personToUpdate))
                if (await _unitOfWork.CompleteAsync())
                    return StatusCode(201, personToUpdate);

            return StatusCode(500, "Could not update");
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _unitOfWork.PersonRepository.FindById(id);
            if (person == null)
                return NotFound($"Could not find a person with id: {id} to delete");

            return StatusCode(201, "Deleted");
        }

    }
}
