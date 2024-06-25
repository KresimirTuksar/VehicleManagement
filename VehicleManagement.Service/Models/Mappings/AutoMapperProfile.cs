using AutoMapper;
using VehicleManagement.Service.Models.Entities;
using VehicleManagement.Service.Models.Response;

namespace VehicleManagement.Service.Models.Mappings
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {

            // CreateMap<Source, Destination>();
            CreateMap<VehicleMake, VehicleMakeResponse>();
            CreateMap<VehicleModel, VehicleModelResponse>();

            CreateMap<VehicleMake, VehicleMakeWithModels>()
            .ForMember(dest => dest.Make, opt => opt.MapFrom(src => new VehicleMakeResponse
            {
                Name = src.Name,
                Abrv = src.Abrv,
                // Add any other properties you want to map
            }))
            .ForMember(dest => dest.Models, opt => opt.MapFrom(src => src.Models.Select(m => new VehicleModelResponse
            {
                Name = m.Name,
                Abrv = m.Abrv,
                // Add any other properties you want to map for VehicleModelResponse
            })));
        }
    }
}
