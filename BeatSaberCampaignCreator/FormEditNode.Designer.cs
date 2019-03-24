namespace BeatSaberCampaignCreator
{
    partial class FormEditNode
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
            this.letterPortion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numberPortion = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nodeScale = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numberPortion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nodeScale)).BeginInit();
            this.SuspendLayout();
            // 
            // letterPortion
            // 
            this.letterPortion.Location = new System.Drawing.Point(162, 59);
            this.letterPortion.Name = "letterPortion";
            this.letterPortion.Size = new System.Drawing.Size(100, 20);
            this.letterPortion.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Superscript";
            // 
            // numberPortion
            // 
            this.numberPortion.Location = new System.Drawing.Point(162, 33);
            this.numberPortion.Name = "numberPortion";
            this.numberPortion.Size = new System.Drawing.Size(120, 20);
            this.numberPortion.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Node Scale";
            // 
            // nodeScale
            // 
            this.nodeScale.DecimalPlaces = 1;
            this.nodeScale.Location = new System.Drawing.Point(162, 85);
            this.nodeScale.Name = "nodeScale";
            this.nodeScale.Size = new System.Drawing.Size(120, 20);
            this.nodeScale.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(162, 126);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormEditNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 199);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nodeScale);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numberPortion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.letterPortion);
            this.Name = "FormEditNode";
            this.Text = "FormEditNode";
            this.Load += new System.EventHandler(this.FormEditNode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numberPortion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nodeScale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox letterPortion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numberPortion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nodeScale;
        private System.Windows.Forms.Button button1;
    }
}