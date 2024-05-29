namespace Ecommerce_Rookie_NashTech_CustomerFrontend.Extensions
{
    public class Util
    {
		public static string UploadImage(IFormFile image, string folder)
		{
			try
			{
				var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", folder, image.FileName);
				using (var myfile = new FileStream(fullPath, FileMode.CreateNew))
				{
					image.CopyTo(myfile);
				}
				return image.FileName;
			}
			catch (Exception ex)
			{
				return string.Empty;
			}
		}
	}
}
