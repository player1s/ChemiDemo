namespace ChemiDemo.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public class Row
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Supplier { get; set; }

            public bool IsUpdated { get; set; }

            public string Uri { get; set; }
        }

        public class Create
        {
            [MaxLength(250)]
            public string Name { get; set; }

            [MaxLength(250)]
            public string Supplier { get; set; }

            [MaxLength(200)]
            public string Uri { get; set; }

            [MaxLength(250)]
            public string UserName { get; set; }

            [MaxLength(250)]
            public string Password { get; set; }
        }

        public class Edit
            : Create
        {
            public int Id { get; set; }
        }
    }
}