namespace Movies.API.Dtos.Category
{
    public record GetCategoryDto
    {
        public int CategoryId { get; init; }
        public string? Name { get; init; } 
    }
}
