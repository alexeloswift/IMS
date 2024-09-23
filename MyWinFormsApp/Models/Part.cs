namespace MyWinFormsApp
{
    public abstract class Part(int partID, string name, int inStock, decimal price, int min, int max)
    {
        public int PartID { get; set; } = partID;
        public string Name { get; set; } = name;
        public int InStock { get; set; } = inStock;
        public decimal Price { get; set; } = price;
        public int Min { get; set; } = min;
        public int Max { get; set; } = max;
    }
}
