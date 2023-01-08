namespace BlogWebSite.Data
{
    public class BlogEntryRecord
    {
        public Guid EntryId { get; set; }
        public string Title { get; set; } = "";
        public DateTimeOffset CreateTime { get; set; }
        public string BodyText { get; set; } = "";
    }
}
