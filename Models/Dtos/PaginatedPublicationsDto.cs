namespace YouthProtectionApi.Models.Dtos
{
    public class PaginatedPublicationsDto
    {
        public List<PublicationsModelDto> Publications { get; set; } = new List<PublicationsModelDto>();
        public int TotalCount {  get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
