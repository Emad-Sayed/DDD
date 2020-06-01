using Application.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IPhotoRepository
    {
        Task<string> UploadPhoto(IFormFile photo);
        void DeletePhoto(string name);
    }
}
