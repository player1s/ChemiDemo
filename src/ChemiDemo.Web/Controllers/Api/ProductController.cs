namespace ChemiDemo.Web.Controllers.Api
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.OData;
    using AutoMapper;
    using ChemiDemo.DataContext.Repositories;
    using ChemiDemo.Web.Models;
    using ChemiDemo.Web.Utility;
    using Microsoft.AspNetCore.Mvc;
    using NHibernate.Linq;

    using Entity = DataContext.Entities;

    public class ProductController 
        : Controller
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public ProductController(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("Api/Products")]
        public IActionResult List([FromQuery]int page = 0, [FromQuery]int size = 100)
        {
            var count = repository
                            .Query<Entity.Product>()
                            .ToFutureValue(x => x.Count());

            var result = repository
                            .Query<Entity.Product>()
                            .Skip(page)
                            .Take(size)
                            .ToFuture();

            //new PageResult<Product.Row> ... , null, count.Value ----- removed these parts to fit the json format

            return Ok((result.Select(x => mapper.Map<Product.Row>(x))));
        }

        [HttpGet("Api/Product/{id:int}")]
        public IActionResult Get(int id)
        {
            var item = GetProduct(id);

            if (item == null) {
                return NotFound();
            }

            string wholeListInJson = MyApp.Namespace.IndexModel.Get("http://localhost:64888/Api/Products");
            List<ChemiDemo.Web.Models.Product.Row> products = ChemiDemo.Web.Utility.JsonHandler.deSerializeProductsToList(wholeListInJson);
            Product.Row product = null;

            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Id == id)
                    product = products[i];
            }

            Downloader.Download(product.Uri, product.Name);

            return Ok(mapper.Map<Product.Edit>(item));
        }

        [HttpPut("Api/Product/{id:int}")]
        public IActionResult Save(int id, [FromBody]Product.Edit model)
        {
            if (ModelState.IsValid)
            {
                var item = GetProduct(id);

                if (item == null) {
                    return NotFound();
                }

                mapper.Map(model, item);

                repository.SaveOrUpdate(item);

                return Ok(mapper.Map<Product.Edit>(item));
            }

            return BadRequest(ModelState);
        }

        [HttpPost("Api/Product")]
        public IActionResult Create([FromBody]Product.Create model)
        {
            if (ModelState.IsValid) {
                var item = mapper.Map(model, new Entity.Product());

                repository.SaveOrUpdate(item);

                return Created($"Api/Product/{item.Id}", mapper.Map<Product.Edit>(item));
            }

            return BadRequest(ModelState);                       
        }

        [HttpDelete("Api/Product/{id:int}")]
        public IActionResult Delete(int id)
        {
            var item = GetProduct(id);

            if (item == null) {
                return NotFound();
            }

            repository.Delete(item);

            return Ok(id);
        }

        [NonAction]
        private Entity.Product GetProduct(int id) => repository.Get<Entity.Product>(id);
    }
}