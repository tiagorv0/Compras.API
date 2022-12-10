using ComprasSolution.Domain.Validations;

namespace ComprasSolution.Domain.Entities
{
    public sealed class Person : Base
    {
        public string Name { get; private set; }
        public string Document { get; private set; }
        public string Phone { get; private set; }
        public ICollection<Purchase> Purcheses { get; set; } = new List<Purchase>();

        public Person(string name, string document, string phone)
        {
            Validation(name, document, phone);
        }

        public Person(int id, string name, string document, string phone)
        {
            DomainValidationException.When(id < 0, "Id deve ser maior que zero");

            Id = id;
            Validation(name, document, phone);
        }

        private void Validation(string name, string document, string phone)
        {
            DomainValidationException.When(string.IsNullOrEmpty(name), "Nome deve ser informado");
            DomainValidationException.When(string.IsNullOrEmpty(document), "Docmento deve ser informado");
            DomainValidationException.When(string.IsNullOrEmpty(phone), "Telefone deve ser informado");

            Name = name;
            Document = document;
            Phone = phone;
        }
    }
}
