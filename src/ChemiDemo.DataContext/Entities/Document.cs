namespace ChemiDemo.DataContext.Entities
{
    using System;

    public class Document
    {
        public virtual string ContentType { get; set; }

        public virtual byte[] Content { get; set; }

        public virtual int StatusCode { get; set; }

        public virtual DateTime? LastUpdate { get; set; }
    }
}