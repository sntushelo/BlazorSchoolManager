using BlazorSchoolManager.Domain.Contracts;
using System.Collections.Generic;

namespace BlazorSchoolManager.Domain.Entities
{
    public class Venue : AuditableEntity<int>
    {
        public Venue()
        {
            //Lessons = new HashSet<Lesson>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageDataURL { get; set; }
        public int Capacity { get; set; }
        public bool IsOnline { get; set; }

        //public ICollection<Lesson> Lessons { get; set; }
    }
}
