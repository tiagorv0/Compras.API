using AutoMapper;
using ComprasSolution.Application.DTOs;
using ComprasSolution.Application.Services.Interfaces;
using ComprasSolution.Domain.Entities;
using ComprasSolution.Domain.Interfaces;
using FluentValidation;

namespace ComprasSolution.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<PurchaseDTO> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseService(IPurchaseRepository purchaseRepository,
                            IMapper mapper,
                            IValidator<PurchaseDTO> validator,
                            IUnitOfWork unitOfWork,
                            IProductRepository productRepository,
                            IPersonRepository personRepository)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _personRepository = personRepository;
        }

        public async Task<ResultService<PurchaseDTO>> CreateAsync(PurchaseDTO purchaseDTO)
        {
            if (purchaseDTO == null)
                return ResultService.Fail<PurchaseDTO>("Objeto deve ser informado!");

            var validation = await _validator.ValidateAsync(purchaseDTO);
            if (!validation.IsValid)
                return ResultService.RequestError<PurchaseDTO>("Problemas de Validação!", validation);

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var productId = await _productRepository.GetIdByCodErpAsync(purchaseDTO.CodErp);
                if (productId == 0)
                {
                    var product = new Product(purchaseDTO.ProductName, purchaseDTO.CodErp, purchaseDTO.Price.Value);
                    await _productRepository.CreateAsync(product);
                    productId = product.Id;
                }

                var personId = await _personRepository.GetIdByDocumentAsync(purchaseDTO.Document);
                var purchase = new Purchase(productId, personId);

                var data = await _purchaseRepository.CreateAsync(purchase);
                purchaseDTO.Id = data.Id;

                await _unitOfWork.CommitTransactionAsync();

                return ResultService.Ok<PurchaseDTO>(purchaseDTO);
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollBackAsync();
                return ResultService.Fail<PurchaseDTO>($"Erro: {ex.Message}");
            }
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var purchase = await _purchaseRepository.GetByIdAsync(id);
            if (purchase == null)
                return ResultService.Fail("Compra não encontrada");

            await _purchaseRepository.DeleteAsync(purchase);
            return ResultService.Ok($"Compra do {id} foi deletada com sucesso");
        }

        public async Task<ResultService<ICollection<PurchaseDetailDTO>>> GetAllAsync()
        {
            var purchases = await _purchaseRepository.GetAllAsync();
            return ResultService.Ok(_mapper.Map<ICollection<PurchaseDetailDTO>>(purchases));
        }

        public async Task<ResultService<PurchaseDetailDTO>> GetByIdAsync(int id)
        {
            var purchase = await _purchaseRepository.GetByIdAsync(id);
            if (purchase == null)
                return ResultService.Fail<PurchaseDetailDTO>("Compra não encontrada");

            return ResultService.Ok(_mapper.Map<PurchaseDetailDTO>(purchase));
        }

        public async Task<ResultService<PurchaseDTO>> UpdateAsync(PurchaseDTO purchaseDTO)
        {
            if (purchaseDTO == null)
                return ResultService.Fail<PurchaseDTO>("Objeto deve ser informado!");

            var validation = await _validator.ValidateAsync(purchaseDTO);
            if (!validation.IsValid)
                return ResultService.RequestError<PurchaseDTO>("Problemas de Validação!", validation);

            var purchase = await _purchaseRepository.GetByIdAsync(purchaseDTO.Id);

            if(purchase == null)
                return ResultService.Fail<PurchaseDTO>("Compra não encontrada!");

            var productId = await _productRepository.GetIdByCodErpAsync(purchaseDTO.CodErp);
            var personId = await _personRepository.GetIdByDocumentAsync(purchaseDTO.Document);
            purchase.Edit(purchase.Id, productId, personId);

            await _purchaseRepository.UpdateAsync(purchase);

            return ResultService.Ok(_mapper.Map<PurchaseDTO>(purchase));
        }
    }
}
