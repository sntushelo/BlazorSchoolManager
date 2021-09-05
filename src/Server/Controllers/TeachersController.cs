using BlazorSchoolManager.Application.Features.Teachers.Queries;
using BlazorSchoolManager.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorSchoolManager.Server.Controllers
{
    public class TeachersController : BaseApiController<TeachersController>
    {
        /// <summary>
        /// Get All Teachers
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Teachers.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString, string orderBy = null)
        {
            var teachers = await _mediator.Send(new GetTeachersQuery(pageNumber, pageSize, searchString, orderBy));
            return Ok(teachers);
        }
    }
}
