namespace BusinessLogicLayer.DTO
{
    public record User
    {
        public Guid UserId { get; init; }
        public string? Email { get; init; }
        public string? PersonName { get; init; }
        public string? Gender { get; init; }
    }
}
