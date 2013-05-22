namespace ExpressionTreeVisualizer
{
    partial class ExpressionTreeVisualizerForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.treeBrowser1 = new ExpressionTreeVisualizer.TreeBrowser();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(491, 54);
            this.textBox1.TabIndex = 0;
            // 
            // treeBrowser1
            // 
            this.treeBrowser1.Location = new System.Drawing.Point(0, 63);
            this.treeBrowser1.Name = "treeBrowser1";
            this.treeBrowser1.Size = new System.Drawing.Size(491, 549);
            this.treeBrowser1.TabIndex = 1;
            // 
            // ExpressionTreeVisualizerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 611);
            this.Controls.Add(this.treeBrowser1);
            this.Controls.Add(this.textBox1);
            this.Name = "ExpressionTreeVisualizerForm";
            this.Text = "ExpressionTreeVisualizerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private TreeBrowser treeBrowser1;
    }
}