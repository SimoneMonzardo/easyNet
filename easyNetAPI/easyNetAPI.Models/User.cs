using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace easyNetAPI.Models
{
	public class User : IdentityUser
	{
        [PersonalData]
        [Required]
        [MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Not permitted")]
        public string Name { get; set; } = null!;

        [PersonalData]
        [Required]
        [MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Not permitted")]
        public string Surname { get; set; } = null!;

        [PersonalData]
        [Required]
        [MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Not permitted")]
        public string Gender { get; set; } = null!;

        [PersonalData]
        [Required]
        public DateOnly DateOfBirth { get; set; }

        [PersonalData]
        public string ProfilePicture { get; set; } = null!;
    }
}