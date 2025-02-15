using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult GetAll()
        {
            var regionsDomain = dbContext.Regions.ToList();

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

        public IActionResult GetByID([FromRoute] Guid id)
        {

            var regionDomain = dbContext.Regions.FirstOrDefault(x=>x.Id==id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map Domain with Dto

            var regionDto = new RegionDto();
            regionDto.Id = regionDomain.Id;
            regionDto.Name = regionDomain.Name;
            regionDto.Code = regionDomain.Code;
            regionDto.RegionImageUrl = regionDomain.RegionImageUrl;

            return Ok(regionDto);

        }

        //[HttpPost]


    }

}

