using CourseManagement.Models;
using X.PagedList;

namespace CourseManagement.ViewModels
{
    public class CourseViewModel
    {
        public IPagedList<Course> Courses { get; set; }
    }
}
