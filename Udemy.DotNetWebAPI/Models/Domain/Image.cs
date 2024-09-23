﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Udemy.DotNetWebAPI.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtention { get; set; }
        public string? FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
    }
}
