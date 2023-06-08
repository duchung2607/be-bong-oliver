using System.ComponentModel.DataAnnotations;

namespace BongOliver.Models
{
    public class Item
    {
        [Key]
        public int id {  get; set; }
        public string url { get; set; } = "";
        public string title { get; set; } = "";
        public string description { get; set; } = "";
        public DateTime dateModify { get; set; } = DateTime.Now;
    }
}
