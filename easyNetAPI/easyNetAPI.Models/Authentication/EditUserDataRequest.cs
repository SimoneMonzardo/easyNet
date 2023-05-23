using System;
namespace easyNetAPI.Models.Authentication
{
	public class EditUserDataRequest
	{
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public string ProfilePicture { get; set; } = null!;
    }
}