using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;

namespace GestionCommerciale.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private IWebHostEnvironment _hostingEnvironment;

        public FilesController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> PostFile(/*IFormFile file*/)
        {
            try
            {
                var file = Request.Form.Files[0];
                string fullPath = "";
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "Visite");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (file.Length > 0)
                {
                    fullPath = Path.Combine(path, file.Name);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                return Ok(new { fileName = $"{file.Name}" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult DeleteFile(MyFile myFile)
        {
            try
            {
                if (myFile.filename != 0)
                {
                    string DeleteThis = "" + myFile.filename;
                    string[] Files = Directory.GetFiles($"{_hostingEnvironment.WebRootPath}\\{myFile.folder}");

                    foreach (string file in Files)
                    {
                        if (file.ToUpper().Contains(DeleteThis.ToUpper()))
                        {
                            var fileInfo = new FileInfo(file);
                            fileInfo.Delete();
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                var msg = ex.Message;
            }

            return Ok(new { msg = "ok" });
        }

        [HttpPost]
        public IActionResult DeleteFiles(MyFile2 myFile)
        {
            if (myFile.filenames.Length == 0)
            {
                return Ok();
            }
            try
            {
                foreach (var filename in myFile.filenames)
                {
                    var fileInfo = new FileInfo($"{_hostingEnvironment.WebRootPath}\\{myFile.folder}\\{filename}");
                    if (fileInfo.Exists)
                    {

                        fileInfo.Delete();
                    }
                    else
                    {
                        return Ok(new {message = "not exist"});
                    }
                }

            }
            catch (System.Exception ex)
            {
                Ok(ex.Message);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult download(MyFile myFile)
        {
            try
            {
                if (myFile.filename == 0)
                {
                    string[] fileNames = System.IO.Directory.GetFiles(_hostingEnvironment.WebRootPath, $"{myFile.filename}_*");
                    var fileInfo = new FileInfo(fileNames[0]);
                    // var fileInfo = new FileInfo($"{_hostingEnvironment.WebRootPath}/{myFile.filename}_*");
                    fileInfo.Delete();
                }

            }
            catch (System.Exception ex)
            {
                var msg = ex.Message;
            }

            return Ok(new { msg = "ok" });
        }

        [HttpPost("{folder}"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFiles([FromRoute]string folder)
        {
            IFormFileCollection files = Request.Form.Files;
            
            string path = Path.Combine(_hostingEnvironment.WebRootPath, folder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (files.Count > 0)
            {
                try
                {
                    foreach (var file in files)
                    {
                        string fullPath = Path.Combine(path, file.FileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    BadRequest(ex.Message);
                }

            }
            return Ok();
        }
    }

    public class MyFile
    {
        public int filename { get; set; }
        public string folder { get; set; }
    }

    public class MyFile2
    {
        public string[] filenames { get; set; }
        public string folder { get; set; }
    }
}
