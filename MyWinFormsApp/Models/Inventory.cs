using System;
using System.ComponentModel; // For BindingList

namespace MyWinFormsApp
{
    public static class Inventory
    {
        public static BindingList<Product> Products { get; set; } = new BindingList<Product>();
        public static BindingList<Part> AllParts { get; set; } = new BindingList<Part>();

        static Inventory()
        {
            LoadSampleData();
        }

        public static void LoadSampleData()
        {
            AllParts.Add(new Inhouse(1, "A", 15.50m, 10, 1, 100, 101));
            AllParts.Add(new Inhouse(2, "B", 25.75m, 15, 1, 100, 102));

            AllParts.Add(new Outsourced(3, "C", 10.00m, 5, 1, 50, "Supplier A"));
            AllParts.Add(new Outsourced(4, "D", 30.99m, 8, 1, 50, "Supplier B"));

            Products.Add(new Product(101, "Product A", 10, 150.00m, 1, 20, new BindingList<Part>()));
            Products.Add(new Product(102, "Product B", 8, 250.00m, 1, 15, new BindingList<Part>()));

            Products[0].AddAssociatedPart(AllParts[0]);  
            Products[0].AddAssociatedPart(AllParts[2]);  

            Products[1].AddAssociatedPart(AllParts[1]);  
            Products[1].AddAssociatedPart(AllParts[3]);  
        }


        public static void AddProduct(Product newProduct)
        {
            Products.Add(newProduct);
        }

        public static bool RemoveProduct(Product product)
        {
            return Products.Remove(product);
        }
        public static Product? LookupProduct(int productID)
        {
            return Products.FirstOrDefault(product => product.ProductID == productID);
        }

        public static BindingList<Product> LookupProduct(string searchText)
        {
            BindingList<Product> filteredProducts = new BindingList<Product>();

            if (string.IsNullOrEmpty(searchText))
            {
                return Products;
            }

            bool isNumeric = decimal.TryParse(searchText, out decimal searchNumber);

            foreach (var product in Products)
            {
                if (isNumeric)
                {
                    if (product.ProductID == (int)searchNumber ||
                        product.Price == searchNumber ||
                        product.InStock == (int)searchNumber ||
                        product.Min == (int)searchNumber ||
                        product.Max == (int)searchNumber)
                    {
                        filteredProducts.Add(product);
                    }
                }
                else
                {
                    if (product.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    {
                        filteredProducts.Add(product);
                    }
                }
            }

            return filteredProducts;
        }


        public static void UpdateProduct(int productID, Product updatedProduct)
        {
            var existingProduct = LookupProduct(productID);
            if (existingProduct != null)
            {
                int index = Products.IndexOf(existingProduct);
                Products[index] = updatedProduct;
            }
        }

        public static void AddPart(Part newPart)
        {
            AllParts.Add(newPart);
        }

        public static bool DeletePart(Part part)
        {
            return AllParts.Remove(part);
        }

        public static BindingList<Part> LookUpPart(string searchText)
        {
            BindingList<Part> filteredParts = new BindingList<Part>();

            if (string.IsNullOrEmpty(searchText))
            {
                return AllParts;
            }

            bool isNumeric = decimal.TryParse(searchText, out decimal searchNumber);

            foreach (var part in AllParts)
            {
                if (isNumeric)
                {
                    if (part.PartID == (int)searchNumber ||
                        part.Price == searchNumber ||
                        part.Min == (int)searchNumber ||
                        part.Max == (int)searchNumber ||
                        part.InStock == (int)searchNumber)
                    {
                        filteredParts.Add(part);
                    }
                }
                else
                {
                    if (part.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        filteredParts.Add(part);
                    }
                }
            }

            return filteredParts;
        }

        public static void UpdatePart(int partID, Part updatedPart)
        {
            Part? existingPart = AllParts.FirstOrDefault(p => p.PartID == partID);

            if (existingPart != null)
            {
                int index = AllParts.IndexOf(existingPart);
                AllParts[index] = updatedPart;
            }
            else
            {
                throw new ArgumentException($"No part found with PartID {partID}");
            }
        }
    }
}
