using System.ComponentModel.DataAnnotations;

namespace OBS_App.ViewsModel
{
	public class LoginViewsModel
	{
		[Required]
		[EmailAddress]
		public string? Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string? Password { get; set; }

		public bool RememberMe { get; set; } = true;
	}
}
