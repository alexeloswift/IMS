using System.Windows.Forms;
using System.Drawing;

namespace MyWinFormsApp
{
    public partial class ModifyPart : BaseForm
    {
        // MARK: - Properties

        private RadioButton radioButtonInHouse = new();
        private RadioButton radioButtonOutsourced = new();
        private TextBox textBoxID = new();
        private TextBox textBoxName = new();
        private TextBox textBoxInventory = new();
        private TextBox textBoxPrice = new();
        private TextBox textBoxMax = new();
        private TextBox textBoxMin = new();
        private TextBox textBoxMachineId = new();
        private Label labelMachineId = new();
        private TextBox textBoxCompanyName = new();
        private Label labelCompanyName = new();
        private Part selectedPart;

        // MARK: - Main

        public ModifyPart(Part part)
        {
            InitializeComponent();
            selectedPart = part;
            InitializeButtonsAndTextBox();
            PopulateFields();
        }

        // MARK: - Main Methods

        private void PopulateFields()
        {
            textBoxID.Text = selectedPart.PartID.ToString();
            textBoxID.ReadOnly = true;

            textBoxName.Text = selectedPart.Name;
            textBoxInventory.Text = selectedPart.InStock.ToString();
            textBoxPrice.Text = selectedPart.Price.ToString();
            textBoxMax.Text = selectedPart.Max.ToString();
            textBoxMin.Text = selectedPart.Min.ToString();

            if (selectedPart is Inhouse inhousePart)
            {
                radioButtonInHouse.Checked = true;
                textBoxMachineId.Text = inhousePart.MachineID.ToString();
                textBoxMachineId.Visible = true;
                labelMachineId.Visible = true;
                textBoxCompanyName.Visible = false;
                labelCompanyName.Visible = false;
            }
            else if (selectedPart is Outsourced outsourcedPart)
            {
                radioButtonOutsourced.Checked = true;
                textBoxCompanyName.Text = outsourcedPart.CompanyName;
                textBoxCompanyName.Visible = true;
                labelCompanyName.Visible = true;
                textBoxMachineId.Visible = false;
                labelMachineId.Visible = false;
            }
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
                    selectedPart.Name = textBoxName.Text;
                    selectedPart.InStock = int.Parse(textBoxInventory.Text);
                    selectedPart.Price = decimal.Parse(textBoxPrice.Text);
                    selectedPart.Max = int.Parse(textBoxMax.Text);
                    selectedPart.Min = int.Parse(textBoxMin.Text);

                    if (selectedPart is Inhouse inhousePart)
                    {
                        inhousePart.MachineID = int.Parse(textBoxMachineId.Text);
                    }
                    else if (selectedPart is Outsourced outsourcedPart)
                    {
                        outsourcedPart.CompanyName = textBoxCompanyName.Text;
                    }
                    Inventory.UpdatePart(selectedPart.PartID, selectedPart);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void InitializeButtonsAndTextBox()
        {
            radioButtonInHouse.Text = "In-house";
            radioButtonInHouse.Location = new Point(250, 25);
            radioButtonInHouse.AutoSize = true;
            radioButtonInHouse.Checked = true;
            radioButtonOutsourced.Text = "Outsourced";
            radioButtonOutsourced.Location = new Point(450, 25);
            radioButtonOutsourced.AutoSize = true;

            radioButtonInHouse.CheckedChanged += RadioButton_CheckedChanged;
            radioButtonOutsourced.CheckedChanged += RadioButton_CheckedChanged;
            this.Controls.Add(radioButtonInHouse);
            this.Controls.Add(radioButtonOutsourced);

            textBoxID = AddLabeledTextBox(new Point(350, 100), new Size(300, 50), "ID");
            textBoxName = AddLabeledTextBox(new Point(350, 150), new Size(300, 50), "Name");
            textBoxInventory = AddLabeledTextBox(new Point(350, 200), new Size(300, 50), "Inventory");
            textBoxPrice = AddLabeledTextBox(new Point(350, 250), new Size(300, 50), "Price / Cost");
            textBoxMax = AddLabeledTextBox(new Point(350, 300), new Size(110, 50), "Max");
            textBoxMin = AddLabeledTextBox(new Point(540, 300), new Size(110, 50), "Min");

            textBoxMachineId = AddTextBox(new Point(350, 350), new Size(300, 50));
            labelMachineId = AddLabel(new Point(200, 350), "Machine ID");

            textBoxCompanyName = AddTextBox(new Point(350, 350), new Size(300, 50));
            labelCompanyName = AddLabel(new Point(160, 350), "Company Name");
            textBoxCompanyName.Visible = false;
            labelCompanyName.Visible = false;

            AddButton("Save", new Point(350, 500), Save);
            AddButton("Cancel", new Point(525, 500), Cancel);
        }

        private void RadioButton_CheckedChanged(object? sender, EventArgs? e)
        {
            bool isInHouseChecked = radioButtonInHouse.Checked;
            bool isOutsourcedChecked = radioButtonOutsourced.Checked;

            if (selectedPart is Inhouse && isOutsourcedChecked)
            {
                ConvertPartToOutsourced();
            }
            else if (selectedPart is Outsourced && isInHouseChecked)
            {
                ConvertPartToInhouse();
            }
            textBoxMachineId.Visible = labelMachineId.Visible = isInHouseChecked;
            textBoxCompanyName.Visible = labelCompanyName.Visible = isOutsourcedChecked;
        }

        private void Cancel(object? sender, EventArgs e)
        {
            this.Close();
        }

        // MARK: - Helper Methods

        private void ConvertPartToOutsourced()
        {
            if (selectedPart is Inhouse inhousePart)
            {
                selectedPart = new Outsourced(
                    inhousePart.PartID,
                    inhousePart.Name,
                    inhousePart.Price,
                    inhousePart.InStock,
                    inhousePart.Min,
                    inhousePart.Max,
                    textBoxCompanyName.Text
                );
            }
            radioButtonOutsourced.Checked = true;
            PopulateFields();
        }

        private void ConvertPartToInhouse()
        {
            if (selectedPart is Outsourced outsourcedPart)
            {
                selectedPart = new Inhouse(
                    outsourcedPart.PartID,
                    outsourcedPart.Name,
                    outsourcedPart.Price,
                    outsourcedPart.InStock,
                    outsourcedPart.Min,
                    outsourcedPart.Max,
                    int.Parse(textBoxMachineId.Text)
                );
            }

            radioButtonInHouse.Checked = true;
            PopulateFields();
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
            if (radioButtonInHouse.Checked)
            {
                if (!int.TryParse(textBoxMachineId.Text, out _))
                {
                    textBoxMachineId.BackColor = Color.LightPink;
                    isValid = false;
                    MessageBox.Show("Machine ID must be a numeric value.");
                }
                else
                {
                    textBoxMachineId.BackColor = Color.White;
                }
            }
            return isValid;
        }
    }
}
