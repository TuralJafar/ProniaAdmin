namespace Pronia.mModels
{
    public class Slide
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public  string SubTitle { get; set; }   
        public string Description { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModeifieddAt { get; set; }
    }
}
