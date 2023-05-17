using Microsoft.AspNetCore.Mvc;
using first_web_api.Models.Domain;
using first_web_api.Models.DTO;
using first_web_api.Repositories;
using AutoMapper;

namespace first_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionReposotory regionRepository;
        private readonly IMapper mapper;

        public RegionController(IRegionReposotory regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Region> regions = await regionRepository.GetAllAsync();

            List<RegionDto> regionsDto = mapper.Map<List<RegionDto>>(regions);

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            Region? region = await regionRepository.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            RegionDto regionDto = mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto newRegionDto)
        {
            Region newRegion = mapper.Map<Region>(newRegionDto);

            Region populatedRegion = await regionRepository.CreateAsync(newRegion);

            RegionDto regionDto = mapper.Map<RegionDto>(populatedRegion);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updatedRegionRequestDto)
        {
            Region newValuesRegion = mapper.Map<Region>(updatedRegionRequestDto);

            Region? regionEntity = await regionRepository.UpdateAsync(id, newValuesRegion);

            if (regionEntity == null)
            {
                return NotFound();
            }

            RegionDto updatedRegionDto = mapper.Map<RegionDto>(regionEntity);

            return Ok(updatedRegionDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Region? entityRegion = await regionRepository.DeleteAsync(id);
            if (entityRegion == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}