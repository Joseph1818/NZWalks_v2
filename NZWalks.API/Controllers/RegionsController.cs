using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        public readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        // Construction Injection
        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        //localhost:portNumber/api/Regions 
        [HttpGet]
        public async Task <IActionResult> GetAllRegions()
        {
            // Fetch data from database - Regions table Domain model
            var regionDomains = await regionRepository.GetAllAsync();

            // Map Domain to DTO
            var regionsDto = new List<RegionDto>();

            foreach (var regionDomain in regionDomains)
            {
                regionsDto.Add(new RegionDto
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });

            }
            return Ok(regionsDto);

        }

        // GET SINGLE REGION
        [HttpGet]
        [Route("{id:guid}")]

        public async Task <IActionResult> GetRegionById([FromRoute] Guid id)
        {
            // Fetch data from database - Regions table Domain model
            //var regionDomains = await dbContext.regions.FindAsync(id);
            var regionDomains = await regionRepository.GetRegionByI(id);

            if (regionDomains == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto
            {
                Id = regionDomains.Id,
                Name = regionDomains.Name,
                Code = regionDomains.Code,
                RegionImageUrl = regionDomains.RegionImageUrl
            };

            //FirstOfDefault opiton

            return Ok(regionDto);
        }

        //POST/CREATE REGION
        // POST: localhost:portnumber/api/Regions

        [HttpPost]
        public async Task <IActionResult> AddRegion([FromBody] AddRequestRegionDto addRequestRegion)
        {
            //Map or convert DTO to Domain model
            var regionDomainModel = new Region
            {
                Name = addRequestRegion.Name,
                Code = addRequestRegion.Code,
                RegionImageUrl = addRequestRegion.RegionImageUrl
            };
            // Use domain to create region in database
            await regionRepository.AddAsync(regionDomainModel);

            // Map Domain back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDomainModel.Id }, regionDomainModel);
        }

        //UPDATE REGION
        // PUT: localhost:portnumber/api/Regions/{id}

        [HttpPut]
        [Route("{id:guid}")]
        public async Task <IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRequestRegionDto updateRequestRegion)
        {
            //check if region exists
            var regionDomainModel = await regionRepository.UpdateAsync(id, new Region
            {
                Name = updateRequestRegion.Name,
                Code = updateRequestRegion.Code,
                RegionImageUrl = updateRequestRegion.RegionImageUrl
            });

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Convert Domain back to DTO
            var regionDto = new RegionDto
            {
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);

        }

        // DELETE REGION
        // DELETE: localhost:portnumber/api/Regions/{id}
        [HttpDelete]
        [Route("{id:guid}")]

        public async Task <IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel =  await regionRepository.DeleteAsync(id);
            
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            // Convert Domain back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);

        }
    }
}
