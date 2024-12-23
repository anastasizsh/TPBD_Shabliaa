using System.Collections.Generic;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }

    // Навігаційна властивість для Lazy Loading
    public virtual ICollection<Product> Products { get; set; }
}
