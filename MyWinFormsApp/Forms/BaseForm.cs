using System.ComponentModel;

namespace MyWinFormsApp
{
    public class BaseForm : Form
    {
        // MARK: - Methods
        
        protected Label AddLabel(Point location, string text)
        {
            Label label = new()
            {
                Text = text,
                Location = location,
                AutoSize = true
            };
            this.Controls.Add(label);
            return label;
        }

        protected Button AddButton(string buttonText, Point location, EventHandler clickAction)
        {
            Button newButton = new()
            {
                Text = buttonText,
                Location = location,
                Size = new Size(125, 40)
            };

            newButton.Click += clickAction;
            this.Controls.Add(newButton);
            return newButton;
        }

        protected TextBox AddLabeledTextBox(Point textBoxLocation, Size textBoxSize, string labelText)
        {
            TextBox textBox = AddTextBox(textBoxLocation, textBoxSize);

            Label label = new()
            {
                Text = labelText,
                Location = new Point(textBoxLocation.X - (textBoxSize.Width / 2) - 15, textBoxLocation.Y),
                AutoSize = true
            };

            this.Controls.Add(label);
            return textBox;
        }

        protected TextBox AddTextBox(Point location, Size size)
        {
            TextBox newTextBox = new()
            {
                Location = location,
                Size = size,
            };

            this.Controls.Add(newTextBox);
            return newTextBox;
        }

        protected static bool ValidateMinMaxInventory(TextBox minTextBox, TextBox maxTextBox, TextBox inventoryTextBox)
        {
            int min = int.Parse(minTextBox.Text); 
            int max = int.Parse(maxTextBox.Text); 
            int inventory = int.Parse(inventoryTextBox.Text);

            if (min > max)
            {
                MessageBox.Show("Min cannot be greater than Max.");
                minTextBox.Focus();
                return false;
            }

            if (inventory < min || inventory > max)
            {
                MessageBox.Show("Inventory must be between Min and Max.");
                inventoryTextBox.Focus();
                return false;
            }
            return true;
        }
    }
}
