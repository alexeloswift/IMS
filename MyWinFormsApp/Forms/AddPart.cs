using System.Windows.Forms;
using System.Drawing;

namespace MyWinFormsApp
{
    public partial class AddPart : BaseForm
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

        // MARK: - Main

        public AddPart()
        {
            InitializeComponent();
            InitializeButtonsAndTextBox();
            int newPartID = GenerateNewPartID();
            textBoxID.Text = newPartID.ToString();
            textBoxID.ReadOnly = true;
        }

        // MARK: - Main Methods

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
                    int partID = int.Parse(textBoxID.Text);
                    string name = textBoxName.Text;
                    int inventory = int.Parse(textBoxInventory.Text);
                    decimal price = decimal.Parse(textBoxPrice.Text);
                    int max = int.Parse(textBoxMax.Text);
                    int min = int.Parse(textBoxMin.Text);

                    Part newPart;

                    if (radioButtonInHouse.Checked)
                    {
                        int machineID = int.Parse(textBoxMachineId.Text);
                        newPart = new Inhouse(partID, name, price, inventory, min, max, machineID);
                    }
                    else if (radioButtonOutsourced.Checked)
                    {
                        string companyName = textBoxCompanyName.Text;
                        newPart = new Outsourced(partID, name, price, inventory, min, max, companyName);
                    }
                    else
                    {
                        throw new Exception("Please select either In-house or Outsourced.");
                    }

                    Inventory.AddPart(newPart);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void Cancel(object? sender, EventArgs e)
        {
            this.Close();
        }

        // MARK: - Helper Methods

        private static int GenerateNewPartID()
        {
            if (Inventory.AllParts.Count == 0)
            {
                return 1;
            }
            return Inventory.AllParts.Max(part => part.PartID) + 1;
        }

        private void RadioButton_CheckedChanged(object? sender, EventArgs e)
        {
            if (radioButtonInHouse.Checked)
            {
                textBoxMachineId.Visible = true;
                labelMachineId.Visible = true;
                textBoxCompanyName.Visible = false;
                labelCompanyName.Visible = false;
            }
            else if (radioButtonOutsourced.Checked)
            {
                textBoxMachineId.Visible = false;
                labelMachineId.Visible = false;
                textBoxCompanyName.Visible = true;
                labelCompanyName.Visible = true;
            }
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

