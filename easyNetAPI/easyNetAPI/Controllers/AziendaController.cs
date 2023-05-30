using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace easyNetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AziendaController : Controller
    {
        private readonly ILogger<AziendaController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public AziendaController(ILogger<AziendaController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetCompanies")]
        [Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId is null)
                    return BadRequest("UserId is null");
                var aziende = await _unitOfWork.Company.GetAllAsync();
                return Json(aziende);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
        [HttpGet("GetCompany")]
        [Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId is null)
                    return BadRequest("UserId is null");
                if (id == null)
                    return BadRequest("id of company is null"); 
                var azienda = await _unitOfWork.Company.GetFirstOrDefault(id);
                return Json(azienda);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
        [HttpPost("PostCompany")]
        [Authorize(Roles = $"{SD.ROLE_MODERATOR}")]
        public async Task<IActionResult> PostCompany(Company company)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Model is not valid");
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId is null)
                    return BadRequest("UserId is null");
                var azienda = await _unitOfWork.Company.AddAsync(company, userId);
                return Ok(azienda);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
        [HttpPut("PutCompany")]
        [Authorize(Roles = $"{SD.ROLE_COMPANY_ADMIN}")]
        public async Task<IActionResult> PutCompany(Company company)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Model is not valid");
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId is null)
                    return BadRequest("UserId is null");
                var azienda = await _unitOfWork.Company.UpdateOneAsync(company);
                return Ok(azienda);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
        [HttpDelete("DeleteCompany")]
        [Authorize(Roles = $"{SD.ROLE_MODERATOR}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId is null)
                    return BadRequest("UserId is null");
                if (id == null)
                    return BadRequest("id of company is null");
                var azienda = await _unitOfWork.Company.RemoveAsync(id);
                return Ok(azienda);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
    }
}
