using Microsoft.EntityFrameworkCore;

namespace RestaurantCuisine.Models
{
  public class RestaurantCuisineContext : DbContext
  {
    public DbSet<Cuisine> Cuisines { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public RestaurantCuisineContext(DbContextOptions options) : base(options) { }
  }
}