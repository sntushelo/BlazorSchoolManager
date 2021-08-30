using BlazorSchoolManager.Application.Features.Venues.Queries;
using BlazorSchoolManager.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorSchoolManager.Server.Controllers
{
    public class VenuesController : BaseApiController<VenuesController>
    {
        /// <summary>
        /// Get All Venues
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Venues.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString, string orderBy = null)
        {
            var venues = await _mediator.Send(new GetVenuesQuery(pageNumber, pageSize, searchString, orderBy));
            return Ok(venues);
        }

        /**
        /// <summary>
        /// Get a Venue Image by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        
        [Authorize(Policy = Permissions.Venues.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVenue(int id)
        {
            var result = await _mediator.Send(new GetVenueQuery(id));
            return Ok(result);
        }
        **/
    }
}
