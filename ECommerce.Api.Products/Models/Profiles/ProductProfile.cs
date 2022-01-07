
namespace ECommerce.Api.Products.Models.Profiles 
{
    using AutoMapper;

    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Db.Product,Models.Product>();
        }
    }
        
}