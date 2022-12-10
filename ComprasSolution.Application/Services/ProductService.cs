using AutoMapper;
using ComprasSolution.Application.DTOs;
using ComprasSolution.Application.Services.Interfaces;
using ComprasSolution.Domain.Entities;
using ComprasSolution.Domain.Interfaces;
using FluentValidation;

namespace ComprasSolution.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductDTO> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository,
                            IMapper mapper,
                            IValidator<ProductDTO> validator
,
                            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<ProductDTO>> CreateAsync(ProductDTO productDTO)
        {
            if (productDTO == null)
                return ResultService.Fail<ProductDTO>("Objeto deve ser informado");

            var validation = await _validator.ValidateAsync(productDTO);
            if (!validation.IsValid)
                return ResultService.RequestError<ProductDTO>("Problemas de Validação!", validation);

            var product = _mapper.Map<Product>(productDTO);
            var result = await _productRepository.CreateAsync(product);
            await _unitOfWork.CommitAsync();
            return ResultService.Ok<ProductDTO>(_mapper.Map<ProductDTO>(result));
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return ResultService.Fail("Produto não encontrado");

            await _productRepository.DeleteAsync(product);
            await _unitOfWork.CommitAsync();
            return ResultService.Ok($"Produto do {id} foi deletado com sucesso");
        }

        public async Task<ResultService<ICollection<ProductDTO>>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return ResultService.Ok<ICollection<ProductDTO>>(_mapper.Map<ICollection<ProductDTO>>(products));
        }

        public async Task<ResultService<ProductDTO>> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return ResultService.Fail<ProductDTO>("Produto não encontrado");

            return ResultService.Ok<ProductDTO>(_mapper.Map<ProductDTO>(product));
        }

        public async Task<ResultService<ProductDTO>> UpdateAsync(ProductDTO productDTO)
        {
            if (productDTO == null)
                return ResultService.Fail<ProductDTO>("Objeto deve ser informado");

            var validation = await _validator.ValidateAsync(productDTO);
            if (!validation.IsValid)
                return ResultService.RequestError<ProductDTO>("Problemas de Validação!", validation);

            var product = await _productRepository.GetByIdAsync(productDTO.Id);
            if (product == null)
                return ResultService.Fail<ProductDTO>("Produto não encontrado");

            var result = _mapper.Map<ProductDTO, Product>(productDTO, product);
            await _productRepository.UpdateAsync(result);
            await _unitOfWork.CommitAsync();

            return ResultService.Ok<ProductDTO>(_mapper.Map<ProductDTO>(result));
        }
    }
}
