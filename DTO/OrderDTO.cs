using System;
using System.Collections.Generic;
public class OrderDTO {
    public int Id {get; set;}
    public DateTime Created {get; set; }
   // public string orderRows {get; set; }
    public int TotalPrice {get; set; }
    
   public ICollection<OrderRowDTO> OrderRowDTOs { get; set; }  

    public int CustomerDTOId {get; set; }
   public CustomerDTO CustomerDTO  {get; set; }

}