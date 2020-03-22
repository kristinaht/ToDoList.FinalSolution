using System.Collections.Generic;

namespace ToDoList.Models
{
  public class Category
  {
    public Category()
    {
      this.Items = new HashSet<Item>();
    }

    public int CategoryId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Item> Items { get; set; }
  }
}

//HashSet is an unordered collection of unique elements. Can't have duplicates. More performant than list

//ICollecction is a generic interface build into tne .NET.
//Interface is a collection of method sigantures bundled together.
//ICollection outlines methods for querying and changing data so that we don't have to manually interact with the database. 