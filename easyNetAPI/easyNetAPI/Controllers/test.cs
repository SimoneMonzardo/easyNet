using easyNetAPI.Data.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using easyNetAPI.Models;
using Org.BouncyCastle.Bcpg;

namespace easyNetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class test : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public test(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IEnumerable<UserBehavior> GetUsers(string userId)
        {
            // Qui puoi implementare la logica per ottenere i dati desiderati
            // Ad esempio, puoi creare una lista di oggetti WeatherForecast e restituirla

            var forecasts = _unitOfWork.UserBehavior.GetAllAsync().Result;
            return forecasts;
        }
        [HttpPost]
        public void PostUsers(UserBehavior user, string userId)
        {
            _unitOfWork.UserBehavior.AddAsync(user);
        }
        [HttpPatch]
        public void patchUsers(Company user, int userId)
        {
        }
        [HttpDelete]
        public void delete(int userId)
        {
            _unitOfWork.Company.RemoveAsync(userId);
        }
        //[HttpGet]
        //public async Task<UserBehavior> GetCompanies(string userId)
        //{
        //     Qui puoi implementare la logica per ottenere i dati desiderati
        //     Ad esempio, puoi creare una lista di oggetti WeatherForecast e restituirla

        //    var forecasts = await _unitOfWork.UserBehavior.GetFirstOrDefault(userId);

        //    return forecasts;
        //}
    }

}
