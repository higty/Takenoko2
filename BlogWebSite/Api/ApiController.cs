using HigLabo.Data;
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
    }
}
