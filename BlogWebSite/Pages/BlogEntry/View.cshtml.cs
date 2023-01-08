using BlogWebSite.Data;
using HigLabo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogWebSite.Pages.BlogEntry
{
    public class ViewModel : PageModel
    {
        private IConfiguration _Configuration;
        
        public Guid EntryId { get; set; }
        public string Title { get; set; } = "";
        public string BodyText { get; set; } = "";

        public ViewModel(IConfiguration configuration)
        {
            this._Configuration = configuration;
        }
        public async Task OnGet(Guid entryId)
        {
            this.EntryId = entryId;
            using (var db = new SqlServerDatabase(_Configuration.GetSection("ConnectionStrings").GetValue<String>("Dev")))
            {
                db.Open();
                var query = $"select * from TBlogEntry where EntryId = '{entryId}'";
                var r = await db.GetRecordAsync<BlogEntryRecord>(query);
                this.Title = r.Title;
                this.BodyText = r.BodyText;
            }
        }
    }
}
