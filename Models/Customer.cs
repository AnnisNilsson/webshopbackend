using System;
using System.Collections.Generic;
public class Customer {
    public int Id {get; set;}
    public string Name {get; set; }
    public string Adress {get; set; }
    public string City {get; set; }
    
   public ICollection<Order> Orders{ get; set; }

}