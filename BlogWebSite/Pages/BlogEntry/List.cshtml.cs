using BlogWebSite.Data;
using HigLabo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace BlogWebSite.Pages.BlogEntry
{
    public class ListModel : PageModel
    {
        private IConfiguration _Configuration;
        public List<BlogEntryRecord> BlogEntryList { get; init; } = new();
        public ListModel(IConfiguration configuration)
        {
            this._Configuration = configuration;
        }
        public async Task OnGet()
        {
            using (var db = new SqlServerDatabase(_Configuration.GetSection("ConnectionStrings").GetValue<String>("Dev")))
            {
                db.Open();
                var query = $"select * from TBlogEntry order by CreateTime desc";
                foreach (var item in await db.GetRecordListAsync<BlogEntryRecord>(query))
                {
                    this.BlogEntryList.Add(item);
                }
            }
        }
    }
} 
