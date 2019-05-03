namespace ChemiDemo.Web.Controllers
{
    using System;
    using System.Net.Http;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using ChemiDemo.DataContext.Entities;
    using ChemiDemo.DataContext.Repositories;
    using Microsoft.AspNetCore.Mvc;

    public class UpdateController 
        : Controller
    {
        private readonly IRepository repository;

        public UpdateController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("Update/{id:int}")]
        public async Task<IActionResult> Index(int id)
        {
            var product = repository.Get<Product>(id);

            await GetAsync(product)
                    .ContinueWith(async x => {
                        var document = new Document() { LastUpdate = DateTime.Now };
                        product.IsUpdated = true;

                        if (x.Status == TaskStatus.RanToCompletion) {
                            document.StatusCode = (int)x.Result.StatusCode;

                            if (x.Result.StatusCode == System.Net.HttpStatusCode.OK) {
                                document.Content = await x.Result.Content.ReadAsByteArrayAsync();
                                document.ContentType = x.Result.Content.Headers.ContentType.MediaType;
                            }
                        }
                        else {
                            document.StatusCode = 408; // Request timeout
                        }

                        if (product.Document != null && document.Content != null) {                         
                            product.IsUpdated = !ByteArrayCompare(product.Document.Content ?? new byte[0], document.Content);

                            if (!product.IsUpdated) {
                                document.LastUpdate = product.Document.LastUpdate;
                            }
                        }

                        product.Document = document;
                        repository.SaveOrUpdate<Product>(product);
                    });

            return NoContent();
        }

        private async Task<HttpResponseMessage> GetAsync(Product product)
        {
            using (HttpClient client = product.Login != null ? new HttpClient(new WebRequestHandler() { Credentials = product.Login }) : new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 0, 20);

                return await client.GetAsync(product.Uri);
            }
        }

        static bool ByteArrayCompare(byte[] b1, byte[] b2) => b1.Length == b2.Length && memcmp(b1, b2, b1.Length) == 0;

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int memcmp(byte[] b1, byte[] b2, long count);
    }
}