using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Text.Json.Serialization;

namespace CarRent.API.Domain.Entity
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }

        [JsonIgnore]
        private ILazyLoader LazyLoader { get; set; }
    }
}
