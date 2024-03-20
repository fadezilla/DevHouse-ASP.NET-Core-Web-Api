namespace devhouse.Models
{
    public class ProjectType
    {
        public ProjectType(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Project>? Projects { get; set; }
    }
}