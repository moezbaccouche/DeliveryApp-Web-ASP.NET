using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeliveryApp.API.Models.DTO;
using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.ControllersAPI
{
    [ApiController]
    [Route("delivery-app/deliveryInfos")]
    public class DeliveryInfosController : ControllerBase
    {
        private readonly IDeliveryInfoService deliveryInfosService;
        private readonly IOrderService orderService;
        private readonly IMapper _mapper;
        private readonly IDeliveryManService deliveryManService;

        public DeliveryInfosController(IDeliveryInfoService deliveryInfosService, IOrderService orderService,
            IMapper mapper, IDeliveryManService deliveryManService)
        {
            this.deliveryInfosService = deliveryInfosService;
            this.orderService = orderService;
            _mapper = mapper;
            this.deliveryManService = deliveryManService;
        }

        [EnableCors("AllowAll")]
        [HttpGet("orders/{orderId}")]
        public ActionResult<DeliveryInfoForTrackingDto> GetOrderDeliveryInfos(int orderId)
        {
            var order = orderService.GetOrderById(orderId);
            if (order == null)
            {
                return NotFound();
            }

            var infos = deliveryInfosService.GetOrderDeliveryInfo(orderId);
            if (infos == null)
            {
                return NotFound();
            }

            var deliveryMan = deliveryManService.GetDeliveryManById(infos.IdDeliveryMan);

            var deliveryManToReturn = _mapper.Map<DeliveryManForTrackingPageDto>(deliveryMan);

            var deliveryInfos = new DeliveryInfoForTrackingDto
            {
                OrderId = orderId,
                OrderTime = order.OrderTime,
                EstimatedDeliveryTime = infos.EstimatedDeliveryTime,
                AcceptingDeliveryTime = infos.AcceptingOrderTime,
                Distance = 0,
                DeliveryMan = deliveryManToReturn,
                RealDeliveryTime = infos.RealDeliveryTime
            };

            return Ok(deliveryInfos);
        }
    }
}