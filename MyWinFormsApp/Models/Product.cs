using System.ComponentModel;
namespace MyWinFormsApp
{
    public class Product(int productID, string name, int inStock, decimal price, int min, int max, BindingList<Part> associatedParts)
    {
        public BindingList<Part> AssociatedParts { get; set; } = associatedParts;
        public int ProductID { get; set; } = productID;
        public string Name { get; set; } = name;
        public decimal Price { get; set; } = price;
        public int InStock { get; set; } = inStock;
        public int Min { get; set; } = min;
        public int Max { get; set; } = max;

        public void AddAssociatedPart(Part part)
        {
            AssociatedParts.Add(part);
        }

        public bool RemoveAssociatedPart(int partID)
        {
            var part = LookupAssociatedPart(partID);
            if (part != null)
            {
                AssociatedParts.Remove(part);
                return true;
            }
            return false;
        }

        public Part? LookupAssociatedPart(int partID)
        {
            return AssociatedParts.FirstOrDefault(p => p.PartID == partID);
        }
    }
}
