using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RestaurantCuisine.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantCuisine.Controllers
{
  public class RestaurantsController : Controller
  {
    private readonly RestaurantCuisineContext _db;

    public RestaurantsController(RestaurantCuisineContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Restaurant> model = _db.Restaurants
                            .Include(restaurant => restaurant.Cuisine)
                            .ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.CuisineId = new SelectList(_db.Cuisines, "CuisineId", "Name");
      return View();
    }


    [HttpPost]
    public ActionResult Find(string queryString)
    {
      List<Restaurant> model = _db.Restaurants.Include(restaurant => restaurant.Cuisine).Where(model => model.Name.Contains(queryString)).ToList();
      return View("Index", model);
    }


    [HttpPost]
    public ActionResult Create(Restaurant restaurant)
    {
      if (restaurant.CuisineId == 0)
      {
        return RedirectToAction("Create");
      }
      _db.Restaurants.Add(restaurant);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Restaurant thisRestaurant = _db.Restaurants
                          .Include(restaurant => restaurant.Cuisine)
                          .Include(restaurant => restaurant.Reviews)
                          .FirstOrDefault(restaurant => restaurant.RestaurantId == id);
      return View(thisRestaurant);
    }

    public ActionResult Edit(int id)
    {
      Restaurant thisRestaurant = _db.Restaurants.FirstOrDefault(restaurant => restaurant.RestaurantId == id);
      ViewBag.CuisineId = new SelectList(_db.Cuisines, "CuisineId", "Name");
      return View(thisRestaurant);
    }

    [HttpPost]
    public ActionResult Edit(Restaurant restaurant)
    {
      _db.Restaurants.Update(restaurant);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Restaurant thisRestaurant = _db.Restaurants.FirstOrDefault(restaurant => restaurant.RestaurantId == id);
      return View(thisRestaurant);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Restaurant thisRestaurant = _db.Restaurants.FirstOrDefault(restaurant => restaurant.RestaurantId == id);
      _db.Restaurants.Remove(thisRestaurant);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}