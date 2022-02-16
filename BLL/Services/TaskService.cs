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
    public class TaskService : ITaskService
    {
        protected readonly ProjectContext _context;
        protected readonly IMapper _mapper;

        public TaskService(ProjectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskDto> AddTask(TaskCreateDto task)
        {
            var item = _mapper.Map<DAL.Models.Task>(task);
            item.CreatedAt = DateTime.Now;
            await _context.Tasks.AddAsync(item);
            await _context.SaveChangesAsync();

            var created = await _context.Tasks.Include(x => x.Performer).Include(t => t.Project).FirstAsync(u => u.Id == item.Id);

            return _mapper.Map<TaskDto>(created);
        }

        public async System.Threading.Tasks.Task DeleteTask(int id)
        {
            DAL.Models.Task task = _mapper.Map<DAL.Models.Task>(await GetTask(id));
            if (task == null)
                throw new System.Exception("Invalid id");

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<TaskDto>> GetAllTasks()
        {
            ICollection<DAL.Models.Task> projects = await _context.Tasks.Include(x => x.Performer).Include(t => t.Project).ToListAsync();
            var result = _mapper.Map<ICollection<TaskDto>>(projects);
            return result;
        }

        public async Task<TaskDto> GetTask(int id)
        {
            var result = await _context.Tasks.Include(x => x.Performer).Include(t => t.Project).FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<TaskDto>(result);
        }

        public async Task<TaskDto> UpdateTask(int id, TaskDto item)
        {
            Project project = _mapper.Map<Project>(await GetTask(id));
            if (project == null)
                throw new System.Exception("Invalid id");
            project.Name = item.Name;
            project.Description = item.Description;
            await _context.SaveChangesAsync();
            return _mapper.Map<TaskDto>(project);
        }
    }
}
