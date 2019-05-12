using MyDiary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiary.VIewModel
{
    public class NoBadWords : ValidationAttribute
    {
        protected String _text;

        public NoBadWords(String Text)
        {
            _text = Text;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Article article = (Article)validationContext.ObjectInstance;
            if (article.Content.Contains(_text))
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return "Comment can not contain bad words.";
        }
    }
}
