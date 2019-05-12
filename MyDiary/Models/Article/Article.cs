using Microsoft.AspNetCore.Mvc;
using MyDiary.VIewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiary.Models
{
    public class Article : IArticle
    {
        [Key]
        public int Id { get; set; }

        [Remote(
            action: "VerifyTitle",
            controller: "Articles",
            ErrorMessage = "Use a new title"
        )]
        [StringLength(80, MinimumLength = 1)]
        public string Title { get; set; }

        [StringLength(50000,MinimumLength = 10)]
        [NoBadWords("bad")]
        public String Content { get; set; }
        ICollection<Comment> Comments { get; set; }
    }
}
