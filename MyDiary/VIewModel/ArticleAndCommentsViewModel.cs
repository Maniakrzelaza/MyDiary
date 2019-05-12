using MyDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiary.VIewModel
{
    public class ArticleAndCommentsViewModel
    {
        public Article Article { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
