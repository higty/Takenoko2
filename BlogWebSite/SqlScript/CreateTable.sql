create table TBlogEntry
(EntryId uniqueidentifier not null
,Title nvarchar(128) not null
,CreateTime datetimeoffset(7) not null
,BodyText nvarchar(max) not null
)
go

select * from TBlogEntry
