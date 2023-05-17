using Microsoft.AspNetCore.Mvc;
using first_web_api.Models.Domain;
using first_web_api.Models.DTO;
using first_web_api.Repositories;
using AutoMapper;

namespace first_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IWalkReposotory walkReposotory;
        private readonly IMapper mapper;

        public WalkController(IWalkReposotory walkReposotory, IMapper mapper)
        {
            this.walkReposotory = walkReposotory;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            Walk newWalk = mapper.Map<Walk>(addWalkRequestDto);

            Walk populatedWalk = await walkReposotory.CreateAsync(newWalk);

            WalkDto walkDto = mapper.Map<WalkDto>(populatedWalk);

            return Ok(walkDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Walk> walkEntities = await walkReposotory.GetAllAsync();

            List<WalkDto> walkDtos = mapper.Map<List<WalkDto>>(walkEntities);

            return Ok(walkDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            Walk? walkEntity = await walkReposotory.GetByIdAsync(id);

            if (walkEntity == null)
            {
                return NotFound();
            }

            WalkDto walkDto = mapper.Map<WalkDto>(walkEntity);

            return Ok(walkDto);

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            Walk walkUpdateModel = mapper.Map<Walk>(updateWalkRequestDto);
            Walk? walkEntity = await walkReposotory.UpdateAsync(id, walkUpdateModel);

            if (walkEntity == null)
            {
                return NotFound();
            }

            WalkDto walkDto = mapper.Map<WalkDto>(walkEntity);

            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Walk? walkEntity = await walkReposotory.DeleteAsync(id);

            if (walkEntity == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}