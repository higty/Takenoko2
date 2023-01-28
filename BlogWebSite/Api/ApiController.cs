using Azure.Storage.Blobs;
using HigLabo.Data;
using HigLabo.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlogWebSite.Api
{
    public class ApiController : ControllerBase
    {
        private IConfiguration _Configuration;
        public ApiController(IConfiguration configuration)
        {
            this._Configuration = configuration;
        }

        public class BlogEntry_Add_Parameter
        {
            public string Title { get; set; } = "";
            public string BodyText { get; set; } = "";
        }
        [HttpPost("/api/blog/entry/add")]
        public async Task<object> BlogEntry_Add()
        {
            var sr = new StreamReader(Request.Body);
            var json = await sr.ReadToEndAsync();
            var p = JsonConvert.DeserializeObject<BlogEntry_Add_Parameter>(json);
            using (var db = new SqlServerDatabase(_Configuration.GetSection("ConnectionStrings").GetValue<String>("Dev")))
            {
                db.Open();
                var query = $"insert into TBlogEntry(EntryId,Title,CreateTime,BodyText) values (NEWID(),'{p.Title}','{DateTime.Now}','{p.BodyText}')";
                db.ExecuteCommand(query);
            }
            return new object();
        }

        [HttpPost("/api/file/upload")]
        public async Task<Object> Api_File_Upload()
        {
            var l = new List<Object>();
            var cl = new BlobServiceClient(_Configuration.GetValue<string>("BlobAccessKey"));
            var container = cl.GetBlobContainerClient("content");

            foreach (var f in this.Request.Form.Files)
            {
                var now = DateTime.UtcNow.AddHours(9);
                var fileName = "file_" + now.ToString("yyyyMMdd_HHmmss_fff");
                var ext = Path.GetExtension(f.FileName);
                var blob = container.GetBlobClient(fileName + ext);
                var res = await blob.UploadAsync(f.OpenReadStream());

                if (f.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
                {
                    var result = new WebApiResult();
                    result.Data = new { ImageUrl = blob.Uri.AbsoluteUri };
                    return result;
                }
                else
                {
                    return new { Url = blob.Uri.AbsoluteUri, FileName = f.FileName };
                }
            }

            return l;
        }
    }
}
