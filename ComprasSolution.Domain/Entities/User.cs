using ComprasSolution.Domain.Validations;

namespace ComprasSolution.Domain.Entities
{
    public class User : Base
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public User(string email, string password)
        {
            Validation(email, password);
        }

        public User(int id, string email, string password)
        {
            DomainValidationException.When(id <= 0, "Id deve ser informado");
            Id = id;
            Validation(email, password);
        }

        private void Validation(string email, string password)
        {
            DomainValidationException.When(string.IsNullOrEmpty(email), "Email deve ser informado");
            DomainValidationException.When(string.IsNullOrEmpty(password), "Senha deve ser informado");

            Email = email;
            Password = password;
        }
    }
}
