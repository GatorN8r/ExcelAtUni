namespace ExcelAtUni.Models
{
    public class Qualification
    {
        public Qualification()
        {

        }
        public Qualification(int id, string name, string description, int qualificationType)
        {
            Id = id;
            Name = name;
            Description = description;
            QualificationType = qualificationType;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int QualificationType { get; set; }
    }
}
