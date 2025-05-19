namespace WinFormsApp2
{
    partial class FormBinaryHeap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label1 = new Label();
            button4 = new Button();
            button5 = new Button();
            button7 = new Button();
            menuStrip1 = new MenuStrip();
            toolStripComboBox1 = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            поставьтеМаксБаллToolStripMenuItem = new ToolStripMenuItem();
            слЗагрToolStripMenuItem = new ToolStripMenuItem();
            button8 = new Button();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 57);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(776, 392);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(1, 22);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 1;
            button1.Text = "дан";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(101, 22);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 2;
            button2.Text = "шаг";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(201, 22);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 3;
            button3.Text = "иниц";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 63);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 4;
            label1.Text = "label1";
            // 
            // button4
            // 
            button4.Location = new Point(301, 22);
            button4.Name = "button4";
            button4.Size = new Size(94, 29);
            button4.TabIndex = 5;
            button4.Text = "пр сохр";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(401, 22);
            button5.Name = "button5";
            button5.Size = new Size(94, 29);
            button5.TabIndex = 6;
            button5.Text = "пр загр";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button7
            // 
            button7.Location = new Point(501, 22);
            button7.Name = "button7";
            button7.Size = new Size(94, 29);
            button7.TabIndex = 8;
            button7.Text = "форма";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripComboBox1, toolStripMenuItem1 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 9;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // toolStripComboBox1
            // 
            toolStripComboBox1.Name = "toolStripComboBox1";
            toolStripComboBox1.Size = new Size(14, 24);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { toolStripSeparator1, поставьтеМаксБаллToolStripMenuItem, слЗагрToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(121, 24);
            toolStripMenuItem1.Text = "сл сохр и загр";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(140, 6);
            // 
            // поставьтеМаксБаллToolStripMenuItem
            // 
            поставьтеМаксБаллToolStripMenuItem.Name = "поставьтеМаксБаллToolStripMenuItem";
            поставьтеМаксБаллToolStripMenuItem.Size = new Size(143, 26);
            поставьтеМаксБаллToolStripMenuItem.Text = "сл сохр";
            поставьтеМаксБаллToolStripMenuItem.Click += поставьтеМаксБаллToolStripMenuItem_Click;
            // 
            // слЗагрToolStripMenuItem
            // 
            слЗагрToolStripMenuItem.Name = "слЗагрToolStripMenuItem";
            слЗагрToolStripMenuItem.Size = new Size(143, 26);
            слЗагрToolStripMenuItem.Text = "сл загр";
            слЗагрToolStripMenuItem.Click += слЗагрToolStripMenuItem_Click;
            // 
            // button8
            // 
            button8.Location = new Point(601, 22);
            button8.Name = "button8";
            button8.Size = new Size(94, 29);
            button8.TabIndex = 10;
            button8.Text = "добавить элемент";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(701, 24);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(99, 27);
            textBox1.TabIndex = 11;
            // 
            // FormBinaryHeap
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox1);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(label1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FormBinaryHeap";
            Text = "FormBinaryHeap";
            Load += FormBinaryHeap_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Label label1;
        private Button button4;
        private Button button5;
        private Button button7;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripComboBox1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem поставьтеМаксБаллToolStripMenuItem;
        private ToolStripMenuItem слЗагрToolStripMenuItem;
        private Button button8;
        private TextBox textBox1;
    }
}