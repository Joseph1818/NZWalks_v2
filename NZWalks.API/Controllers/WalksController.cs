using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilter;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;

namespace NZWalks.API.Controllers
{
    // localhost:portNumber/api/Walks
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalksController : ControllerBase
    {
        private readonly NZWalksDbContext dbcontext;
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        public WalksController(NZWalksDbContext dbContext, IMapper mapper, IWalkRepository walkRepository)
        {
            this.dbcontext = dbContext;
            this.mapper = mapper;
            this.walkRepository = walkRepository;

        }

        // POST WALKS 

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddWalkAsync([FromBody] AddRequestWalkDto addRequestWalk)
        {
                // Map DTO to Domain Model Without mapper 
                var walkDomain = mapper.Map<Walk>(addRequestWalk);

                await walkRepository.CreateAsync(walkDomain);

                // Map Domain Model to DTO
                var walkDto = mapper.Map<WalkDto>(walkDomain);
                return Ok(walkDto);

        }

        // Get Walks
        // GET: localhost:portNumber/api/Walks?filterOn=Name&filterQuery=Track&SortBy=Name&isAscending=true&PageNumber=1&PageSize=1000
        // Added Filtering Parameters

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync(
            [FromQuery] string? filterOn,[FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int? pageNumber = 1, [FromQuery] int? pageSize = 1000)
        {
            var walksDomain = await walkRepository.GetAllAsync(filterOn,filterQuery, sortBy, isAscending ?? true);

            return Ok(mapper.Map<List<WalkDto>>(walksDomain));
        }


        [HttpGet]
        [Route("{id:guid}")]
        public async Task <IActionResult> GetWalkByIdAsync(Guid id)
        {
            var walkDomain = await walkRepository.GetByIdAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(walkDomain);
            return Ok(walkDto);
        }

        [HttpPut]
        [ValidateModel]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] UpdateRequestWalkDto updateRequestWalk)
        {

         
                // Convert DTO to Domain Model
                var walkDomain = mapper.Map<Walk>(updateRequestWalk);
                // Update Walk using repository

                walkDomain = await walkRepository.UpdateAsync(id, walkDomain);
                // If null then NotFound
                if (walkDomain == null)
                {
                    return NotFound();
                }
                // Convert Domain back to DTO
                var walkDto = mapper.Map<WalkDto>(walkDomain);
                // Return Ok response
                return Ok(walkDto);

        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var walkDomain = await walkRepository.DeleteAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(walkDomain);
            return Ok(walkDto);
        }

    }
}
