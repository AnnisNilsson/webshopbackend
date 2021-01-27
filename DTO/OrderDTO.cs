using System;
using System.Collections.Generic;
public class OrderDTO {
    public int Id {get; set;}
    public DateTime Created {get; set; }
  // public string OrderRows {get; set; }
  
    public int TotalPrice {get; set; }
    
  public ICollection<OrderRowDTO> OrderRows { get; set; }  
// public OrderRowDTO OrderRowDTO{get; set;}

    public int CustomerId {get; set; }
   public CustomerDTO CustomerDTO  {get; set; }

}