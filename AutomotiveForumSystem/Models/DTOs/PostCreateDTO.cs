﻿using System.ComponentModel.DataAnnotations;

namespace AutomotiveForumSystem.Models.PostDtos
{
    public class PostCreateDTO
    {
        public int CategoryID { get; set; }

        [Required, MinLength(16), MaxLength(64)]
        public string Title { get; set; } = "";

        [Required, MinLength(32), MaxLength(8192)]
        public string Content { get; set; } = "";

    }
}
