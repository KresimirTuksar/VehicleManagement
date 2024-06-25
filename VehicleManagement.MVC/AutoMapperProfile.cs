using AutoMapper;
using VehicleManagement.Service.Models.Entities;
using VehicleManagement.Service.Models.Response;

namespace VehicleManagement.MVC
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            // CreateMap<Source, Destination>();
            CreateMap<VehicleMake, VehicleMakeResponse>();
            CreateMap<VehicleModel, VehicleModelResponse>();
        }
    }
}
