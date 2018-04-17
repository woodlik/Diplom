using GoSport.Client.Infrastructure.Mapping;
using GoSport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoSport.Client.ViewModels.Comments
{
    public class CommentViewModel: IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public int SportCenterId { get; set; }

        public virtual SportCenter SportCenter { get; set; }

        public virtual ICollection<Like> Likes { get; set; }
    }
}