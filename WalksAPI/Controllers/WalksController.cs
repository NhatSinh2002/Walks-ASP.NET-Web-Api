using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Reflection.Metadata.Ecma335;
using WalksAPI.CustomActionFilters;
using WalksAPI.Models.Domain;
using WalksAPI.Models.DTO;
using WalksAPI.Repositories;

namespace WalksAPI.Controllers
{
    //api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        // CREATE WALK
        //POST 
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
                //Map DTO to Domain Model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
                await walkRepository.CreateAsync(walkDomainModel);
                //Map Domain Model to DTO
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            
        }

        //GET ALL WALKS
        //GET
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomainMode = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true,pageNumber,pageSize);
            return Ok(mapper.Map<List<WalkDto>>(walksDomainMode));
        } 

        //GET A WALK BY ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walksDomainMode = await walkRepository.GetByIdAsync(id); 
            if (walksDomainMode == null)
            {
                return NotFound();
            }
            //Map Domain Model to DTO
            return Ok(mapper.Map<WalkDto>(walksDomainMode));

        }

        //UPDATE WALK BY ID
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
                //Map DTO to Domain Model
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);
                if (walkDomainModel == null)
                {
                    return NotFound();
                }
                //Map Domain Model to DTO
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        //DELETE WALK BY ID
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteWalkDomainModel = await walkRepository.DeleteAsync(id);
            if(deleteWalkDomainModel == null)
            {
                return NotFound();  
            }
            //Map Domain Model to DTO
            return Ok(mapper.Map<WalkDto>(deleteWalkDomainModel));
        }
    }
}
