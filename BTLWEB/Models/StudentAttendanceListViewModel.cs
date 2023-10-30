namespace BTLWEB.Models
{
    public class StudentAttendanceListViewModel
    {
        public List<StudentAttendance> studentAttendances { get; set; }
        public PaginationViewModel Pagination { get; set; }
    }
}
