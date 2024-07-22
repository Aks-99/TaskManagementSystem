using AutoMapper;
using TaskManagement.API.Models.Domain;
using TaskManagement.API.Models.DTO.DocumentDto;
using TaskManagement.API.Models.DTO.EmpTaskDto;
using TaskManagement.API.Models.DTO.NoteDto;
using TaskManagement.API.Models.DTO.TeamDto;

namespace TaskManagement.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Teams mapper
            CreateMap<Team, TeamDto>().ReverseMap();
            CreateMap<AddTeamRequestDto, Team>().ReverseMap();
            CreateMap<UpdateTeamRequestDto, Team>().ReverseMap();

            // EmpTasks mapper
            CreateMap<EmpTask, EmpTaskDto>().ReverseMap();
            CreateMap<AddEmpTaskRequestDto, EmpTask>().ReverseMap();
            CreateMap<UpdateEmpTaskRequestDto, EmpTask>().ReverseMap();

            // Notes mapper
            CreateMap<Note, NoteDto>().ReverseMap();
            CreateMap<AddNoteRequestDto, Note>().ReverseMap();
            CreateMap<UpdateNoteRequestDto, Note>().ReverseMap();

            // Document mapper
            CreateMap<Document, DocumentDto>().ReverseMap();

        }
    }
}
