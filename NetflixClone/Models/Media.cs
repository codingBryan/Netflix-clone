﻿
namespace NetflixClone.Models
{
    public class Media
    {
        public int Id { get; set; }
        public string DisplayTitle { get; set; } //movie title
        public string MediaType { get; set; } // "movie" or "tv"

        public string Thumbnail { get; set; }
        public string ThumbnailSmall { get; set; }
        public string ThumbnailUrl { get; set; }

        public string Overview { get; set; }
        public string ReleaseDate { get; set; }
    }
}
