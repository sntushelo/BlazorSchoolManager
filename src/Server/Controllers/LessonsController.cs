using BlazorSchoolManager.Application.Features.Lessons.Queries;
using BlazorSchoolManager.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorSchoolManager.Server.Controllers
{
    public class LessonsController : BaseApiController<LessonsController>
    {
        /// <summary>
        /// Get All Lessons
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Lessons.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString, string orderBy = null)
        {
            var lessons = await _mediator.Send(new GetLessonsQuery(pageNumber, pageSize, searchString, orderBy));
            return Ok(lessons);
        }

        /**
        /// <summary>
        /// Get a Lesson Image by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        
        [Authorize(Policy = Permissions.Lessons.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLesson(int id)
        {
            var result = await _mediator.Send(new GetLessonQuery(id));
            return Ok(result);
        }
        **/
    }
}
