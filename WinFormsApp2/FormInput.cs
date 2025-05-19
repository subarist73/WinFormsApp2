using ProjectBinaryHeap;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProjectBinaryHeap
{
    public partial class FormInput : Form
    {
        public event EventHandler<HeapParameters> ParametersSubmitted;

        public FormInput()
        {
            InitializeComponent();
        }

        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string input = txtNumbers.Text;
                string[] parts = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                List<int> numbers = new List<int>();
                foreach (string part in parts)
                {
                    numbers.Add(int.Parse(part.Trim()));
                }
                HeapParameters parameters = new HeapParameters(numbers, false);
                ParametersSubmitted?.Invoke(this, parameters);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка ввода: " + ex.Message);
            }
        }
    }
}
