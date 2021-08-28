using BlazorSchoolManager.Domain.Entities;

namespace BlazorSchoolManager.Application.Features.Venues.Queries
{
    public static class VenueConverter
    {
        public static GetVenuesResponsePaged ToModel(Venue entity)
        {
            if (entity == null) return null;

            return new GetVenuesResponsePaged()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Capacity = entity.Capacity,
                IsOnline = entity.IsOnline
            };
        }

        public static void ToEntity(GetVenuesResponsePaged model, Venue entity)
        {
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Capacity = model.Capacity;
            entity.IsOnline = model.IsOnline;
        }
    }
}