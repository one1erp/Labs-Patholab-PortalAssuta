namespace HostUserControl
{
    partial class HostUrlActivate
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOpenPortal = new System.Windows.Forms.Button();
            this.textBoxEnterSDG = new System.Windows.Forms.TextBox();
            this.labelEnterSDG = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOpenPortal
            // 
            this.buttonOpenPortal.Location = new System.Drawing.Point(252, 7);
            this.buttonOpenPortal.Name = "buttonOpenPortal";
            this.buttonOpenPortal.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenPortal.TabIndex = 1;
            this.buttonOpenPortal.Text = "Open Portal";
            this.buttonOpenPortal.UseVisualStyleBackColor = true;
            this.buttonOpenPortal.Click += new System.EventHandler(this.buttonOpenPortal_Click);
            // 
            // textBoxEnterSDG
            // 
            this.textBoxEnterSDG.Location = new System.Drawing.Point(124, 10);
            this.textBoxEnterSDG.Name = "textBoxEnterSDG";
            this.textBoxEnterSDG.Size = new System.Drawing.Size(100, 20);
            this.textBoxEnterSDG.TabIndex = 2;
            this.textBoxEnterSDG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxEnterSDG_KeyDown);
            // 
            // labelEnterSDG
            // 
            this.labelEnterSDG.AutoSize = true;
            this.labelEnterSDG.Location = new System.Drawing.Point(46, 13);
            this.labelEnterSDG.Name = "labelEnterSDG";
            this.labelEnterSDG.Size = new System.Drawing.Size(61, 13);
            this.labelEnterSDG.TabIndex = 3;
            this.labelEnterSDG.Text = "Enter SDG:";
            // 
            // HostUrlActivate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelEnterSDG);
            this.Controls.Add(this.textBoxEnterSDG);
            this.Controls.Add(this.buttonOpenPortal);
            this.Name = "HostUrlActivate";
            this.Size = new System.Drawing.Size(1061, 552);
            this.SizeChanged += new System.EventHandler(this.HostUrlActivate_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenPortal;
        private System.Windows.Forms.TextBox textBoxEnterSDG;
        private System.Windows.Forms.Label labelEnterSDG;
    }
}
