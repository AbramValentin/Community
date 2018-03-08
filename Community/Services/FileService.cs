using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Services
{
    public class FileService
    {
        
        public async Task<string> SaveImage(IFormFile formFile)
        {
            if (formFile == null)
            {
                throw new ArgumentNullException("directoryPath or formFile is null");
            }

            var directoryForImage = @"wwwroot\Images\";
            
            var fileName = GetRandomFileName(directoryForImage);

            var fileExtension = Path.GetExtension(formFile.FileName);

            fileName = Path.ChangeExtension(fileName, fileExtension);

            

            var filePath = directoryForImage + fileName;


            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            filePath = filePath.Replace("wwwroot", "");

            return filePath;
        }


        public async Task<string> UpdateImageAsync(string oldImagePath, IFormFile formFile)
        {
            if (File.Exists(oldImagePath))
            {
                File.Delete(oldImagePath);
            }

            return await SaveImage(formFile);
        }


        private string GetRandomFileName(string directoryPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            List<string> fileNameList = new List<string>();

            fileNameList.AddRange(
                    directoryInfo.GetFiles().Select(m => m.Name)
                );

            string fileName = "";

            for (int i = 0; i < 1000000; i++)
            {
                fileName = Path.GetRandomFileName();
                if (fileNameList.Contains(fileName)==false)
                {
                    break;
                }
            }

            if (fileNameList.Contains(fileName))
            {
                throw new Exception("can't create random file");
            }

            return fileName;
        }
    }
}
