using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks_API.CustomActionFilters;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.DTO;
using NZWalks_API.Repositories;

namespace NZWalks_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   

    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext2, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext2;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        //Read Function--------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Reader, Writer")]

        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQeury,
            [FromQuery] string? sortBy, [FromQuery] bool isAscendong, [FromQuery] int pageNumber = 1 , [FromQuery] int pageSize=10)
        {
            var regionsDomain = await regionRepository.GetAllAsync(filterOn, filterQeury, sortBy, isAscendong, pageNumber, pageSize);

            if (regionsDomain != null) {

                //var regionDto = new List<RegionDto>();
                //foreach (var regiondomain in regionsDomain)
                //{
                //    regionDto.Add(new RegionDto()
                //    {
                //        Id = regiondomain.Id,
                //        Name = regiondomain.Name,
                //        Code = regiondomain.Code,
                //        RegionImageUrl = regiondomain.RegionImageUrl
                //    });
                //}

                return Ok(mapper.Map<List<RegionDto>>(regionsDomain));

            }
            //Map Domain models to DTO

            return NotFound();
            
        }


        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader, Writer")]

        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {

            var regionDomain = await regionRepository.GetByIDAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map Domain models to DTO

            //var regionDto = new RegionDto
            //{
            //    Id = regionDomain.Id,
            //    Name = regionDomain.Name,
            //    Code = regionDomain.Code,
            //    RegionImageUrl = regionDomain.RegionImageUrl

            //};

            return Ok(mapper.Map<RegionDto>(regionDomain));

        }


        //Create Function--------------------------------------------

        [HttpPost]
        [ValidateModelAttributes]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> AddItem([FromBody] AddRegionRequestDto addDetailsDto)
        {
            //Map DTO to Domain Model

            //var regionDomainModel = new Region
            //{
            //    Code = addDetailsDto.Code,
            //    Name = addDetailsDto.Name,
            //    RegionImageUrl = addDetailsDto.RegionImageUrl

            //};

                var regionDomainModel = mapper.Map<Region>(addDetailsDto);

                //Use domain model to create Region
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                //Map Domain model to DTO

                //var regionDto = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Name = regionDomainModel.Name,
                //    Code = regionDomainModel.Code,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl

                //}; ;

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetByID), new { id = regionDto.Id }, regionDto);
            }


        //Update Function-------------------------------------------

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]

        public async  Task<IActionResult> ChangeByID([FromRoute] Guid id, [FromBody] ChangeRequestDTO changeRequestDto)
        {
            //Map DTO to DomainModel
            //var regionDomainModel = new Region
            //{
            //    Code = changeRequestDto.Code,
            //    Name = changeRequestDto.Name,
            //    RegionImageUrl = changeRequestDto.RegionImageUrl
            //};

            if (ModelState.IsValid)
            {
                var regionDomainModel = mapper.Map<Region>(changeRequestDto);

                //Check If Region exists

                regionDomainModel = await regionRepository.ChangeByIDAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                //Map DomainModel to DTO

                //var regionDto = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Name = regionDomainModel.Name,
                //    Code = regionDomainModel.Code,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl
                //};

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return Ok(regionDto);
            }

            return BadRequest(ModelState);
        }


        //Delete Function------------------------------------------

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.DeleteAsync(id);

            if(regionDomain == null)
            {
                return NotFound();
            }

            return NoContent();

        }
    }

}

