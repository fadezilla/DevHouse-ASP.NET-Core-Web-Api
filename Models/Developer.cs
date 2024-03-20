namespace devhouse.Models
{
    public class Developer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int RoleId { get; set; }
        public Role? Role { get; set; }

        public int TeamId { get; set; }
        public Team? Team { get; set; }

    }
}