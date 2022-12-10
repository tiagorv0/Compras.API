using ComprasSolution.Domain.Validations;

namespace ComprasSolution.Domain.Entities
{
    public sealed class Purchase : Base
    {
        public int ProductId { get; private set; }
        public Product Product { get; set; }
        public int PersonId { get; private set; }
        public Person Person { get; set; }
        public DateTime Date { get; private set; }

        public Purchase(int productId, int personId)
        {
            Validation(productId, personId);
        }

        public Purchase(int id, int productId, int personId)
        {
            DomainValidationException.When(id <= 0, "Id deve ser maior que zero");

            Id = id;
            Validation(productId, personId);
        }

        public void Edit(int id, int productId, int personId)
        {
            DomainValidationException.When(id <= 0, "Id deve ser maior que zero");
            Id = id;
            Validation(productId, personId);
        }

        private void Validation(int productId, int personId)
        {
            DomainValidationException.When(productId <= 0, "Id Produto deve ser informado");
            DomainValidationException.When(personId <= 0, "Id Pessoa deve ser informado");

            ProductId = productId;
            PersonId = personId;
            Date = DateTime.UtcNow;
        }
    }
}
