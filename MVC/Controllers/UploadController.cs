using BLLServices.Common.UploadService;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    // just for testing UploadService
    public class UploadController : Controller
    {
        private readonly IUploadService _uploadService;

        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        // Display the upload form
        public IActionResult Index()
        {
            return View();
        }

        // Handle the file upload POST action
        [HttpPost]
        public  async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewData["Message"] = "No file selected!";
                return View("Index");
            }

            try
            {
                //string fileName =  _uploadService.UploadFile(file);// for creating a new one 
                string fileName =  await _uploadService.UploadFile(file,oldFilename: "aae1e8cc-dc37-42c2-9048-f709366a2ee4.png");// for update  image 
                ViewData["Message"] = "File uploaded successfully!";
                ViewData["FileName"] = fileName;
            }
            catch (Exception ex)
            {
                ViewData["Message"] = $"File upload failed: {ex.Message}";
            }

            return View("Index");
        }

    }
}
