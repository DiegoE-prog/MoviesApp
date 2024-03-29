﻿using System.ComponentModel.DataAnnotations;

namespace Movies.Common.Models.Dtos.Category
{
    public record CategoryToAddDto
    {
        [Required(ErrorMessage ="Name is a required field")]
        [MaxLength(25)]
        [MinLength(5)]
        public string Name { get; init; } = String.Empty;
    }
}
