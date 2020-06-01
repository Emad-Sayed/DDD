using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoUploaderController : ControllerBase
    {
        private readonly IPhotoRepository _photoRepository;

        public PhotoUploaderController(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFile photo)
        {
            var photoPath = await _photoRepository.UploadPhoto(photo);
            return Ok(new { PhotoPath = photoPath });
        }
    }
}