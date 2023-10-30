namespace BTLWEB.Models
{
    public class TeacherAttendanceListViewModel
    {
        public List<TeacherAttendance> teacherAttendances { get; set; }
        public PaginationViewModel Pagination { get; set; }
    }
}
