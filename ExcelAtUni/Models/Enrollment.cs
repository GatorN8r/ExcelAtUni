namespace ExcelAtUni.Models
{
    public class Enrollment
    {
        public Enrollment()
        {

        }
        public Enrollment(DateTime enrollmentDate)
        {
            EnrollmentDate = enrollmentDate;
        }

        public DateTime EnrollmentDate { get; set; }
    }
}
