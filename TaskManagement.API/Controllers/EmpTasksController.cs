using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.CustomActionFilters;
using TaskManagement.API.Models.Domain;
using TaskManagement.API.Models.DTO.EmpTaskDto;
using TaskManagement.API.Repositories;

namespace TaskManagement.API.Controllers
{
    // https://localhost:portnumber/api/emptasks
    [Route("api/[controller]")]
    [ApiController]
    public class EmpTasksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IEmpTaskRepository empTaskRepository;

        public EmpTasksController(IMapper mapper, IEmpTaskRepository empTaskRepository)
        {
            this.mapper = mapper;
            this.empTaskRepository = empTaskRepository;
        }

        // GET ALL EmpTasks
        // GET: https://localhost:portnumber/api/emptasks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var empTaskDomainModel = await empTaskRepository.GetAllAsync();

            return Ok(mapper.Map<List<EmpTaskDto>>(empTaskDomainModel));
        }

        // GET SINGLE EmpTask (By Id)
        // GET: https://localhost:portnumber/api/emptasks/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var empTaskDomainModel = await empTaskRepository.GetByIdAsync(id);

            if (empTaskDomainModel == null) { return NotFound(); }

            return Ok(mapper.Map<EmpTaskDto>(empTaskDomainModel));
        }

        // GET ALL EmpTask (By Assigned to)
        // GET: https://localhost:portnumber/api/emptasks/{name}
        [HttpGet]
        [Route("/api/EmpTasks/name/{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var empTaskDomainModel = await empTaskRepository.GetByEmpNameAsync(name);

            if (empTaskDomainModel == null) { return NotFound("No Tasks found."); }

            return Ok(mapper.Map<List<EmpTaskDto>>(empTaskDomainModel));
        }

        // GET ALL EmpTask due in {days}
        // GET: https://localhost:portnumber/api/due/{days}
        [HttpGet]
        [Route("/api/EmpTasks/due/{days:int}")]
        public async Task<IActionResult> GetByDays([FromRoute] int days)
        {
            var empTaskDomainModel = await empTaskRepository.GetByDays(days);

            if (empTaskDomainModel == null) { return NotFound("No Tasks due in a week."); }

            return Ok(mapper.Map<List<EmpTaskDto>>(empTaskDomainModel));
        }

        // GET ALL EmpTask (By status)
        // GET: https://localhost:portnumber/api/emptasks/{status}
        [HttpGet]
        [Route("/api/EmpTasks/Status/{status}")]
        [ValidateModel]
        public async Task<IActionResult> GetByStatus([FromRoute] string status)
        {
            var empTaskDomainModel = await empTaskRepository.GetByStatusAsync(status);

            if (empTaskDomainModel == null) { return NotFound(); }

            return Ok(mapper.Map<List<EmpTaskDto>>(empTaskDomainModel));
        }

        // GET SINGLE EmpTask (By teamId)
        // GET: https://localhost:portnumber/api/emptasks/{teamId}
        [HttpGet]
        [Route("/api/EmpTasks/Team/{teamId:guid}")]
        public async Task<IActionResult> GetByName([FromRoute] Guid teamId)
        {
            var empTaskDomainModel = await empTaskRepository.GetByTeamAsync(teamId);

            if (empTaskDomainModel == null) { return NotFound(); }

            return Ok(mapper.Map<List<EmpTaskDto>>(empTaskDomainModel));
        }

        // POST To create Employee task
        // POST: https://localhost:portnumber/api/emptasks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddEmpTaskRequestDto addEmpTaskRequestDto)
        {
            var empTaskDomainModel = mapper.Map<EmpTask>(addEmpTaskRequestDto);

            await empTaskRepository.CreateAsync(empTaskDomainModel);

            return Ok(mapper.Map<EmpTaskDto>(empTaskDomainModel));
        }

        // PUT To Update Employee task
        // PUT: https://localhost:portnumber/api/emptasks/{id}
        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateEmpTaskRequestDto updateEmpTaskRequestDto)
        {
            var empTaskDomainModel = mapper.Map<EmpTask>(updateEmpTaskRequestDto);

            empTaskDomainModel = await empTaskRepository.UpdateAsync(id, empTaskDomainModel);

            if (empTaskDomainModel == null) { return NotFound(); }

            return Ok(mapper.Map<EmpTaskDto>(empTaskDomainModel));
        }

        // DELETE Employee Task
        // DELETE: https://localhost:portnumber/api/emptasks/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var empTaskDomainModel = await empTaskRepository.DeleteAsync(id);

            if ( empTaskDomainModel == null) { return NotFound(); }

            return Ok(mapper.Map<EmpTaskDto>(empTaskDomainModel));
        }
    }
}
