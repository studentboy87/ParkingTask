using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingTask;
using ParkingTask.Enums;

namespace ParkingAPI.Controllers
{
    [ApiController]
    [Route("api/Parking")]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpGet]
        [Route("GetAllVacantParkingSpaces")]
        public OkObjectResult GetAllVacantParkingSpaces()
        {
            try
            {
                var spaces = _parkingService.GetAllVacantParkingSpaces();
                return Ok(spaces);
            }
            catch (Exception e)
            {
                var reason = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    ReasonPhrase = e.Message
                };
                return new OkObjectResult(reason);
            }

        }

        [HttpGet]
        [Route("GetAllPossibleVacantParkingSpaces")]
        public OkObjectResult GetAllPossiblePlaneParkingSpaces(int planeType)
        {
            try
            {
                var spaces = _parkingService.GetAllPossibleVacantParkingSpaces((PlaneType) planeType);
                return Ok(spaces);
            }
            catch (Exception e)
            {
                var reason = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    ReasonPhrase = e.Message
                };
                return new OkObjectResult(reason);
            }

        }

        [HttpGet]
        [Route("GetAllVacantParkingSpacesForType")]
        public OkObjectResult GetAllVacantParkingSpacesForType(int planeType)
        {
            try
            {
                var spaces = _parkingService.GetAllVacantParkingSpacesForType((PlaneType) planeType);
                return Ok(spaces);
            }
            catch (Exception e)
            {
                var reason = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    ReasonPhrase = e.Message
                };
                return new OkObjectResult(reason);
            }
        }

        [HttpGet]
        [Route("GetFirstPossibleParkingSpace")]
        public OkObjectResult GetFirstPossibleParkingSpace(int planeType)
        {
            try
            {
                var space = _parkingService.GetFirstPossiblePlaneParkingSpace((PlaneType) planeType);
                return Ok(space);
            }
            catch (Exception e)
            {
                var reason = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    ReasonPhrase = e.Message
                };
                return new OkObjectResult(reason);
            }
        }

        [HttpPut]
        [Route("ParkPlaneInFirstSpotAvailable")]
        public OkObjectResult ParkPlaneInFirstPossibleSpace(int planeType)
        {
            try
            {
                _parkingService.GetFirstPossiblePlaneParkingSpace((PlaneType) planeType);
                return new OkObjectResult(Ok());
            }
            catch (Exception e)
            {
                var reason = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    ReasonPhrase = e.Message
                };
                return new OkObjectResult(reason);
            }


        }
    }
}
