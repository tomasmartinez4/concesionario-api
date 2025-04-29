using AutoMapper;
using ConcesionarioApi.DTOs;
using ConcesionarioApi.Models;

namespace ConcesionarioApi.Profiles
{
    public class AutoProfile : Profile
    {
        public AutoProfile() 
        {
            CreateMap<Auto, AutoDto>();
            CreateMap<CreateAutoDto, Auto>();
            CreateMap<UpdateAutoDto, Auto>();
        }
    }
}
