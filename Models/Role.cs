namespace devhouse.Models
{
    public class Role
    {
        public Role(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Developer>? Developers { get; set; }
    }
}