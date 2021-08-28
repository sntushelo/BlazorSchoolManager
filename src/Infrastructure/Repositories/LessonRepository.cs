using BlazorSchoolManager.Application.Interfaces.Repositories;
using BlazorSchoolManager.Domain.Entities;

namespace BlazorSchoolManager.Infrastructure.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly IRepositoryAsync<Lesson, int> _repository;

        public LessonRepository(IRepositoryAsync<Lesson, int> repository)
        {
            _repository = repository;
        }
    }
}