using BlazorSchoolManager.Application.Interfaces.Services;
using System;

namespace BlazorSchoolManager.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}