public class TeamDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required List<DeveloperDTO> Developers { get; set; }
}