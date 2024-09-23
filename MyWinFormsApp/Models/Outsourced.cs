namespace MyWinFormsApp
{
    public class Outsourced(int partID, string name, decimal price, int inStock, int min, int max, string companyName) : Part(partID, name, inStock, price, min, max)
    {
        public string CompanyName { get; set; } = companyName;
    }
}
