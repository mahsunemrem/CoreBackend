using AutoMapper;
using Core.Entities.Concrete;
using Entities.Dtos;

namespace Business.AutoMapperProfile
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {

            CreateMap<User, UserForRegisterDto>();
            CreateMap<UserForRegisterDto, User>();

        }
    }
}