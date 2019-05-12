namespace MyDiary.Models
{
    public interface IArticle
    {
        string Content { get; set; }
        int Id { get; set; }
        string Title { get; set; }
    }
}