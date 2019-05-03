namespace ChemiDemo.Web
{
    using System.Linq;
    using AutoMapper;
    using ChemiDemo.DataContext.Entities;
    using ChemiDemo.DataContext.Repositories;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using NHibernate.Dialect;
    using NHibernate.Driver;

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<NHibernate.ISessionFactory>(factory => {
                return Fluently
                            .Configure()
                            .Database(() => {
                                var connection = SQLiteConfiguration.Standard.Dialect<SQLiteDialect>()
                                                    .Driver<SQLite20Driver>()
                                                    .ShowSql()
                                                    .ConnectionString("Data Source=data.sqlite;Version=3;New=True;");

                                return connection;
                            })
                            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Product>())                            
                            .BuildSessionFactory();
            });

            services.AddScoped<NHibernate.ISession>(factory => {                
                var service = factory
                                .GetServices<NHibernate.ISessionFactory>()
                                .First();
                
                return service.OpenSession();
            });

            services.AddTransient<IRepository, NHibernateRepository>();

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            
            app
                .UseStaticFiles()
                .UseMvcWithDefaultRoute();
        }
    }
}