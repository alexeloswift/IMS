using System.Windows.Forms;
using System.ComponentModel;

namespace MyWinFormsApp
{
    public partial class AddProduct : BaseForm
    {
        // MARK: - Properties

        private Product? product;
        private DataGridView dataGridViewAllParts;
        private DataGridView dataGridViewAssociatedParts;
        private Label allPartsTableTitle;
        private Label associatedPartsTableTitle;
        private TextBox textBoxID = new();
        private TextBox textBoxName = new();
        private TextBox textBoxInventory = new();
        private TextBox textBoxPrice = new();
        private TextBox textBoxMax = new();
        private TextBox textBoxMin = new();
        private Part? selectedCandidatePart;
        private Part? selectedAssociatedPart;
        private BindingList<Part> AssociatedParts;
        private TextBox partsSearchBox;

        // MARK: - Main

        public AddProduct()
        {
            dataGridViewAllParts = new DataGridView();
            dataGridViewAssociatedParts = new DataGridView();
            allPartsTableTitle = new Label();
            associatedPartsTableTitle = new Label();
            AssociatedParts = new BindingList<Part>();
            partsSearchBox = new TextBox();

            InitializeComponent();
            InitializeControls();
            int newProductID = GenerateNewProductID();
            textBoxID.Text = newProductID.ToString();
            textBoxID.ReadOnly = true;
        }

        // MARK: - Main Methods

        private void InitializeControls()
        {
            InitializeButtonsAndTextBox();
            InitializeAllPartsTable();
            InitializeAssociatedPartTable();
            LoadAllPartsData();
            LoadAssociatedPartsData();
        }

        private void InitializeButtonsAndTextBox()
        {
            partsSearchBox = AddTextBox(new Point(1650, 25), new Size(300, 50));
            textBoxID = AddLabeledTextBox(new Point(350, 200), new Size(300, 150), "ID");
            textBoxName = AddLabeledTextBox(new Point(350, 300), new Size(300, 200), "Name");
            textBoxInventory = AddLabeledTextBox(new Point(350, 400), new Size(300, 250), "Inventory");
            textBoxPrice = AddLabeledTextBox(new Point(350, 500), new Size(300, 300), "Price / Cost");
            textBoxMax = AddLabeledTextBox(new Point(350, 600), new Size(110, 350), "Max");
            textBoxMin = AddLabeledTextBox(new Point(540, 600), new Size(110, 400), "Min");
            AddButton("Search", new Point(1500, 25), SearchParts);
            AddButton("Add", new Point(1400, 465), AddPartToAssociatedParts);
            AddButton("Save", new Point(350, 750), Save);
            AddButton("Cancel", new Point(500, 750), Cancel);
            AddButton("Delete", new Point(1400, 915), DeleteAssociatedPart);

        }
        private void InitializeAllPartsTable()
        {
            allPartsTableTitle.AutoSize = true;
            allPartsTableTitle.Location = new Point(900, 60);
            allPartsTableTitle.Text = "All Candidate Parts";
            allPartsTableTitle.Font = new Font("Arial", 8F);
            this.Controls.Add(allPartsTableTitle);

            dataGridViewAllParts.Location = new Point(900, 100);
            dataGridViewAllParts.Size = new Size(1050, 350);
            dataGridViewAllParts.RowHeadersVisible = false;
            dataGridViewAllParts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewAllParts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewAllParts.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridViewAllParts.DefaultCellStyle.SelectionForeColor = Color.Black;
            this.Controls.Add(dataGridViewAllParts);
        }

        private void InitializeAssociatedPartTable()
        {
            associatedPartsTableTitle.AutoSize = true;
            associatedPartsTableTitle.Location = new Point(900, 510);
            associatedPartsTableTitle.Text = "Parts Associated with this Product";
            associatedPartsTableTitle.Font = new Font("Arial", 8F);
            this.Controls.Add(associatedPartsTableTitle);

            dataGridViewAssociatedParts.Location = new Point(900, 550);
            dataGridViewAssociatedParts.Size = new Size(1050, 350);
            dataGridViewAssociatedParts.RowHeadersVisible = false;
            dataGridViewAssociatedParts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewAssociatedParts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewAssociatedParts.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridViewAssociatedParts.DefaultCellStyle.SelectionForeColor = Color.Black;
            this.Controls.Add(dataGridViewAssociatedParts);
        }

        private void AddPartToAssociatedParts(object? sender, EventArgs e)
        {
            if (dataGridViewAllParts.SelectedRows.Count > 0)
            {
                selectedCandidatePart = (Part)dataGridViewAllParts.SelectedRows[0].DataBoundItem;
                AssociatedParts.Add(selectedCandidatePart);
                LoadAssociatedPartsData();
                MessageBox.Show($"Part {selectedCandidatePart.Name} has been added to the associated parts list.");
            }
            else
            {
                MessageBox.Show("Please select a part to add.");
            }
        }

        private void DeleteAssociatedPart(object? sender, EventArgs e)
        {
            if (dataGridViewAssociatedParts.SelectedRows.Count > 0)
            {
                selectedAssociatedPart = (Part)dataGridViewAssociatedParts.SelectedRows[0].DataBoundItem;
                var result = MessageBox.Show($"Are you sure you want to remove Part {selectedAssociatedPart.Name} from the associated parts?", "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    AssociatedParts.Remove(selectedAssociatedPart);
                    LoadAssociatedPartsData();
                    MessageBox.Show($"Part {selectedAssociatedPart.Name} has been deleted from associated parts list.");
                }
            }
            else
            {
                MessageBox.Show("Please select a part to delete.");
            }
        }

        private void LoadAllPartsData()
        {
            dataGridViewAllParts.DataSource = Inventory.AllParts;
            dataGridViewAllParts.Columns["PartID"].HeaderText = "Part ID";
            dataGridViewAllParts.Columns["InStock"].HeaderText = "Inventory";
        }

        private void LoadAssociatedPartsData()
        {
            dataGridViewAssociatedParts.DataSource = AssociatedParts;
            dataGridViewAssociatedParts.Columns["PartID"].HeaderText = "Part ID";
            dataGridViewAssociatedParts.Columns["InStock"].HeaderText = "Inventory";
        }

        private void Save(object? sender, EventArgs e)
        {
            bool isValid = ValidateAllTextBoxes();

            if (!isValid)
            {
                MessageBox.Show("Please correct the highlighted fields.");
                return;
            }
            else if (isValid)
            {
                try
                {
                    bool minMaxValid = ValidateMinMaxInventory(textBoxMin, textBoxMax, textBoxInventory);
                    if (!minMaxValid)
                    {
                        return;
                    }
                    product = new Product(
                        int.Parse(textBoxID.Text),
                        textBoxName.Text,
                        int.Parse(textBoxInventory.Text),
                        decimal.Parse(textBoxPrice.Text),
                        int.Parse(textBoxMax.Text),
                        int.Parse(textBoxMin.Text),
                        AssociatedParts
                        );

                    Inventory.AddProduct(product);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void SearchParts(object? sender, EventArgs e)
        {
            var searchText = partsSearchBox.Text.Trim();
            BindingList<Part> filteredParts = Inventory.LookUpPart(searchText);
            dataGridViewAllParts.DataSource = filteredParts;

            if (filteredParts.Count == 0)
            {
                MessageBox.Show("No parts found matching the search criteria.");
            }
        }

        private void Cancel(object? sender, EventArgs e)
        {
            this.Close();
        }

        // MARK: - Helper Methods

        private static int GenerateNewProductID()
        {
            if (Inventory.Products.Count == 0)
            {
                return 1;
            }
            return Inventory.Products.Max(product => product.ProductID) + 1;
        }

        private bool ValidateAllTextBoxes()
        {
            bool isValid = true;

            if (!decimal.TryParse(textBoxPrice.Text, out _))
            {
                textBoxPrice.BackColor = Color.LightPink;
                isValid = false;
                MessageBox.Show("Price must be a numeric value.");
            }
            else
            {
                textBoxPrice.BackColor = Color.White;
            }

            if (!int.TryParse(textBoxInventory.Text, out _))
            {
                textBoxInventory.BackColor = Color.LightPink;
                isValid = false;
                MessageBox.Show("Inventory must be a numeric value.");
            }
            else
            {
                textBoxInventory.BackColor = Color.White;
            }

            if (!int.TryParse(textBoxMax.Text, out _))
            {
                textBoxMax.BackColor = Color.LightPink;
                isValid = false;
                MessageBox.Show("Max must be a numeric value.");
            }
            else
            {
                textBoxMax.BackColor = Color.White;
            }

            if (!int.TryParse(textBoxMin.Text, out _))
            {
                textBoxMin.BackColor = Color.LightPink;
                isValid = false;
                MessageBox.Show("Min must be a numeric value.");
            }
            else
            {
                textBoxMin.BackColor = Color.White;
            }
            return isValid;
        }
    }
}