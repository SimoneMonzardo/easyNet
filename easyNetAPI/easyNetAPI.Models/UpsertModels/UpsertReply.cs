using System;
namespace easyNetAPI.Models.UpsertModels
{
	public class UpsertReply
	{
        public int ReplyId { get; set; }
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string? Content { get; set; }
    }
}

