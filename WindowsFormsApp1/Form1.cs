using System.Timers;
using System.Windows.Forms;
using System;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Label resultLabel;
        private System.Timers.Timer timer;

        public Form1()
        {
            InitializeComponent();
            InitializeResultLabel();
        }

        private void InitializeResultLabel()
        {
            resultLabel = new Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(10, 50),
                Visible = false
            };
            this.Controls.Add(resultLabel);
        }

        private string NumToRoman(string original)
        {
            if (string.IsNullOrEmpty(original) || !int.TryParse(original, out int number) || number <= 0)
            {
                MessageBox.Show("Please enter a valid positive integer.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "err";
            }

            var romanNums = new (int Value, string Roman)[]
            {
                (1000, "M"),
                (900, "CM"),
                (500, "D"),
                (400, "CD"),
                (100, "C"),
                (90, "XC"),
                (50, "L"),
                (40, "XL"),
                (10, "X"),
                (9, "IX"),
                (5, "V"),
                (4, "IV"),
                (1, "I")
            };

            string result = string.Empty;
            foreach (var (Value, Roman) in romanNums)
            {
                while (number >= Value)
                {
                    result += Roman;
                    number -= Value;
                }
            }
            return result;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string romanResult = NumToRoman(textBox1.Text);
            if (romanResult != "err")
            {
                resultLabel.Text = romanResult;
                resultLabel.Visible = true;

                timer = new System.Timers.Timer(4000);
                timer.Elapsed += OnTimedEvent;
                timer.AutoReset = false;
                timer.Start();
            }
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate {
                resultLabel.Visible = false;
            });
        }
    }
}