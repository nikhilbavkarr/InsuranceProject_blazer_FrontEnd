namespace WebApplication1.DTOs
{
	public class InsuredDtos
	{
		public int InsuredId { get; set; }

		public int PolicyHolderId { get; set; }

		public string Name { get; set; } = null!;

		public DateOnly Dob { get; set; }

		public string Gender { get; set; } = null!;

		public DateOnly RegistrationDate { get; set; }
	}
}
