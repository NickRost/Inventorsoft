using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITaskService
    {
        Task<TaskDto> AddTask(TaskCreateDto task);
        Task<ICollection<TaskDto>> GetAllTasks();
        Task<TaskDto> GetTask(int id);
        Task<TaskDto> UpdateTask(int id, TaskDto task);
        Task DeleteTask(int id);
    }
}
