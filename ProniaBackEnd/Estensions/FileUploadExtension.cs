namespace ProniaBackEnd.Estensions
{
    public class FileUploadExtension
    {

        public static string SaveFile(IFormFile file,string path)
        {
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string fullPath = Path.Combine(path, fileName);

            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            };

            return fileName;
        }
    }
}
