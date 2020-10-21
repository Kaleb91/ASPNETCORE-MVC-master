using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Cooperchip.ITDeveloper.Application.Extensions
{
    public static class ArquivoValido
    {
        public static bool IsValid(IFormFile file,string nome)
        {

            return file != null && !string.IsNullOrEmpty(file.FileName) && (nome.ToUpper() == file.FileName.ToUpper());
        }
    }
}
