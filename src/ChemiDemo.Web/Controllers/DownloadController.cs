namespace ChemiDemo.Web.Controllers
{
    using ChemiDemo.DataContext.Entities;
    using ChemiDemo.DataContext.Repositories;
    using Microsoft.AspNetCore.Mvc;

    public class DownloadController
        : Controller
    {
        private readonly IRepository repository;

        public DownloadController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("Download/{id:int}")]
        public IActionResult GetDocument(int id)
        {
            var product = repository.Get<Product>(id);

            if (product?.Document != null) {
                return File(product.Document.Content, product.Document.ContentType);
            }

            return NotFound();
        }
    }
}