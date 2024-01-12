using Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete;

public class Category:Entity<int>
{
    public string Name { get; set; }
   
    public virtual IList<Product>? Products { get; set; }

   
}
