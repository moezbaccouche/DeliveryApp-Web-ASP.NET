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
    [Route("delivery-app/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClientService clientService;
        private readonly ILocationService locationService;

        public ClientsController(IMapper mapper, IClientService clientService, ILocationService locationService)
        {
            _mapper = mapper;
            this.clientService = clientService;
            this.locationService = locationService;
        }

        [EnableCors("AllowAll")]
        [HttpGet]
        public ActionResult<IEnumerable<ClientForProfileDto>> GetAllClients()
        {
            var allClients = clientService.GetAllClients();
            return Ok(_mapper.Map<IEnumerable<ClientForProfileDto>>(allClients));
        }

        [EnableCors("AllowCors")]
        [HttpGet("{clientId}", Name = "GetClient")]
        public ActionResult<ClientForProfileDto> GetClient(int clientId)
        {
            var client = clientService.GetClientById(clientId);
            if(client == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ClientForProfileDto>(client));
        }

        [EnableCors("AllowAll")]
        [HttpPost]
        public ActionResult<ClientForProfileDto> NewClient([FromBody] ClientForCreationDto newClient)
        {
            //Insert in the table location
            var location = locationService.AddLocation(newClient.Location);
            newClient.Location = location;

            var entityClient = _mapper.Map<Client>(newClient);

            //Insert the new user in the DB
            var addedClient = clientService.AddClient(entityClient);
            return CreatedAtAction("GetClient", new { clientId = addedClient.Id }, addedClient);
        }
    }
}