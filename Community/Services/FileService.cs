using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Services
{
    public class FileService
    {
        private readonly IConfiguration _configuration;

        public FileService()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }
        /// <summary>
        /// Saves image in directory and returns file path,
        /// which can be used to access this image letter.
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public async Task<string> SaveImage(IFormFile formFile)
        {
            if (formFile == null)
            {
                return _configuration["FileService:DefaultImagePath"];
            }
            
            var directoryForImage = _configuration["FileService:DirectoryForImages"];
            
            // Creates random name for image
            var fileName = GetRandomFileName(directoryForImage);

            // Replaces random extension of generated filename
            // to extension of given file from formFile object 
            var fileExtension = Path.GetExtension(formFile.FileName);
            fileName = Path.ChangeExtension(fileName, fileExtension);

            // Creates full file path for saving
            var filePath = directoryForImage + fileName;

            // Saves file in directory
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

           
            return _configuration["FileService:ImageAccessPath"]+fileName;
        }


        public async Task<string> UpdateImageAsync(string oldImagePath, IFormFile formFile)
        {
            // Meeting can have only 1 image,
            // so previous, if exists, should be deleted
            if (File.Exists(oldImagePath))
            {
                File.Delete(oldImagePath);
            }

            return await SaveImage(formFile);
        }


        private string GetRandomFileName(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                throw new ArgumentNullException("directoryPath");
            }

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

            // If after full cycle couldn't figure out random filename 
            if (fileNameList.Contains(fileName))
            {
                throw new Exception("can't create random file");
            }

            return fileName;
        }
    }
}
