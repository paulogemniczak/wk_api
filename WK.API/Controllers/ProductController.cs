using WK.API.Validators;
using Microsoft.AspNetCore.Mvc;
using WK.AppService.Interfaces;
using System.Net.Mime;
using WK.API.Results;
using WK.AppService.Dtos;
using WK.API.Extensions;
using WK.AppService.Filters;

namespace WK.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductController : Controller
    {
        private readonly IProductAppService _appService;
        private readonly ProductValidator _validator;

        public ProductController(
            IProductAppService appService,
            ProductValidator validator)
        {
            _appService = appService;
            _validator = validator;
        }

        /// <summary>
        /// Register product.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>
        /// Request example:
        /// 
        ///     POST /api/Product
        ///     {
        ///         "productName": "produto teste",
        ///         "productCategory": {
        ///             "categoryId": 1,
        ///             "categoryName": "categoria abc"
        ///         }
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Register product successfully.</response>
        /// <response code="422">Model invalid.</response>
        /// <response code="500">Internal server error.</response>
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto model)
        {
            var result = new GenericResult<ProductDto>();
            var validatorResult = _validator.Validate(model);

            // if model is invalid, return 422
            if (!validatorResult.IsValid)
            {
                result.Errors = validatorResult.GetErrors();
                return StatusCode(StatusCodes.Status422UnprocessableEntity, result);
            }

            try
            {
                result.Result = await _appService.Create(model);
                result.Success = true;
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }

        /// <summary>
        /// Update product.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>
        /// Request example:
        /// 
        ///     PUT /api/Product/1
        ///     {
        ///         "productId": 1,
        ///         "productName": "produto teste 123",
        ///         "productCategory": {
        ///             "categoryId": 1,
        ///             "categoryName": "categoria abc"
        ///         }
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Update product successfully.</response>
        /// <response code="422">Model invalid.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="500">Internal server error.</response>
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductDto model)
        {
            var result = new GenericResult();
            var validatorResult = _validator.Validate(model);

            if (id != model.ProductId)
            {
                result.Errors = new string[] { "Id inválido." };
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }

            // if model is invalid, return 422
            if (!validatorResult.IsValid)
            {
                result.Errors = validatorResult.GetErrors();
                return StatusCode(StatusCodes.Status422UnprocessableEntity, result);
            }

            try
            {
                await _appService.Update(model);
                result.Success = true;
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }

        /// <summary>
        /// Delete product.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>
        /// Request example:
        /// 
        ///     DELETE /api/Product/1
        ///     {
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Delete product successfully.</response>
        /// <response code="500">Internal server error.</response>
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = new GenericResult();

            try
            {
                await _appService.Delete(id);
                result.Success = true;
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }

        /// <summary>
        /// Get a specific product.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Request example:
        /// 
        ///     GET /api/Product/1
        ///     {
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Get product successfully.</response>
        /// <response code="404">Product not found.</response>
        /// <response code="500">Internal server error.</response>
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = new GenericResult<ProductDto>();

            try
            {
                result.Result = await _appService.GetById(id);
                if (result.Result is null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, result);
                }
                else
                {
                    result.Success = true;
                    return StatusCode(StatusCodes.Status200OK, result);
                }
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }

        /// <summary>
        /// Get a list of product.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <remarks>
        /// Request example:
        /// 
        ///     GET /api/Product?InputText=cat&amp;PageNumber=1&amp;PageSize=10&amp;OrderBy=productName&amp;IsAsc=true
        ///     {
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Get categories successfully.</response>
        /// <response code="500">Internal server error.</response>
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ProductFilterDto filter)
        {
            var result = new GenericResult<IEnumerable<ProductDto>>();

            try
            {
                result.Result = await _appService.List(filter);
                result.Success = true;
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                result.Errors = new string[] { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }
    }
}
