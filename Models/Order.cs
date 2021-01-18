using System;
using System.Collections.Generic;
public class Order {
    public int Id {get; set;}
    public DateTime Created {get; set; }
   // public string orderRows {get; set; }
    public int TotalPrice {get; set; }
    
   public ICollection<OrderRow> OrderRows{ get; set; }  

    public string CustomerId {get; set; }
   public Customer Customer  {get; set; }

}