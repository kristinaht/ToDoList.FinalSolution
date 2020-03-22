using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToDoList.Controllers
{
  public class ItemsController : Controller
  {
    private readonly ToDoListContext _db;

    public ItemsController(ToDoListContext db)
    {
      _db = db;
    }

//--------------------------------------------------------------
//REGARDING db.Items used in Index() route: 
//db.Items is our dataset. dataset can't be used as our model for views. 
//we use LINQ method ToList() to be able to use dataset in views. ToList() translates the dataset into a list we can use in the view/
//db is an instance of our DbContext class. It is holding reference of our database
//once there it looks for and object named Items. Items is the DbSet declared in ToDoListContext.cs
//LINQ turns this DbSet into a list using ToList()
//This expression is what creates the model we'll use in the Index view.
    public ActionResult Index()
    {
      List<Item> model = _db.Items.Include(items => items.Category).ToList(); //for each Item in the db, include the Category it belongs to and then put all the Items into list.
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Item item)
    {
      //Add() & SaveChanges() update DbSet and sync the changes to the database represented by DbContext
      _db.Items.Add(item); //Add() is run on DbSet property (_db.Items in this case)
      _db.SaveChanges();  //SaveChanges() is run on DbContext itself
      return RedirectToAction("Index");
    }


//____________________________________________________________
//EXPLANATION OF METHOD USED IN DETAILS ACTION:
//1.start by looking at db.Items - this is items table in db
//2. find any items where the ItemId of an item is equal the same as the id we passed into this method.
    public ActionResult Details(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(ItemsController => ItemsController.ItemId == id);
      return View(thisItem);
    }


//GET EDIT ACTION EXPLANATION:
//GET is finding a specific item and then passing it to the view.

//SelectList() will provide all the categories for a dropdown menu in the view. It will also set the select option value to CategoryId and the select option to display the Name. This allows the user to select an Item from the dropdown menu to associate it with the Category. 
    public ActionResult Edit(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View();
    }

// POST REQUEST EXPLANATION:
// 1. we find and update all of the properties of the item passed as parameter into the Entry() method.
// 2. we update item's State property to EntityState.Modified. This tells Entity that the entry has been modified. 
    [HttpPost]
    public ActionResult Edit(Item item)
    {
      _db.Entry(item).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
      return View(thisItem);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
      _db.Items.Remove(thisItem);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
  }
}