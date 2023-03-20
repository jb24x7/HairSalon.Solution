namespace HairSalon.Models
{
  public class Review
  {
    public int ReviewId { get; set; }
    public int Stars { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string Author { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; }
  }
}