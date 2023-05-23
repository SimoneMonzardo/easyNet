using System;
namespace easyNetAPI.Models.Authentication
{
	public class ChangePasswordRequest
	{
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}