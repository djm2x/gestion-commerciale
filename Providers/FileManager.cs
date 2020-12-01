// using System.IdentityModel.Tokens.Jwt;
// using System.Linq;
// using System.Security.Claims;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
// using System.Collections.Generic;
// using Microsoft.AspNetCore.Hosting;
// using System.IO;
// using Microsoft.AspNetCore.StaticFiles;

// namespace Admin5.Providers
// {
//     public class FileManager
//     {
//         private IWebHostEnvironment _hostingEnvironment;

//         public FileManager(IWebHostEnvironment environment)
//         {
//             _hostingEnvironment = environment;
//         }

//         public async Task<List<string>> UploadList(HttpContext context)
//         {
//             var result = new List<string>();

//             var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
//             if (Directory.Exists(uploads))
//             {
//                 var provider = _hostingEnvironment.ContentRootFileProvider;
//                 foreach (string fileName in Directory.GetFiles(uploads))
//                 {
//                     var fileInfo = provider.GetFileInfo(fileName);
//                     result.Add(fileInfo.Name);
//                 }
//             }
//             return result;
//         }

//         public async Task<int> UploadOne(HttpContext context)
//         {
//             JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
//             // var token0 = await context.GetTokenAsync("name");
//             // var me = HttpContext.au;
//             string tokenString = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//             JwtSecurityToken token = tokenHandler.ReadJwtToken(tokenString);
//             Claim claim = token.Claims.SingleOrDefault(e => e.Type == "unique_name");

//             return int.Parse(claim?.Value);
//         }

//         public async Task<int> Download(string file)
//         {
//             var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
//             var filePath = Path.Combine(uploads, file);
//             if (!System.IO.File.Exists(filePath))
//                 return 0;

//             var memory = new MemoryStream();
//             using (var stream = new FileStream(filePath, FileMode.Open))
//             {
//                 await stream.CopyToAsync(memory);
//             }
//             memory.Position = 0;
//             return 1;
//             // File.
//             // return File(memory, GetContentType(filePath), file);
//         }

//         private string GetContentType(string path)
//         {
//             var provider = new FileExtensionContentTypeProvider();
//             string contentType;
//             if (!provider.TryGetContentType(path, out contentType))
//             {
//                 contentType = "application/octet-stream";
//             }
//             return contentType;
//         }

//         public async Task<FileStream> DownloadFile(string fileName)
//         {
//             var currentDirectory = System.IO.Directory.GetCurrentDirectory();
//             currentDirectory = currentDirectory + "\\src\\assets";
//             var file = Path.Combine(Path.Combine(currentDirectory, "attachments"), fileName);
//             return new FileStream(file, FileMode.Open, FileAccess.Read);
//         }

//     }
// }