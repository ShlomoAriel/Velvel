using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velvel.Domain.Users.Managers;

namespace Velvel.Domain.Data
{
    public class CommentGroup:BaseEntity
    {

    }
    public class Comment:BaseEntity
    {
        public int CommentGroupId { get; set; }
        public CommentGroup CommentGroup { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public DateTime ?Read { get; set; }
    }
}
