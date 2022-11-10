using System.Collections;
using System.Collections.Generic;

namespace University.Models.SchoolViewModels
{
    public class InstructorIndexData
    {
        public IEnumerable<Instructor> Instructors { get; set; }
        //public IEnumerable<OfficeAssignment> OfficeAssignments { get; set; }
        //public IEnumerable<CourseAssignment> CourseAssignments { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}
