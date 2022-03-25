using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementApp.Server.Controllers.APIs
{
    [DisableRequestSizeLimit]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadsController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public UploadsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("single")]
        public async Task<IActionResult> Single(IFormFile file)
        {
            try
            {
                await UploadFileAsync(file);
                return StatusCode(200, file.FileName);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("single/{employeeId:int}")]
        public async Task<IActionResult> Single(IFormFile file, int employeeId)
        {
            try
            {
                await UploadFileAsync(file, employeeId);
                return StatusCode(200, file.FileName);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        public async Task UploadFileAsync(IFormFile file, int employeeId = 0)
        {
            if (file != null && file.Length > 0)
            {
                var imagePath = @"\Photos" + (employeeId == 0 ? string.Empty : @$"\{employeeId}");
                var uploadPath = (_environment.WebRootPath ?? _environment.ContentRootPath) + imagePath;

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var fullPath = Path.Combine(uploadPath, file.FileName);
                using (FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
        }
    }
}