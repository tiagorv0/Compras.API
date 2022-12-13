using AutoMapper;
using ComprasSolution.Application.DTOs;
using ComprasSolution.Application.Services.Interfaces;
using ComprasSolution.Domain.Entities;
using ComprasSolution.Domain.Integrations;
using ComprasSolution.Domain.Interfaces;
using FluentValidation;

namespace ComprasSolution.Application.Services
{
    public class PersonImageService : IPersonImageService
    {
        private readonly IPersonImageRepository _personImageRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IValidator<PersonImageDTO> _validator;
        private readonly ISavePersonImage _savePersonImage;

        public PersonImageService(IPersonImageRepository personImageRepository, IValidator<PersonImageDTO> validator, IPersonRepository personRepository, ISavePersonImage savePersonImage)
        {
            _personImageRepository = personImageRepository;
            _validator = validator;
            _personRepository = personRepository;
            _savePersonImage = savePersonImage;
        }

        public async Task<ResultService> CreateImageAsync(PersonImageDTO personImageDTO)
        {
            if (personImageDTO == null)
                return ResultService.Fail("Objeto deve ser informado");

            var validation = await _validator.ValidateAsync(personImageDTO);
            if (!validation.IsValid)
                return ResultService.RequestError("Problemas de Validação!", validation);

            var person = await _personRepository.GetByIdAsync(personImageDTO.PersonId);
            if (person == null)
                return ResultService.Fail("Pessoa não encontrada");

            var pathImage = _savePersonImage.Save(personImageDTO.Image);
            var personImage = new PersonImage(person.Id, pathImage, null);
            await _personImageRepository.CreateAsync(personImage);
            return ResultService.Ok("Imagem Salva");
        }

        public async Task<ResultService> CreateImageBase64Async(PersonImageDTO personImageDTO)
        {
            if (personImageDTO == null)
                return ResultService.Fail("Objeto deve ser informado");

            var validation = await _validator.ValidateAsync(personImageDTO);
            if (!validation.IsValid)
                return ResultService.RequestError("Problemas de Validação!", validation);

            var person = await _personRepository.GetByIdAsync(personImageDTO.PersonId);
            if (person == null)
                return ResultService.Fail("Pessoa não encontrada");

            var personImage = new PersonImage(person.Id, null, personImageDTO.Image);
            await _personImageRepository.CreateAsync(personImage);
            return ResultService.Ok("Imagem em base64 salva");
        }
    }
}
