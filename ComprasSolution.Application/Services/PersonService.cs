using AutoMapper;
using ComprasSolution.Application.DTOs;
using ComprasSolution.Application.Services.Interfaces;
using ComprasSolution.Domain.Entities;
using ComprasSolution.Domain.FiltersDb;
using ComprasSolution.Domain.Interfaces;
using FluentValidation;

namespace ComprasSolution.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<PersonDTO> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IPersonRepository personRepository,
                            IMapper mapper,
                            IValidator<PersonDTO> validator
,
                            IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<PersonDTO>> CreateAsync(PersonDTO personDTO)
        {
            if (personDTO == null)
                return ResultService.Fail<PersonDTO>("Objeto deve ser informado");

            var validation = await _validator.ValidateAsync(personDTO);
            if (!validation.IsValid)
                return ResultService.RequestError<PersonDTO>("Problemas de Validação!", validation);

            var person = _mapper.Map<Person>(personDTO);
            var result = await _personRepository.CreateAsync(person);

            return ResultService.Ok<PersonDTO>(_mapper.Map<PersonDTO>(result));
        }

        public async Task<ResultService<PersonDTO>> GetByIdAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
                return ResultService.Fail<PersonDTO>("Pessoa não encontrada");

            return ResultService.Ok<PersonDTO>(_mapper.Map<PersonDTO>(person));
        }

        public async Task<ResultService<ICollection<PersonDTO>>> GetAllAsync()
        {
            var people = await _personRepository.GetAllAsync();
            return ResultService.Ok<ICollection<PersonDTO>>(_mapper.Map<ICollection<PersonDTO>>(people));
        }

        public async Task<ResultService<PersonDTO>> UpdateAsync(PersonDTO personDTO)
        {
            if (personDTO == null)
                return ResultService.Fail<PersonDTO>("Objeto deve ser informado");

            var validation = await _validator.ValidateAsync(personDTO);
            if (!validation.IsValid)
                return ResultService.RequestError<PersonDTO>("Problemas de Validação!", validation);

            var person = await _personRepository.GetByIdAsync(personDTO.Id);
            if (person == null)
                return ResultService.Fail<PersonDTO>("Pessoa não encontrada");

            var result = _mapper.Map<PersonDTO, Person>(personDTO, person);
            await _personRepository.UpdateAsync(result);

            return ResultService.Ok<PersonDTO>(_mapper.Map<PersonDTO>(result));
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
                return ResultService.Fail("Pessoa não encontrada");

            await _personRepository.DeleteAsync(person);

            return ResultService.Ok($"Pessoa do {id} foi deletada com sucesso");
        }

        public async Task<ResultService<PagedBaseResponseDTO<PersonDTO>>> GetPagedAsync(PersonFilterDb personFilterDb)
        {
            var peoplePaged = await _personRepository.GetPagedAsync(personFilterDb);
            var result = new PagedBaseResponseDTO<PersonDTO>(
                peoplePaged.TotalRegisters,
                _mapper.Map<List<PersonDTO>>(peoplePaged.Data)
            );
            return ResultService.Ok(result);
        }
    }
}
