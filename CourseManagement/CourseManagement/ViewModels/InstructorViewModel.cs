using CourseManagement.Models;
using X.PagedList;

namespace CourseManagement.ViewModels
{
    public class InstructorViewModel
    {
        public IPagedList<Instructor> Instructors { get; set; }
    }
}
