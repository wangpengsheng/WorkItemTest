namespace Demo
{
    partial class TaskFm
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
            kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            kryptonDataGridView1 = new Krypton.Toolkit.KryptonDataGridView();
            kryptonDataGridView2 = new Krypton.Toolkit.KryptonDataGridView();
            kryptonButton2 = new Krypton.Toolkit.KryptonButton();
            kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            kryptonCheckedListBox1 = new Krypton.Toolkit.KryptonCheckedListBox();
            kryptonCheckedListBox2 = new Krypton.Toolkit.KryptonCheckedListBox();
            ((System.ComponentModel.ISupportInitialize)kryptonDataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonDataGridView2).BeginInit();
            SuspendLayout();
            // 
            // kryptonButton1
            // 
            kryptonButton1.CornerRoundingRadius = -1F;
            kryptonButton1.Location = new Point(790, 12);
            kryptonButton1.Name = "kryptonButton1";
            kryptonButton1.Size = new Size(113, 38);
            kryptonButton1.TabIndex = 1;
            kryptonButton1.Values.Text = "串行";
            kryptonButton1.Click += kryptonButton1_Click;
            // 
            // kryptonDataGridView1
            // 
            kryptonDataGridView1.Location = new Point(12, 12);
            kryptonDataGridView1.Name = "kryptonDataGridView1";
            kryptonDataGridView1.RowTemplate.Height = 25;
            kryptonDataGridView1.Size = new Size(628, 213);
            kryptonDataGridView1.TabIndex = 2;
            // 
            // kryptonDataGridView2
            // 
            kryptonDataGridView2.Location = new Point(12, 251);
            kryptonDataGridView2.Name = "kryptonDataGridView2";
            kryptonDataGridView2.RowTemplate.Height = 25;
            kryptonDataGridView2.Size = new Size(632, 214);
            kryptonDataGridView2.TabIndex = 2;
            // 
            // kryptonButton2
            // 
            kryptonButton2.CornerRoundingRadius = -1F;
            kryptonButton2.Location = new Point(790, 251);
            kryptonButton2.Name = "kryptonButton2";
            kryptonButton2.Size = new Size(113, 38);
            kryptonButton2.TabIndex = 1;
            kryptonButton2.Values.Text = "并行";
            kryptonButton2.Click += kryptonButton2_Click;
            // 
            // kryptonLabel1
            // 
            kryptonLabel1.AutoSize = false;
            kryptonLabel1.Location = new Point(664, 12);
            kryptonLabel1.Name = "kryptonLabel1";
            kryptonLabel1.Size = new Size(100, 21);
            kryptonLabel1.TabIndex = 4;
            kryptonLabel1.Values.Text = "";
            // 
            // kryptonLabel2
            // 
            kryptonLabel2.AutoSize = false;
            kryptonLabel2.Location = new Point(664, 251);
            kryptonLabel2.Name = "kryptonLabel2";
            kryptonLabel2.Size = new Size(100, 21);
            kryptonLabel2.TabIndex = 4;
            kryptonLabel2.Values.Text = "";
            // 
            // kryptonCheckedListBox1
            // 
            kryptonCheckedListBox1.CornerRoundingRadius = -1F;
            kryptonCheckedListBox1.ItemCornerRoundingRadius = -1F;
            kryptonCheckedListBox1.Items.AddRange(new object[] { "暂停", "继续", "停止" });
            kryptonCheckedListBox1.Location = new Point(790, 56);
            kryptonCheckedListBox1.Name = "kryptonCheckedListBox1";
            kryptonCheckedListBox1.Size = new Size(113, 89);
            kryptonCheckedListBox1.TabIndex = 5;
            kryptonCheckedListBox1.SelectedIndexChanged += kryptonCheckedListBox1_SelectedIndexChanged;
            // 
            // kryptonCheckedListBox2
            // 
            kryptonCheckedListBox2.CornerRoundingRadius = -1F;
            kryptonCheckedListBox2.ItemCornerRoundingRadius = -1F;
            kryptonCheckedListBox2.Items.AddRange(new object[] { "暂停", "继续", "停止" });
            kryptonCheckedListBox2.Location = new Point(790, 295);
            kryptonCheckedListBox2.Name = "kryptonCheckedListBox2";
            kryptonCheckedListBox2.Size = new Size(113, 89);
            kryptonCheckedListBox2.TabIndex = 5;
            // 
            // TaskFm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(915, 546);
            Controls.Add(kryptonCheckedListBox2);
            Controls.Add(kryptonCheckedListBox1);
            Controls.Add(kryptonLabel2);
            Controls.Add(kryptonLabel1);
            Controls.Add(kryptonDataGridView2);
            Controls.Add(kryptonDataGridView1);
            Controls.Add(kryptonButton2);
            Controls.Add(kryptonButton1);
            Name = "TaskFm";
            Text = "TaskFm";
            Load += TaskFm_Load;
            ((System.ComponentModel.ISupportInitialize)kryptonDataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonDataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonButton kryptonButton1;
        private Krypton.Toolkit.KryptonDataGridView kryptonDataGridView1;
        private Krypton.Toolkit.KryptonDataGridView kryptonDataGridView2;
        private Krypton.Toolkit.KryptonButton kryptonButton2;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonCheckedListBox kryptonCheckedListBox1;
        private Krypton.Toolkit.KryptonCheckedListBox kryptonCheckedListBox2;
    }
}