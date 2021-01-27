using System;
using System.Collections.Generic;

public class OrderRowDTO{
    
    public int OrderId { get; set; }
    public OrderDTO OrderDTO { get; set; }
    public int ProductId { get; set; }
    public ProductDTO ProductDTO { get; set; }
}