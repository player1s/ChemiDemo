namespace ChemiDemo.DataContext.Mappings
{    
    using ChemiDemo.DataContext.Entities;
    using FluentNHibernate;
    using FluentNHibernate.Mapping;

    internal class ProductMap
        : ClassMap<Product>
    {
        public ProductMap()
        {
            Table("tblProduct");

            Id(x => x.Id)
                .GeneratedBy.Identity()
                .UnsavedValue(0);
            
            Map(x => x.Uri, "Uri")
                .Length(200)
                .Nullable();

            Map(x => x.Name, "ProductName")
                .Length(250)
                .Nullable();

            Map(x => x.Supplier, "SupplierName")
                .Length(250)
                .Nullable();

            Map(x => x.IsUpdated)
                .Default("0")
                .Not.Nullable();

            Map(Reveal.Member<Product>("_username"), "UserName")
                .Length(250)
                .Nullable();

            Map(Reveal.Member<Product>("_password"), "Password")
                .Length(250)
                .Nullable();

            Component(x => x.Document, d => {
                d.Map(z => z.Content)
                    .Nullable();

                d.Map(z => z.ContentType)
                    .Nullable()
                    .Length(100);

                d.Map(z => z.StatusCode)
                    .Nullable();

                d.Map(z => z.LastUpdate)
                    .Nullable();

            }).LazyLoad();
        }
    }
}
