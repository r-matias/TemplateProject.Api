using AutoMapper;
using TemplateProject.Entities.Model;
using TemplateProject.Models.ViewModel.UserViewModel;

namespace TemplateProject.Utilities.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserUpdateViewModel>().ReverseMap();
            CreateMap<User, UserCreateViewModel>().ReverseMap();
            CreateMap<User, UserGetIdViewModel>();
            CreateMap<User, UserGetListViewModel>();
                //.ForMember(c => c.Location, opt => opt.MapFrom(y => y.Location.GetEnumDisplayName()))
                //.ForMember(c => c.PvpType, opt => opt.MapFrom(y => y.PvpType.GetEnumDisplayName()));
        }
    }
}
