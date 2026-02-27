public class Ingredient
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }

    //För visning av text t.ex "2 klyftor" eller "1 msk"
    public string DisplayQuantity { get; set; } = string.Empty;

    public string Unit { get; set; } = string.Empty;

    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}
