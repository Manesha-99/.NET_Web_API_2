using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.DTO;
using NZWalks_API.Repositories;

namespace NZWalks_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(NZWalksDbContext dbContext, IMapper mapper, IWalkRepository walkRepository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] AddWalksRequestDto addWalksRequestDto)
        {
            //DTO map to Domain Model

            //var walkDomainModel = new Walk()
            //{
            //    Name = addWalksRequestDtocs.Name,
            //    Description = addWalksRequestDtocs.Description,
            //    LengthInKm = addWalksRequestDtocs.LengthInKm,
            //    WalkImageUrl = addWalksRequestDtocs.WalkImageUrl,
            //    DifficultyId = addWalksRequestDtocs.DifficultyId,
            //    RegionId = addWalksRequestDtocs.RegionId,
            //};

            var walkDomainModel = mapper.Map<Walk>(addWalksRequestDto);

            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

            //Domain model map to DTO

            var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = walkDto.Id }, walkDto);
        }

        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var walkDomainModel = await walkRepository.GetAllAsync();

            if (walkDomainModel != null)
            {
                //Domain Model Map to DTO
                var dtoModel = mapper.Map<List<WalkDto>>(walkDomainModel);

                return Ok(dtoModel);

            }

            return NotFound();

        }

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var domainWalkModel = await walkRepository.GetByIdAsync(id);

            if (domainWalkModel != null)
            {
                //Domain Model Map to DTO

                return Ok(mapper.Map<WalkDto>(domainWalkModel));
            }

            return NotFound();
        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> ChangeByID([FromRoute] Guid id, [FromBody] ChangeWalkDto changeWalkDto)
        {
            //DTO map to DomainWalkModel

            var domainWalkModel = mapper.Map<Walk>(changeWalkDto);

            domainWalkModel = await walkRepository.UpdateById(id, domainWalkModel);
            if (domainWalkModel == null)
            {
                return NotFound();
            }

            //Domain Model to DTO

            var walkDto = mapper.Map<WalkDto>(domainWalkModel);
            return Ok(walkDto);

        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteByID(Guid id)
        {
            var domainWalkModel = await walkRepository.DeleteByIdAsync(id);
            if (domainWalkModel != null)
            { 
                return NoContent();
            }
            return NotFound();



        }
    }

}
