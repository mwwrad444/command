using System;
using System.Globalization;
using System.Windows.Forms;

namespace command
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Ensure operator list and default selection
            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Items.AddRange(new object[] { "+", "-", "*", "/" });
            }
            if (comboBox1.SelectedIndex == -1 && comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;

            // Wire up event handlers
            button1.Click += Button1_Click; // Result
            button2.Click += Button2_Click; // Clear
        }

        private void Button1_Click(object? sender, EventArgs e)
        {
            // Validate empty
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter both numbers.", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Parse
            if (!double.TryParse(textBox1.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double a) ||
                !double.TryParse(textBox2.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double b))
            {
                MessageBox.Show("Enter valid numeric values.", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string op = (comboBox1.SelectedItem?.ToString()) ?? "+";
            double result;

            switch (op)
            {
                case "+":
                    result = a + b;
                    break;
                case "-":
                    result = a - b;
                    break;
                case "*":
                    result = a * b;
                    break;
                case "/":
                    if (Math.Abs(b) < double.Epsilon)
                    {
                        MessageBox.Show("Division by zero is not allowed.", "Math error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        label2.Text = "Error";
                        return;
                    }
                    result = a / b;
                    break;
                default:
                    MessageBox.Show("Select an operator.", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            label2.Text = result.ToString(CultureInfo.CurrentCulture);
        }

        private void Button2_Click(object? sender, EventArgs e)
        {
            textBox1.Text = "0";
            textBox2.Text = "0";
            label2.Text = "0";
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }
    }
}
