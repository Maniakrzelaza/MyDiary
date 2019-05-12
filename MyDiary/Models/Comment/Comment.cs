using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiary.Models
    
{
    public interface IComment
    {
        int Id { get; set; }
        String Content { get; set; }
        String Author_id { get; set; }
        int Article_id { get; set; }
    }
    public class Comment : IComment
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        public String Content { get; set; }

        public String Author_id { get; set; }
        public int Article_id { get; set; }
    }
}
