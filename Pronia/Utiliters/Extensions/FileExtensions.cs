﻿using Pronia.mModels;

namespace Pronia.Utiliters.Extensions
{
    public static class FileExtensions
    {
        public static bool CheckFileType(this IFormFile file,string type)
        {
            if(file.ContentType.Contains(type))
            {
                return true;
            }
            return false;
        }
        public static bool CheckFileSize(this IFormFile file, int kb)
        {
            if (file.Length < kb * 1024)
            {
                return true;
            }
            return false;
        }
        public static async Task<string> CreateFileAsync(this IFormFile file, string root, string folder)
        {
            string fileName = Guid.NewGuid().ToString() + file.FileName;


            using (FileStream fileStream = new FileStream(Path.Combine(root, folder, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return fileName;
        }
        public static void DeleteFile(this string FileName ,string root,string folder) 
        {
            string path=Path.Combine(root,folder,FileName);
            if(File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
