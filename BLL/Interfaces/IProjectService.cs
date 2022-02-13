using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto> AddProject(ProjectCreateDto project);
        Task<ICollection<ProjectDto>> GetAllProjects();
        Task<ProjectDto> GetProject(int id);
        Task<ProjectDto> UpdateProject(int id, ProjectDto item);
        Task DeleteProject(int id);
    }
}
