using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{

  //ToDoListContext class extends from (inherits from) Entity's DbContext class. ToDoListContext now has all built-in DbContext functionality.
  public class ToDoListContext : DbContext
  {  
    public DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; } //DbSet is a property. DbSet Items represents Items table in database. <Item> is included in brackets so that DbSet knows what object it represents. 

    public ToDoListContext(DbContextOptions options) : base(options) { }
  }
}