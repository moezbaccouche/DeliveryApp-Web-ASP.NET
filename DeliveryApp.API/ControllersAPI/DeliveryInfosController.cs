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
        private readonly ICurrentLocationService currentLocationService;

        public DeliveryInfosController(IDeliveryInfoService deliveryInfosService, IOrderService orderService,
            IMapper mapper, IDeliveryManService deliveryManService, ICurrentLocationService currentLocationService)
        {
            this.deliveryInfosService = deliveryInfosService;
            this.orderService = orderService;
            _mapper = mapper;
            this.deliveryManService = deliveryManService;
            this.currentLocationService = currentLocationService;
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
                OrderStatus = order.Status,
                //EstimatedDeliveryTime = infos.EstimatedDeliveryTime,
                EstimatedDeliveryTime = order.EstimatedDeliveryTime,
                AcceptingDeliveryTime = infos.AcceptingOrderTime,
                Distance = 0,
                DeliveryMan = deliveryManToReturn,
                RealDeliveryTime = infos.RealDeliveryTime
            };

            return Ok(deliveryInfos);
        }

        [EnableCors("AllowAll")]
        [HttpGet("location/{deliveryManId}")]
        public ActionResult<DeliveryManCurrentLocationDto> GetDeliveryManCurrentLocation(int deliveryManId)
        {
            var deliveryMan = deliveryManService.GetDeliveryManById(deliveryManId);
            if (deliveryMan == null)
            {
                return NotFound();
            }

            var deliveryManCurrentLocation = currentLocationService.GetDeliveryManCurrentLocation(deliveryManId);
            if (deliveryManCurrentLocation == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DeliveryManCurrentLocationDto>(deliveryManCurrentLocation));
        }

        [EnableCors("AllowAll")]
        [HttpPost("location/add")]
        public ActionResult<DeliveryManCurrentLocationDto> AddDeliveryManCurrentLocation([FromBody]DeliveryManCurrentLocationDto location)
        {
            var deliveryMan = deliveryManService.GetDeliveryManById(location.DeliveryManId);
            if (deliveryMan == null)
            {
                return NotFound();
            }

            var newlocation = _mapper.Map<CurrentLocation>(location);
            var locationToReturn = currentLocationService.AddDeliveryManCurrentLocation(newlocation);

            return Ok(_mapper.Map<DeliveryManCurrentLocationDto>(locationToReturn));
        }

        [EnableCors("AllowAll")]
        [HttpPost("location/update")]
        public ActionResult<DeliveryManCurrentLocationDto> UpdateDeliveryManCurrentLocation([FromBody]DeliveryManCurrentLocationDto location)
        {
            var deliveryMan = deliveryManService.GetDeliveryManById(location.DeliveryManId);
            if (deliveryMan == null)
            {
                return NotFound();
            }

            var deliveryManCurrentLocation = currentLocationService.GetDeliveryManCurrentLocation(location.DeliveryManId);
            if (deliveryManCurrentLocation == null)
            {
                return NotFound();
            }

            deliveryManCurrentLocation.Lat = location.Lat;
            deliveryManCurrentLocation.Long = location.Long;

            var newLocation = currentLocationService.UpdateDeliveryManCurrentLocation(deliveryManCurrentLocation);
            return Ok(_mapper.Map<DeliveryManCurrentLocationDto>(newLocation));
        }

        [EnableCors("AllowAll")]
        [HttpPost("location/delete")]
        public ActionResult<DeliveryManCurrentLocationDto> DeleteDeliveryManCurrentLocation([FromBody]CurrentLocationToDeleteDto location)
        {
            var deliveryMan = deliveryManService.GetDeliveryManById(location.DeliveryManId);
            if (deliveryMan == null)
            {
                return NotFound();
            }

            var deliveryManCurrentLocation = currentLocationService.GetDeliveryManCurrentLocation(location.DeliveryManId);
            if (deliveryManCurrentLocation == null)
            {
                return NotFound();
            }

            var deletedLocation = currentLocationService.DeleteDeliveryManCurrentLocation(deliveryManCurrentLocation);
            return Ok(_mapper.Map<DeliveryManCurrentLocationDto>(deletedLocation));
        }
    }
}