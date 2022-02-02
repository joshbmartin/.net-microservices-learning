using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;

namespace PlatformService.Controllers
{
    //here we're setting a route and using a wildcard of [controller] which gives us just the portion of the controllers name on our class Platforms. Or we can do it like this: [Route("api/platforms")] its up to you
    [Route("api/[controller]")]
    //api controller decoration gives us some out of the box functionality
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        //inject in our platform repo & request an instance of IMapper which is our automapper stuff. repository/mapper are being injected and we want to assign them to private fields that we can use inside our class.
        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            //assign private read-only values to our injected params.
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms...");
            var platformItem = _repository.GetAllPlatforms();
            //we're going to use automapper to map our models to our read dtos and return that back 
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
        }
        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platformItem = _repository.GetPlatformById(id);
            if (platformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }
            return NotFound();
        }
    }
}