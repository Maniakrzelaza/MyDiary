using MyDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiary.VIewModel
{
    public class ArticleCommentViewModel
    {
        public int id { get; set; }
        public Comment comment { get; set; }
    }
}
