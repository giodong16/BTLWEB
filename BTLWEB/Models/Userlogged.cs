namespace BTLWEB.Models
{
    public class Userlogged
    {
        public int TeacherId { get; set; }

        public string? UserName { get; set; }

        public string? Name { get; set; }

        public DateTime? Dob { get; set; }

        public string? Gender { get; set; }

        public long? Mobile { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }
        public int? TotalClasses { get; set;}
        public int? TotalSubjects { get; set; }

    }
}
