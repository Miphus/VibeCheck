namespace VibeCheck.Models
{
    public class UserFavorite
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ArtistId { get; set; }

        public ApplicationUser User { get; set; }
        public Artist Artist { get; set; }
    }
}
