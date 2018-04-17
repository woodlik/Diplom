using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoSport.Data.Models
{
    public class Like
    {
        public int Id { get; set; }

        [Required]
        public bool isLiked { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int CommentId { get; set; }

        public virtual Comment  Comment { get; set; }
    }
}