namespace ChemiDemo.Web.Infrastructure
{
    using AutoMapper;
    using ChemiDemo.Web.Models;
    using Entity = DataContext.Entities;

    public class ProductProfile
        : Profile
    {
        public ProductProfile()
        {
            CreateMap<Entity.Product, Product.Row>();

            CreateMap<Entity.Product, Product.Create>()
                .ForMember(x => x.UserName, x => x.MapFrom(z => z.Login.UserName))
                .ForMember(x => x.UserName, x => x.MapFrom(z => z.Login.Password));

            CreateMap<Entity.Product, Product.Edit>()
                .ForMember(x => x.UserName, x => x.MapFrom(z => z.Login.UserName))
                .ForMember(x => x.UserName, x => x.MapFrom(z => z.Login.Password));

            CreateMap<Product.Create, Entity.Product>();

            CreateMap<Product.Edit, Entity.Product>();
        }
    }
}