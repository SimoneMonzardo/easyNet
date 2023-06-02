using easyNetAPI.Data;
using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using easyNetAPI.Models.ModelVM;
using easyNetAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Drawing;

namespace easyNetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AziendaController : Controller
    {
        private readonly ILogger<AziendaController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public AziendaController(ILogger<AziendaController> logger, IUnitOfWork unitOfWork, AppDbContext db, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _db = db;
            _userManager = userManager;
        }
        [HttpGet("GetCompanies")]
        [Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_MODERATOR}")]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId is null)
                    return BadRequest("UserId is null");
                var aziende = await _unitOfWork.Company.GetAllAsync();
                var companies = new List<Company>();
                foreach ( var company in aziende)
                {
                    if(company.CompanyId!=0 && !companies.Contains(company))
                        companies.Add(company);
                }
                return Json(companies);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
        [HttpGet("GetCompany")]
        [Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}, {SD.ROLE_MODERATOR}")]
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
        public async Task<IActionResult> PostCompany(Company company, string username)
        {
            try
            {
                var userIdOfCompanyAdmin = await AuthControllerUtility.GetUserIdFromUsername(username, _db);
                if (!ModelState.IsValid)
                    return BadRequest("Model is not valid");
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId is null)
                    return BadRequest("UserId is null");
                var user = _db.Users.Find(userIdOfCompanyAdmin);
                if (user is null)
                    return BadRequest("User not found");
                var result = true;
                var isUserInRole = await _userManager.IsInRoleAsync(user, SD.ROLE_COMPANY_ADMIN);
                if (!isUserInRole)
                    result = (await _userManager.AddToRoleAsync(user, SD.ROLE_COMPANY_ADMIN)).Succeeded;
                if (!result)
                    return BadRequest("Could not add user to role company admin");
                company.CompanyId = await IdAutoincrementService.GetCompanyAutoincrementId(_unitOfWork);
                var risultato = await _unitOfWork.Company.AddAsync(company, userIdOfCompanyAdmin);
                if(risultato)
                    return Ok("Company created succesfully");
                return BadRequest("Company cannot be created");
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
                var user = await _unitOfWork.UserBehavior.GetFirstOrDefault(userId);
                var risultato = false;
                if(user.Company.CompanyId == company.CompanyId)
                    risultato = await _unitOfWork.Company.UpdateOneAsync(company);
                return Ok(risultato);
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
                var user = _db.Users.Find(userId);
                if (user is null)
                    return BadRequest("User not found");
                var users = await _unitOfWork.UserBehavior.GetAllAsync();
                var result = await _unitOfWork.Company.RemoveAsync(id);
                if (!result)
                    return BadRequest("Could not remove company");
                foreach (var u in users)
                {
                    if (u.Company is not null)
                    {
                        if (u.Company.CompanyId == id)
                        {
                            var _user = _db.Users.Find(u.UserId);
                            if (_user is not null)
                            {
                                if (await _userManager.IsInRoleAsync(_user, SD.ROLE_EMPLOYEE))
                                    await _userManager.RemoveFromRoleAsync(_user, SD.ROLE_EMPLOYEE);
                                if (await _userManager.IsInRoleAsync(_user, SD.ROLE_COMPANY_ADMIN))
                                    await _userManager.RemoveFromRoleAsync(_user, SD.ROLE_COMPANY_ADMIN);
                                result = await _unitOfWork.Company.AddAsync(new Company() { CompanyId = 0, CompanyName = "" }, _user.Id);
                                if (!result)
                                    return BadRequest("Could not add an empty company");
                            }
                        }
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpPost("RequestToAddCompany")]
        [Authorize(Roles = $"{SD.ROLE_USER}")]
        public async Task<IActionResult> RequestToAddCompany(Company company)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Model is not valid");
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId is null)
                    return BadRequest("UserId is null");
                var user = await _unitOfWork.UserBehavior.GetFirstOrDefault(userId);
                if (user is null)
                    return BadRequest("User not found");
                var oldcompany = user.Company;
                if (oldcompany is not null && oldcompany.CompanyId !=0)
                    return BadRequest("User already has a company");
                company.CompanyId = 0;
                var risultato = await _unitOfWork.Company.AddAsync(company, userId);
                if (risultato)
                    return Ok("Request sent succesfully");
                return BadRequest("Request couldn't be sent");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("GetAddCompanyRequest")]
        [Authorize(Roles = $"{SD.ROLE_MODERATOR}")]
        public async Task<IActionResult> GetAddCompanyRequest()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId is null)
                    return BadRequest("UserId is null");
                var aziende = await _unitOfWork.Company.GetAllAsync();
                var companies = new List<Company>();
                var companyRequest = new List<CompanyRequest>();
                foreach (var company in aziende)
                {
                    if (company.CompanyId == 0 && !companies.Contains(company) && !company.CompanyName.IsNullOrEmpty())
                    {
                        companies.Add(company);
                        string userOfCompanyId;
                        try
                        {
                            userOfCompanyId = _unitOfWork.UserBehavior.GetAllAsync().Result.ToList().Where(u => u.Company.CompanyName == company.CompanyName).First().UserId;
                        }catch(Exception ex)
                        {
                            return BadRequest("Couldn't get user who made a request: " + ex.Message);
                        }
                        var user = _db.Users.Find(userOfCompanyId);
                        if (user is not null)
                            companyRequest.Add(new CompanyRequest() { company = company, username = user.UserName });
                    }
                }
                return Json(companyRequest);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpPost("RequestToDeleteCompany")]
        [Authorize(Roles = $"{SD.ROLE_COMPANY_ADMIN}")]
        public async Task<IActionResult> RequestToDeleteCompany()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Model is not valid");
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId is null)
                    return BadRequest("UserId is null");
                var user = await _unitOfWork.UserBehavior.GetFirstOrDefault(userId);
                if (user is null)
                    return BadRequest("User not found");
                var company = user.Company;
                if (company is null)
                    return BadRequest("Company not found");
                if (company.CompanyId < 0)
                    return BadRequest("Request already sent");
                if (company.CompanyId == 0)
                    return BadRequest("User doesn't have a company");
                company.CompanyId = -company.CompanyId;
                var risultato = await _unitOfWork.Company.AddAsync(company, userId);
                if (risultato)
                    return Ok("Request sent succesfully");
                return BadRequest("Request couldn't be sent");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
        [HttpGet("GetDeleteCompanyRequest")]
        [Authorize(Roles = $"{SD.ROLE_MODERATOR}")]
        public async Task<IActionResult> GetDeleteCompanyRequest()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId is null)
                    return BadRequest("UserId is null");
                var aziende = await _unitOfWork.Company.GetAllAsync();
                var companies = new List<Company>();
                var companyRequest = new List<CompanyRequest>();
                foreach (var company in aziende)
                {
                    if (company.CompanyId < 0 && !companies.Contains(company) && !company.CompanyName.IsNullOrEmpty())
                    {
                        companies.Add(company);
                        string userOfCompanyId;
                        try
                        {
                            userOfCompanyId = _unitOfWork.UserBehavior.GetAllAsync().Result.ToList().Where(u => u.Company.CompanyName == company.CompanyName).First().UserId;
                        }
                        catch (Exception ex)
                        {
                            return BadRequest("Couldn't get user who made a request: " + ex.Message);
                        }
                        var user = _db.Users.Find(userOfCompanyId);
                        if (user is not null)
                            companyRequest.Add(new CompanyRequest() { company = company, username = user.UserName });
                    }
                }
                return Json(companyRequest);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

    }
}
