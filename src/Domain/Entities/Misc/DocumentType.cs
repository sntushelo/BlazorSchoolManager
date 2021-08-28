using BlazorSchoolManager.Domain.Contracts;

namespace BlazorSchoolManager.Domain.Entities.Misc
{
    public class DocumentType : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}