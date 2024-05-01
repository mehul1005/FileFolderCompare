using System.Drawing;
using System.Windows.Forms;

namespace FileFolderCompare
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
            this.textBoxFolder1 = new System.Windows.Forms.TextBox();
            this.textBoxFolder2 = new System.Windows.Forms.TextBox();
            this.buttonBrowse1 = new System.Windows.Forms.Button();
            this.buttonBrowse2 = new System.Windows.Forms.Button();
            this.buttonCompare = new System.Windows.Forms.Button();
            this.listBoxInformation = new System.Windows.Forms.ListBox();
            this.lblMissingCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxFolder1
            // 
            this.textBoxFolder1.Location = new System.Drawing.Point(126, 14);
            this.textBoxFolder1.Name = "textBoxFolder1";
            this.textBoxFolder1.Size = new System.Drawing.Size(401, 20);
            this.textBoxFolder1.TabIndex = 0;
            // 
            // textBoxFolder2
            // 
            this.textBoxFolder2.Location = new System.Drawing.Point(126, 49);
            this.textBoxFolder2.Name = "textBoxFolder2";
            this.textBoxFolder2.Size = new System.Drawing.Size(401, 20);
            this.textBoxFolder2.TabIndex = 1;
            // 
            // buttonBrowse1
            // 
            this.buttonBrowse1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBrowse1.Location = new System.Drawing.Point(533, 12);
            this.buttonBrowse1.Name = "buttonBrowse1";
            this.buttonBrowse1.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse1.TabIndex = 2;
            this.buttonBrowse1.Text = "Browse";
            this.buttonBrowse1.UseVisualStyleBackColor = true;
            this.buttonBrowse1.Click += new System.EventHandler(this.buttonBrowse1_Click);
            // 
            // buttonBrowse2
            // 
            this.buttonBrowse2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBrowse2.Location = new System.Drawing.Point(533, 47);
            this.buttonBrowse2.Name = "buttonBrowse2";
            this.buttonBrowse2.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse2.TabIndex = 3;
            this.buttonBrowse2.Text = "Browse";
            this.buttonBrowse2.UseVisualStyleBackColor = true;
            this.buttonBrowse2.Click += new System.EventHandler(this.buttonBrowse2_Click);
            // 
            // buttonCompare
            // 
            this.buttonCompare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCompare.Location = new System.Drawing.Point(533, 81);
            this.buttonCompare.Name = "buttonCompare";
            this.buttonCompare.Size = new System.Drawing.Size(75, 23);
            this.buttonCompare.TabIndex = 4;
            this.buttonCompare.Text = "Compare";
            this.buttonCompare.UseVisualStyleBackColor = true;
            this.buttonCompare.Click += new System.EventHandler(this.buttonCompare_Click);
            // 
            // listBoxInformation
            // 
            this.listBoxInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxInformation.FormattingEnabled = true;
            this.listBoxInformation.ItemHeight = 16;
            this.listBoxInformation.Location = new System.Drawing.Point(12, 120);
            this.listBoxInformation.Name = "listBoxInformation";
            this.listBoxInformation.Size = new System.Drawing.Size(596, 354);
            this.listBoxInformation.TabIndex = 5;
            // 
            // lblMissingCount
            // 
            this.lblMissingCount.AutoSize = true;
            this.lblMissingCount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblMissingCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMissingCount.ForeColor = System.Drawing.Color.Red;
            this.lblMissingCount.Location = new System.Drawing.Point(251, 86);
            this.lblMissingCount.Name = "lblMissingCount";
            this.lblMissingCount.Size = new System.Drawing.Size(78, 15);
            this.lblMissingCount.TabIndex = 6;
            this.lblMissingCount.Text = "Missing Files:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Original Folder";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(9, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Folder to Compare";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 485);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMissingCount);
            this.Controls.Add(this.listBoxInformation);
            this.Controls.Add(this.buttonCompare);
            this.Controls.Add(this.buttonBrowse2);
            this.Controls.Add(this.buttonBrowse1);
            this.Controls.Add(this.textBoxFolder2);
            this.Controls.Add(this.textBoxFolder1);
            this.Name = "Form1";
            this.Text = "Folder File Compare";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFolder1;
        private System.Windows.Forms.TextBox textBoxFolder2;
        private System.Windows.Forms.Button buttonBrowse1;
        private System.Windows.Forms.Button buttonBrowse2;
        private System.Windows.Forms.Button buttonCompare;
        private System.Windows.Forms.ListBox listBoxInformation;
        private System.Windows.Forms.Label lblMissingCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

