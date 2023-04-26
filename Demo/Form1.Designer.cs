namespace Demo;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        kryptonButton1 = new Krypton.Toolkit.KryptonButton();
        SuspendLayout();
        // 
        // kryptonButton1
        // 
        kryptonButton1.CornerRoundingRadius = -1F;
        kryptonButton1.Location = new Point(675, 12);
        kryptonButton1.Name = "kryptonButton1";
        kryptonButton1.Size = new Size(113, 38);
        kryptonButton1.TabIndex = 0;
        kryptonButton1.Values.Text = "测试";
        kryptonButton1.Click += kryptonButton1_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(kryptonButton1);
        Name = "Form1";
        Text = "Form1";
        ResumeLayout(false);
    }

    #endregion

    private Krypton.Toolkit.KryptonButton kryptonButton1;
}
