namespace ChemiDemo.DataContext.Entities
{
    using System.Net;

    public class Product
        : IEntity
    {
        private string _username;
        private string _password;

        public virtual int Id { get; protected set; }

        public virtual string Name { get; set; }

        public virtual string Supplier { get; set; }

        public virtual string Uri { get; set; }

        public virtual NetworkCredential Login
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_username) && !string.IsNullOrWhiteSpace(_password))
                {
                    return new NetworkCredential(_username, _password);
                }

                return null;
            }

            set
            {
                _username = value.UserName;
                _password = value.Password;
            }
        }

        public virtual Document Document { get; set; }

        public virtual bool IsUpdated { get; set; }
    }
}