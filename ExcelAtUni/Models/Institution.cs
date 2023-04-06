namespace ExcelAtUni.Models
{
    public class Institution
    {
        public Institution()
        {

        }
        public Institution(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
