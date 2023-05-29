using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RenTradeWindowForm.Helper
{
    public static class DialogBox
    {
        public static DialogResult ShowInputDialogBox(ref string input, string prompt, string title = "Title", int width = 300, int height = 200)
        {
            //This function creates the custom input dialog box by individually creating the different window elements and adding them to the dialog box

            //Specify the size of the window using the parameters passed
            Size size = new Size(width, height);
            //Create a new form using a System.Windows Form
            Form inputBox = new Form()
            {
                ControlBox = false,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                StartPosition = FormStartPosition.CenterScreen,
                TopMost = true,
                ClientSize = size,
                Text = title,
                ShowInTaskbar = false
            };

            inputBox.MaximizeBox = false;
            inputBox.MinimizeBox = false;

            //Create a new label to hold the prompt
            Label label = new Label()
            {
                Text = prompt,
                Location = new Point(5, 10),
                Width = size.Width - 10
            };

            //Create a textbox to accept the user's input
            TextBox textBox = new TextBox()
            {
                Size = new Size(size.Width - 10, 30),
                Location = new Point(5, label.Location.Y + 25),
                BorderStyle = BorderStyle.Fixed3D,
                Font = new Font("Segoe UI", 11, FontStyle.Regular, GraphicsUnit.Point),
                Multiline = true,
                Text = input,
                TabIndex = 0,
                TabStop = true
            };

            textBox.Focus();

            //Create an OK Button 
            Button okButton = new Button() 
            {

                DialogResult = DialogResult.OK,
                Name = "okButton",
                Size = new Size(75, 23),
                Text = "&OK",
                Location = new Point(size.Width - 80, size.Height - 30),
                TabIndex = 1,
                TabStop = true
            };

            inputBox.Controls.Add(textBox);
            inputBox.Controls.Add(okButton);
            inputBox.Controls.Add(label);

            ////Create a Cancel Button
            //Button cancelButton = new Button();
            //cancelButton.DialogResult = DialogResult.Cancel;
            //cancelButton.Name = "cancelButton";
            //cancelButton.Size = new Size(75, 23);
            //cancelButton.Text = "&Cancel";
            //cancelButton.Location = new Point(size.Width - 80, size.Height - 30);
            //inputBox.Controls.Add(cancelButton);

            inputBox.Focus();

            //Set the input box's buttons to the created OK and Cancel Buttons respectively so the window appropriately behaves with the button clicks
            inputBox.AcceptButton = okButton;
            //inputBox.CancelButton = cancelButton;

            //Show the window dialog box 
            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;

            //After input has been submitted, return the input value
            return result;
        }

        public static string ShowDialog(string caption, string text, string selStr)
        {
            Form prompt = new Form();
            prompt.Width = 280;
            prompt.Height = 160;
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 16, Top = 20, Width = 240, Text = text };
            TextBox textBox = new TextBox() { Left = 16, Top = 40, Width = 240, TabIndex = 0, TabStop = true };
            Label selLabel = new Label() { Left = 16, Top = 66, Width = 88, Text = selStr };
            ComboBox cmbx = new ComboBox() { Left = 112, Top = 64, Width = 144 };
            cmbx.Items.Add("Dark Grey");
            cmbx.Items.Add("Orange");
            cmbx.Items.Add("None");
            Button confirmation = new Button() { Text = "In Ordnung!", Left = 16, Width = 80, Top = 88, TabIndex = 1, TabStop = true };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(selLabel);
            prompt.Controls.Add(cmbx);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;
            prompt.StartPosition = FormStartPosition.CenterScreen;
            prompt.ShowDialog();
            return string.Format("{0};{1}", textBox.Text, cmbx.SelectedItem.ToString());
        }
    }
}
