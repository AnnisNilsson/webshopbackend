using System;
using System.Collections.Generic;
public class Product {
    public int Id {get; set;}
    public string Title {get; set; }
    public string Description {get; set; }
    public int Price {get; set; }
    public string Image {get; set; }
   //     public DateTime Created { get; set; }

    
   public ICollection<OrderRow> OrderRows{ get; set; }

}