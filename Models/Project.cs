namespace devhouse.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ProjectTypeId { get; set; }
        public ProjectType? ProjectType { get; set; }
        
        public int TeamId { get; set; }
        public Team? Team { get; set; }
    }
}