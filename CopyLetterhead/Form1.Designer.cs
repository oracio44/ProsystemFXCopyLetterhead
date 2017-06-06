namespace CopyLetterhead
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.lbWFX32 = new System.Windows.Forms.Label();
            this.chkOfficeGroup = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "Select Letterhead";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbWFX32
            // 
            this.lbWFX32.AutoSize = true;
            this.lbWFX32.Location = new System.Drawing.Point(12, 29);
            this.lbWFX32.Name = "lbWFX32";
            this.lbWFX32.Size = new System.Drawing.Size(35, 13);
            this.lbWFX32.TabIndex = 1;
            this.lbWFX32.Text = "label1";
            // 
            // chkOfficeGroup
            // 
            this.chkOfficeGroup.AutoSize = true;
            this.chkOfficeGroup.Checked = true;
            this.chkOfficeGroup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOfficeGroup.Location = new System.Drawing.Point(12, 96);
            this.chkOfficeGroup.Name = "chkOfficeGroup";
            this.chkOfficeGroup.Size = new System.Drawing.Size(124, 17);
            this.chkOfficeGroup.TabIndex = 2;
            this.chkOfficeGroup.Text = "Update Office Group";
            this.chkOfficeGroup.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "WFX32 Location : ";
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.Checked = true;
            this.chkOverwrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOverwrite.Location = new System.Drawing.Point(12, 119);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(134, 17);
            this.chkOverwrite.TabIndex = 4;
            this.chkOverwrite.Text = "Overwrite Existing Files";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 145);
            this.Controls.Add(this.chkOverwrite);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkOfficeGroup);
            this.Controls.Add(this.lbWFX32);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Set Letterhead";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbWFX32;
        private System.Windows.Forms.CheckBox chkOfficeGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkOverwrite;
    }
}

