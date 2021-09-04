using System.ComponentModel;

namespace BlazorSchoolManager.Application.Enums
{
    public enum UploadType : byte
    {
        [Description(@"Images\Students")]
        Student,

        [Description(@"Images\Teachers")]
        Teacher,

        [Description(@"Images\Venues")]
        Venue,

        [Description(@"Images\Lessons")]
        Lesson,

        [Description(@"Images\Products")]
        Product,

        [Description(@"Images\ProfilePictures")]
        ProfilePicture,

        [Description(@"Documents")]
        Document
    }
}