namespace devhouse.Models
{
    public class Team
    {
        public Team(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Developer>? Developers { get; set; }
        public virtual ICollection<Project>? Projects { get; set; }
    }

}