using BlazorSchoolManager.Application.Features.Attendance.Queries;
using BlazorSchoolManager.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorSchoolManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : BaseApiController<AttendancesController>
    {
        /// <summary>
        /// Get All Attendance
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Attendance.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString, string orderBy = null)
        {
            var Attendance = await _mediator.Send(new GetAttendanceQuery(pageNumber, pageSize, searchString, orderBy));
            return Ok(Attendance);
        }
    }
}