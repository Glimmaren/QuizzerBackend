using AutoMapper;
using PersonSevice.ViewModels.PersonViewModels;
using PersonService.Models;

namespace PersonSevice.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //PersonMapping
            CreateMap<PostPersonViewModel, Person>();
            CreateMap<Person, PostPersonViewModel>();

            CreateMap<PatchPersonViewModel, Person>();
            CreateMap<Person, PatchPersonViewModel>();
        }
    }
}
