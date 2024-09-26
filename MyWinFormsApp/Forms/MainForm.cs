using System.ComponentModel;

namespace MyWinFormsApp
{
    public partial class MainForm : BaseForm
    {
        // MARK: - Properties
        private DataGridView dataGridViewParts;
        private DataGridView dataGridViewProducts;
        private Label partsTableTitle;
        private Label productsTableTitle;
        private TextBox partsSearchBox;
        private TextBox productsSearchBox;

        // MARK: - Main
        public MainForm()
        {
            dataGridViewParts = new DataGridView();
            dataGridViewProducts = new DataGridView();
            partsTableTitle = new Label();
            productsTableTitle = new Label();
            partsSearchBox = new TextBox();
            productsSearchBox = new TextBox();

            InitializeComponent();
            InitializeControls();
            LoadData();
        }

        // MARK: - Main Methods
        private void InitializeControls()
        {
            InitializeButtonsAndTextBox();
            InitializePartsTable();
            InitializeProductsTable();
        }

        private void InitializeButtonsAndTextBox()
        {
            AddButton("Search", new Point(500, 50), SearchParts);
            partsSearchBox = AddTextBox(new Point(650, 50), new Size(300, 50));

            AddButton("Search", new Point(1400, 50), SearchProducts);
            productsSearchBox = AddTextBox(new Point(1550, 50), new Size(300, 50));

            AddButton("Add Part", new Point(525, 850), OpenAddPart);
            AddButton("Modify Part", new Point(675, 850), OpenModifyPart);
            AddButton("Delete Part", new Point(825, 850), RemovePart);

            AddButton("Add Product", new Point(1425, 850), OpenAddProduct);
            AddButton("Modify Product", new Point(1575, 850), OpenModifyProduct);
            AddButton("Delete Product", new Point(1725, 850), RemoveProduct);

            AddButton("Exit", new Point(1860, 950), (s, e) => Application.Exit());
        }

        private void InitializePartsTable()
        {
            partsTableTitle.AutoSize = true;
            partsTableTitle.Location = new Point(100, 100);
            partsTableTitle.Text = "Parts";
            partsTableTitle.Font = new Font("Arial", 12F, FontStyle.Bold);
            this.Controls.Add(partsTableTitle);

            dataGridViewParts.Location = new Point(100, 150);
            dataGridViewParts.Size = new Size(850, 650);
            dataGridViewParts.RowHeadersVisible = false;
            dataGridViewParts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewParts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewParts.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridViewParts.DefaultCellStyle.SelectionForeColor = Color.Black;
            this.Controls.Add(dataGridViewParts);

        }

        private void InitializeProductsTable()
        {
            productsTableTitle.AutoSize = true;
            productsTableTitle.Location = new Point(1000, 100);
            productsTableTitle.Text = "Products";
            productsTableTitle.Font = new Font("Arial", 12F, FontStyle.Bold);
            this.Controls.Add(productsTableTitle);

            dataGridViewProducts.Location = new Point(1000, 150);
            dataGridViewProducts.Size = new Size(850, 650);
            dataGridViewProducts.RowHeadersVisible = false;
            dataGridViewProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewProducts.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridViewProducts.DefaultCellStyle.SelectionForeColor = Color.Black;
            this.Controls.Add(dataGridViewProducts);
        }

        private void LoadData()
        {
            LoadPartsData();
            LoadProductsData();
        }

        private void LoadPartsData()
        {
            dataGridViewParts.DataSource = Inventory.AllParts;
            dataGridViewParts.Columns["PartID"].HeaderText = "Part ID";
            dataGridViewParts.Columns["InStock"].HeaderText = "Inventory";
        }

        private void LoadProductsData()
        {
            dataGridViewProducts.DataSource = Inventory.Products;
            dataGridViewProducts.Columns["ProductID"].HeaderText = "Product ID";
            dataGridViewProducts.Columns["InStock"].HeaderText = "Inventory";
        }

        private void OpenModifyPart(object? sender, EventArgs e)
        {
            if (dataGridViewParts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row.");
                return;
            }
            int selectedRowIndex = dataGridViewParts.SelectedRows[0].Index;
            var selectedPartId = (int)dataGridViewParts.Rows[selectedRowIndex].Cells["PartID"].Value;
            var selectedPart = Inventory.AllParts.FirstOrDefault(part => part.PartID == selectedPartId);

            if (selectedPart == null)
            {
                MessageBox.Show("Selected part not found.");
                return;
            }

            var modifyPart = new ModifyPart(selectedPart);
            modifyPart.Show();
        }

        private void OpenAddProduct(object? sender, EventArgs e)
        {
            var addProduct = new AddProduct();
            addProduct.Show();
        }

        private void OpenModifyProduct(object? sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row.");
                return;
            }

            int selectedRowIndex = dataGridViewProducts.SelectedRows[0].Index;
            var selectedProductID = (int)dataGridViewProducts.Rows[selectedRowIndex].Cells["ProductID"].Value;
            var selectedProduct = Inventory.Products.FirstOrDefault(product => product.ProductID == selectedProductID);

            if (selectedProduct == null)
            {
                MessageBox.Show("Selected product not found.");
                return;
            }

            var modifyProduct = new ModifyProduct(selectedProduct);
            modifyProduct.Show();
        }

        private void OpenAddPart(object? sender, EventArgs e)
        {
            var addPart = new AddPart();
            addPart.Show();
        }

        private void RemoveProduct(object? sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row.");
                return;
            }

            int selectedRowIndex = dataGridViewProducts.SelectedRows[0].Index;
            var selectedProductId = (int)dataGridViewProducts.Rows[selectedRowIndex].Cells["ProductID"].Value;
            var selectedProduct = Inventory.Products.FirstOrDefault(product => product.ProductID == selectedProductId);

            if (selectedProduct == null)
            {
                MessageBox.Show("Selected product not found.");
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure you want to delete Product {selectedProduct.Name} from your inventory?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                Inventory.RemoveProduct(selectedProduct);
                LoadData();
                MessageBox.Show($"{selectedProduct.Name} has been deleted.", "Product Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SearchParts(object? sender, EventArgs e)
        {
            var searchText = partsSearchBox.Text.Trim();
            BindingList<Part> filteredParts = Inventory.LookUpPart(searchText);
            dataGridViewParts.DataSource = filteredParts;

            if (filteredParts.Count == 0)
            {
                MessageBox.Show("No parts found matching the search criteria.");
            }
        }
        private void SearchProducts(object? sender, EventArgs e)
        {
            var searchText = productsSearchBox.Text.Trim();
            BindingList<Product> filteredParts = Inventory.LookupProduct(searchText);
            dataGridViewProducts.DataSource = filteredParts;

            if (filteredParts.Count == 0)
            {
                MessageBox.Show("No product found matching the search criteria.");
            }
        }

        private void RemovePart(object? sender, EventArgs e)
        {
            if (dataGridViewParts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row.");
                return;
            }

            int selectedRowIndex = dataGridViewParts.SelectedRows[0].Index;
            var selectedPartId = (int)dataGridViewParts.Rows[selectedRowIndex].Cells["PartID"].Value;
            var selectedPart = Inventory.AllParts.FirstOrDefault(part => part.PartID == selectedPartId);

            if (selectedPart == null)
            {
                MessageBox.Show("Selected part not found.");
                return;
            }

            var associatedProduct = Inventory.Products.FirstOrDefault(product => product.AssociatedParts.Any(part => part.PartID == selectedPart.PartID));

            if (associatedProduct != null)
            {
                MessageBox.Show($"You can't delete this part because it is associated with {associatedProduct.Name}.", "Part Associated", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure you want to delete Part {selectedPart.Name} from your inventory?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                Inventory.DeletePart(selectedPart);
                LoadData();
                MessageBox.Show($"{selectedPart.Name} has been deleted.", "Part Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
