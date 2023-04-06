namespace ExcelAtUni.Dtos
{
    public class GetStudentDetailsDto
    {

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string IdNumber { get; set; }
        public string Institution { get; set; }
        public string Qualification { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime GraduationDay { get; set; }
        public int yearsAtVarsity { get; set; }
    }
}