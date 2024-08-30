using System.ComponentModel.DataAnnotations;
using Accounting.Domain;
using Accounting.Persistence.EF;
using ImageMagick;
using Microsoft.AspNetCore.Mvc;
using MimeMapping;

namespace Accounting.API.Controllers
{
    [Route("api/documents")]
    [ApiController]
    public class DocumentsController(ApplicationWriteDbContext writeDbContext) : ControllerBase
    {
        [HttpPost]
        public async Task<string> Add([FromForm, Required] IFormFile file)
        {
            using var memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);

            var document = new Document(
                Path.GetExtension(file.FileName),
                memoryStream.ToArray());

            writeDbContext.Add(document);
            await writeDbContext.SaveChangesAsync();

            return document.Id;
        }

        [HttpGet("{id}")]
        public async Task<FileResult> Get(string id, [FromQuery] int? size)
        {
            var document = await writeDbContext
                .Set<Document>()
                .FindAsync(id);

            var data = document.Data;
            if (size.HasValue)
                data = GetThumbnail(document.Data, size.Value);

            return File(data, MimeUtility.GetMimeMapping(document.Extension));
        }

        private byte[] GetThumbnail(byte[] fileBytes, int size)
        {
            using var image = new MagickImage(fileBytes);
            var mainSize = new MagickGeometry(size);
            image.Resize(mainSize);
            return image.ToByteArray();
        }
    }
}
