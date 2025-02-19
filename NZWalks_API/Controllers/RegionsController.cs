using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();

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

        [HttpPost]

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


        [HttpPut]
        [Route("{id:Guid}")]

        public async  Task<IActionResult> ChangeByID([FromRoute] Guid id, [FromBody] ChangeRequestDTO changeRequestDto)
        {
            //Map DTO to DomainModel
            //var regionDomainModel = new Region
            //{
            //    Code = changeRequestDto.Code,
            //    Name = changeRequestDto.Name,
            //    RegionImageUrl = changeRequestDto.RegionImageUrl
            //};
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


        [HttpDelete]
        [Route("{id:guid}")]

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

