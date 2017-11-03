namespace WebPixCoreAdmin.Models
{
    public class MenuViewModel : BaseModel
    {
        public string Url { get; set; }
        public int Pai { get; set; }
        public int Tipo { get; set; }
    }
}