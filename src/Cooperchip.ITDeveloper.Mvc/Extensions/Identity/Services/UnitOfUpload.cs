using Cooperchip.ITDeveloper.Mvc.Infra;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Mvc.Extensions.Identity.Services
{
    public class UnitOfUpload : IUnitOfUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UnitOfUpload(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async void UploadImage(IFormFile file)
        {
            long totalbytes = file.Length;
            string fileName = file.FileName;
            fileName = fileName.Contains("\\") ? fileName.Substring(fileName.LastIndexOf("\\") + 1) : fileName;

            byte[] buffer = new byte[3 * 1024];
            using (FileStream output = System.IO.File.Create(ObterCaminhoMaisNomeDoArquivo(fileName)))
            {
                using(Stream input = file.OpenReadStream())
                {
                    int readBytes;
                    while ((readBytes = input.Read(buffer,0,buffer.Length))>0)
                    {
                        await output.WriteAsync(buffer, 0, readBytes);
                        totalbytes += readBytes;
                    }
                }
            }
        }

        private string ObterCaminhoMaisNomeDoArquivo(string fileName)
        {
            string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            return path + fileName;
        }
    }
}
