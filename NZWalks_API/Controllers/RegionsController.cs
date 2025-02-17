using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.DTO;

namespace NZWalks_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext2)
        {
            this.dbContext = dbContext2;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await dbContext.Regions.ToListAsync();

            if (regionsDomain != null) {

                var regionDto = new List<RegionDto>();
                foreach (var regiondomain in regionsDomain)
                {
                    regionDto.Add(new RegionDto()
                    {
                        Id = regiondomain.Id,
                        Name = regiondomain.Name,
                        Code = regiondomain.Code,
                        RegionImageUrl = regiondomain.RegionImageUrl
                    });
                }

                return Ok(regionDto);

            }
            //Map Domain models to DTO

            return NotFound();
            
        }


        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {

            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id==id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map Domain models to DTO

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl

            };

            return Ok(regionDto);

        }

        [HttpPost]

        public async Task<IActionResult> AddItem([FromBody] AddRegionRequestDto addDetailsDto)
        {
            //Map DTO to Domain Model

            var regionDomainModel = new Region
            {
                Code = addDetailsDto.Code,
                Name = addDetailsDto.Name,
                RegionImageUrl = addDetailsDto.RegionImageUrl

            };

            //Use domain model to create Region
            await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync();

            //Map Domain model to DTO

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl

            }; ;

            return CreatedAtAction(nameof(GetByID), new { id = regionDto.Id }, regionDto);

        }


        [HttpPut]
        [Route("{id:Guid}")]

        public async  Task<IActionResult> ChangeByID([FromRoute] Guid id, [FromBody] ChangeRequestDTO changeRequestDto)
        {

            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }


            //Map DTO to DomainModel

            regionDomain.Code = changeRequestDto.Code;
            regionDomain.Name = changeRequestDto.Name;
            regionDomain.RegionImageUrl= changeRequestDto.RegionImageUrl;


            //Use Domain Model to change Region
            await dbContext.SaveChangesAsync();

            //Map DomainModel to DTO

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);
        }


        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(regionDomain == null)
            {
                return NotFound();
            }

            dbContext.Regions.Remove(regionDomain);
            await dbContext.SaveChangesAsync();

            return NoContent();

        }
    }

}

