using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Models.Domain;
using TaskManagement.API.Models.DTO.TeamDto;
using TaskManagement.API.Repositories;

namespace TaskManagement.API.Controllers
{

    // https://localhost:portnumber/api/teams
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamRepository teamRepository;
        private readonly IMapper mapper;

        public TeamsController(ITeamRepository teamRepository, IMapper mapper)
        {
            this.teamRepository = teamRepository;
            this.mapper = mapper;
        }

        // GET ALL TEAMS
        // GET: https://localhost:portnumber/api/teams
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teamDomainModel = await teamRepository.GetAllAsync();

            return Ok(mapper.Map<List<TeamDto>>(teamDomainModel));
        }

        // GET SINGLE TEAM (By Id)
        // GET: https://localhost:portnumber/api/teams/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var teamDomainModel = await teamRepository.GetByIdAsync(id);

            if (teamDomainModel == null) { return NotFound("Team does not exist!"); }

            return Ok(mapper.Map<TeamDto>(teamDomainModel));
        }

        // POST To create new Team
        // POST: https://localhost:portnumber/api/teams
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddTeamRequestDto addTeamRequestDto)
        {
            var teamDomainModel = mapper.Map<Team>(addTeamRequestDto);

            teamDomainModel = await teamRepository.CreateAsync(teamDomainModel);

            var teamDto = mapper.Map<TeamDto>(teamDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = teamDto.Id }, teamDto);
        }

        // PUT To update Team
        // PUT: https://localhost:portnumber/api/teams/{id}
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTeamRequestDto updateTeamRequestDto)
        {
            var teamDomainModel = mapper.Map<Team>(updateTeamRequestDto);

            teamDomainModel = await teamRepository.UpdateAsync(id, teamDomainModel);

            if (teamDomainModel == null) { return NotFound(); }

            return Ok(mapper.Map<TeamDto>(teamDomainModel));
        }

        // DELETE To Delete Team
        // DELETE: https://localhost:portnumber/api/teams/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var teamDomainModel = await teamRepository.DeleteAsync(id);

            if (teamDomainModel == null)
            { return NotFound(); }

            return Ok(mapper.Map<TeamDto>(teamDomainModel));
        }
    }
}
