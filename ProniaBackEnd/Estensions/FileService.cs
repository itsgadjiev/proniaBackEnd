using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ProniaBackEnd.Estensions
{
    public static class FileService
    {
        public static string SaveFile(this IFormFile file, string path, string folderName)
        {

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string fullPath = Path.Combine(path, folderName, fileName);

            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            };

            return fileName;


        }

        public static void RemoveFile(this IFormFile file, string path, string folderName, string fileName)
        {
            if (file != null)
            {
                string fullPath = Path.Combine(path, folderName, fileName);
                if (System.IO.File.Exists(fullPath)) { System.IO.File.Delete(fullPath); }
            }
        }

        public static void RemoveFile(string path, string folderName, string fileName)
        {
            string fullPath = Path.Combine(path, folderName, fileName);
            if (System.IO.File.Exists(fullPath)) { System.IO.File.Delete(fullPath); }
        }
    }
}
