using System.ComponentModel.DataAnnotations;

namespace PeerLandingFE.DTO.Req
{
	public class ReqRegisterDto
	{
		public string Name { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		[MinLength(8)]
		public string Password { get; set; }
		public string Role { get; set; }
		public decimal Balance { get; set; } = 0;

	}
}
