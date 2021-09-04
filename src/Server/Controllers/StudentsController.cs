using BlazorSchoolManager.Application.Features.Students.Queries;
using BlazorSchoolManager.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorSchoolManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : BaseApiController<StudentsController>
    {
        /// <summary>
        /// Get All Students
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Students.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString, string orderBy = null)
        {
            var venues = await _mediator.Send(new GetStudentsQuery(pageNumber, pageSize, searchString, orderBy));
            return Ok(venues);
        }
    }
}