using GoSport.Data.Common.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace GoSport.Data.Models
{
    public class Comment : AuditInfo, IDeletableEntity
    {
        private ICollection<Like> likes;
        private ICollection<Comment> answers;

        public Comment()
        {
            this.likes = new HashSet<Like>();
            this.answers = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int SportCenterId { get; set; }

        public virtual SportCenter SportCenter { get; set; }

        public virtual ICollection<Like> Likes
        {
            get { return this.likes; }
            set { this.likes = value; }
        }

        public virtual ICollection<Comment> Answers
        {
            get { return this.answers; }
            set { this.answers = value; }
        }

        public bool IsDeleted   { get; set;  }

        public DateTime? DeletedOn { get; set; }
    }
}