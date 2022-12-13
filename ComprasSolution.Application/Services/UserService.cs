using ComprasSolution.Application.DTOs;
using ComprasSolution.Application.Services.Interfaces;
using ComprasSolution.Domain.Interfaces;
using ComprasSolution.Infra.Data.Authentication;
using FluentValidation;

namespace ComprasSolution.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IValidator<UserDTO> _validator;

        public UserService(IUserRepository userRepository, ITokenGenerator tokenGenerator, IValidator<UserDTO> validator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _validator = validator;
        }

        public async Task<ResultService<dynamic>> GenerateTokenAsync(UserDTO userDTO)
        {
            if (userDTO == null)
                return ResultService.Fail<dynamic>("Objeto deve ser informado");

            var validation = await _validator.ValidateAsync(userDTO);
            if (!validation.IsValid)
                return ResultService.RequestError<dynamic>("Problemas de Validação!", validation);

            var user = await _userRepository.GetUserByEmailAndPasswordAsync(userDTO.Email, userDTO.Password);
            if (user == null)
                return ResultService.Fail<dynamic>("Usuário ou senha não encontrados");

            return ResultService.Ok(_tokenGenerator.Generator(user));
        }
    }
}
