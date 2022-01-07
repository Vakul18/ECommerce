namespace ECommerce.Api.Customers.Models.Profiles
{
    using AutoMapper;

    internal class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Db.Customer,Models.Customer>();
        }
    }
}