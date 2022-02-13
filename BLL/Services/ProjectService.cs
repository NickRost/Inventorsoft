using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL.Services
{
    public class ProjectService : IProjectService
    {
        protected readonly ProjectContext _context;
        protected readonly IMapper _mapper;

        public ProjectService(ProjectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProjectDto> AddProject(ProjectCreateDto project)
        {
            var item = _mapper.Map<Project>(project);
            item.CreatedAt = DateTime.Now;
            await _context.Projects.AddAsync(item);
            await _context.SaveChangesAsync();

            var created = await _context.Projects.Include(x => x.Author).Include(t => t.Team).FirstAsync(u => u.Id == item.Id);

            return _mapper.Map<ProjectDto>(created);
        }

        public async System.Threading.Tasks.Task DeleteProject(int id)
        {
            Project project = _mapper.Map<Project>(await GetProject(id));
            if (project == null)
                throw new System.Exception("Invalid id");

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<ProjectDto>> GetAllProjects()
        {
            ICollection<Project> projects = await _context.Projects.Include(x => x.Author).Include(t => t.Team).ToListAsync();
            var result = _mapper.Map<ICollection<ProjectDto>>(projects);
            return result;
        }

        public async Task<ProjectDto> GetProject(int id)
        {
            var result = await _context.Projects.Include(x => x.Author).Include(t => t.Team).FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<ProjectDto>(result);
        }

        public async Task<ProjectDto> UpdateProject(int id, ProjectDto item)
        {
            Project project = _mapper.Map<Project>(await GetProject(id));
            if (project == null)
                throw new System.Exception("Invalid id");
            project.Name = item.Name;
            project.Description = item.Description;
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectDto>(project);
        }
    }
}
