using AutoMapper;
using CatalogService.Dtos;
using CatalogService.MessageBus;
using CatalogService.Models;
using CatalogService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : Controller
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBusClient;

        public CatalogController(IProductRepo productRepo, IMapper mapper, IMessageBusClient messageBusClient)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _messageBusClient = messageBusClient;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAllProducts()
        {
            var products = await _productRepo.GetAllAsync();
            var productsDto = _mapper.Map<IEnumerable<ProductReadDto>>(products);
            return Ok(productsDto);
        }

        [HttpGet("{id}", Name= "GetProductById")]
        public async Task<ActionResult<ProductReadDto>> GetProductById(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(_mapper.Map<ProductReadDto>(product));
        }

        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> CreateProduct([FromBody] ProductCreateDto productCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var product = _mapper.Map<Product>(productCreateDto);
            int reponse = await _productRepo.CreateAsync(product);

            if (reponse == 0)
                return BadRequest("Could not create product");

            var productReadDto = _mapper.Map<ProductReadDto>(product);
            return CreatedAtRoute(nameof(GetProductById), new {id = product.Id}, productReadDto);
        }

        [HttpPost("{id}/addtocart/{username}")]
        public async Task<ActionResult> AddProductToCart(int id, string username)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            var productRead = _mapper.Map<ProductReadDto>(product);
            
            var productPublishDto = _mapper.Map<ProductPublishDto>(productRead);
            productPublishDto.Event = "ProductAddedToCart";
            productPublishDto.Username = username;
            _messageBusClient.AddProductToCart(productPublishDto);
            return Ok();
        }

    }
}