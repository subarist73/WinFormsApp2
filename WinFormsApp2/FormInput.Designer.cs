namespace ProjectBinaryHeap
{
    partial class FormInput
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.TextBox txtNumbers;
        private System.Windows.Forms.Button btnSubmit;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblInstruction = new Label();
            txtNumbers = new TextBox();
            btnSubmit = new Button();
            SuspendLayout();
            // 
            // lblInstruction
            // 
            lblInstruction.AutoSize = true;
            lblInstruction.Location = new Point(10, 20);
            lblInstruction.Name = "lblInstruction";
            lblInstruction.Size = new Size(217, 20);
            lblInstruction.TabIndex = 0;
            lblInstruction.Text = "Введите числа через запятую:";
            // 
            // txtNumbers
            // 
            txtNumbers.Location = new Point(10, 58);
            txtNumbers.Name = "txtNumbers";
            txtNumbers.Size = new Size(404, 27);
            txtNumbers.TabIndex = 1;
            // 
            // btnSubmit
            // 
            btnSubmit.Location = new Point(10, 105);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(134, 51);
            btnSubmit.TabIndex = 2;
            btnSubmit.Text = "Запуск";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += ButtonSubmit_Click;
            // 
            // FormInput
            // 
            ClientSize = new Size(482, 171);
            Controls.Add(btnSubmit);
            Controls.Add(txtNumbers);
            Controls.Add(lblInstruction);
            Name = "FormInput";
            Text = "Ввод параметров кучи";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}