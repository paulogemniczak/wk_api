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
    public class CategoryController : Controller
    {
        private readonly ICategoryAppService _appService;
        private readonly CategoryValidator _validator;

        public CategoryController(
            ICategoryAppService appService,
            CategoryValidator validator)
        {
            _appService = appService;
            _validator = validator;
        }

        /// <summary>
        /// Register category.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>
        /// Request example:
        /// 
        ///     POST /api/Category
        ///     {
        ///         "categoryName": "categoria teste"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Register category successfully.</response>
        /// <response code="422">Model invalid.</response>
        /// <response code="500">Internal server error.</response>
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryDto model)
        {
            var result = new GenericResult<CategoryDto>();
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
        /// Update category.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>
        /// Request example:
        /// 
        ///     PUT /api/Category/1
        ///     {
        ///         "categoryId": 1,
        ///         "categoryName": "categoria teste abc"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Update category successfully.</response>
        /// <response code="422">Model invalid.</response>
        /// <response code="400">Invalid id.</response>
        /// <response code="500">Internal server error.</response>
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryDto model)
        {
            var result = new GenericResult();
            var validatorResult = _validator.Validate(model);

            if (id != model.CategoryId)
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
        /// Delete category.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>
        /// Request example:
        /// 
        ///     DELETE /api/Category/1
        ///     {
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Delete category successfully.</response>
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
        /// Get a specific category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Request example:
        /// 
        ///     GET /api/Category/1
        ///     {
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Get category successfully.</response>
        /// <response code="404">Category not found.</response>
        /// <response code="500">Internal server error.</response>
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = new GenericResult<CategoryDto>();

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
        /// Get a list of category.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <remarks>
        /// Request example:
        /// 
        ///     GET /api/Category?InputText=cat&amp;PageNumber=1&amp;PageSize=10&amp;OrderBy=categoryName&amp;IsAsc=true
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
        public async Task<IActionResult> Get([FromQuery] CategoryFilterDto filter)
        {
            var result = new GenericResult<IEnumerable<CategoryDto>>();

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
