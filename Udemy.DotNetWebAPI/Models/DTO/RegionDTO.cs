﻿namespace Udemy.DotNetWebAPI.Models.DTO
{
    public class RegionDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
    public class AddRegionDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }

}
