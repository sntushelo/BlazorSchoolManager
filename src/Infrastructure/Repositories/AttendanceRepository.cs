using BlazorSchoolManager.Application.Interfaces.Repositories;
using BlazorSchoolManager.Domain.Entities;

namespace BlazorSchoolManager.Infrastructure.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly IRepositoryAsync<Attendance, int> _repository;

        public AttendanceRepository(IRepositoryAsync<Attendance, int> repository)
        {
            _repository = repository;
        }
    }
}
