using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetStore.DTOS;
using PetStore.Services.StoreService;


namespace PetStore.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "User,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            this._storeService = storeService;
        }

        [HttpGet]
        public IActionResult GetAllOrders([FromQuery]string userid)
        {
            var orders = _storeService.GetAllOrders(userid);

            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOrder( int orderId)
        {
            var orders = _storeService.GetOrder(orderId);

            return Ok(orders);
        }

        [HttpPost]
        public IActionResult  AddOrder(OrderDTO order)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
           
           var result = _storeService.AddOrder(order);
            if (!string.IsNullOrEmpty(result)) return BadRequest(result);
            return Ok(new {status="Order Created"});
        }

    }
}
