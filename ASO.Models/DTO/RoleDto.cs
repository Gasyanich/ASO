namespace ASO.Models.DTO
{
    public record RoleDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }
    }
}