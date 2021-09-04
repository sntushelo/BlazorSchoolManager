using BlazorSchoolManager.Application.Interfaces.Repositories;
using BlazorSchoolManager.Domain.Entities;

namespace BlazorSchoolManager.Infrastructure.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly IRepositoryAsync<Teacher, int> _repository;

        public TeacherRepository(IRepositoryAsync<Teacher, int> repository)
        {
            _repository = repository;
        }
    }
}