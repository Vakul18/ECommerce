
namespace ECommerce.Api.Orders.Models.Profiles
{
    using AutoMapper;

    internal class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Db.Order,Models.Order>();
            CreateMap<Db.OrderItem,Models.OrderItem>();
        }        
    }
}