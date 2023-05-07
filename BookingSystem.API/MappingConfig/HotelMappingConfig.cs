using BookingSystem.Application.Dto;
using BookingSystem.Domin.Entities;
using Mapster;

namespace BookingSystem.API.MappingConfig
{
    public class HotelMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Hotel, HotelDto>()
                .Map(desc => desc.HotelBranches,
                src => src.HotelBranches);
        }
    }
}
