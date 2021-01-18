using System.Collections.Generic;

public class ProductDTO {
    public int Id {get; set;}
    public string Title {get; set; }
    public string Description {get; set; }
    public int Price {get; set; }
    public string Image {get; set; }
    
   public ICollection<OrderRowDTO> OrderRowsDTO{ get; set; }

}