using BlazorSchoolManager.Application.Interfaces.Repositories;
using BlazorSchoolManager.Domain.Entities;

namespace BlazorSchoolManager.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IRepositoryAsync<Student, int> _repository;

        public StudentRepository(IRepositoryAsync<Student, int> repository)
        {
            _repository = repository;
        }
    }
}
