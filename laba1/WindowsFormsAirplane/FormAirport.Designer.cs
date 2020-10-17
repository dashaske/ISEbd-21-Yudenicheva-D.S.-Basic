namespace WindowsFormsAirplane
{
    partial class FormAirport
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
            this.buttonSetBomber = new System.Windows.Forms.Button();
            this.buttonSetWarPlane = new System.Windows.Forms.Button();
            this.buttonTakeBomber = new System.Windows.Forms.Button();
            this.groupBoxTake = new System.Windows.Forms.GroupBox();
            this.maskedTextBoxPlace = new System.Windows.Forms.MaskedTextBox();
            this.labelPlace = new System.Windows.Forms.Label();
            this.pictureBoxAirport = new System.Windows.Forms.PictureBox();
            this.groupBoxTake.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAirport)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSetBomber
            // 
            this.buttonSetBomber.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSetBomber.Location = new System.Drawing.Point(767, 12);
            this.buttonSetBomber.Name = "buttonSetBomber";
            this.buttonSetBomber.Size = new System.Drawing.Size(209, 115);
            this.buttonSetBomber.TabIndex = 1;
            this.buttonSetBomber.Text = "Посадить бомбардировщика";
            this.buttonSetBomber.UseVisualStyleBackColor = false;
            this.buttonSetBomber.Click += new System.EventHandler(this.buttonSetBomber_Click);
            // 
            // buttonSetWarPlane
            // 
            this.buttonSetWarPlane.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSetWarPlane.Location = new System.Drawing.Point(767, 133);
            this.buttonSetWarPlane.Name = "buttonSetWarPlane";
            this.buttonSetWarPlane.Size = new System.Drawing.Size(209, 92);
            this.buttonSetWarPlane.TabIndex = 2;
            this.buttonSetWarPlane.Text = "Посадить военный самолет";
            this.buttonSetWarPlane.UseVisualStyleBackColor = false;
            this.buttonSetWarPlane.Click += new System.EventHandler(this.buttonSetWarPlane_Click);
            // 
            // buttonTakeBomber
            // 
            this.buttonTakeBomber.Location = new System.Drawing.Point(88, 92);
            this.buttonTakeBomber.Name = "buttonTakeBomber";
            this.buttonTakeBomber.Size = new System.Drawing.Size(120, 23);
            this.buttonTakeBomber.TabIndex = 2;
            this.buttonTakeBomber.Text = "Забрать";
            this.buttonTakeBomber.UseVisualStyleBackColor = false;
            this.buttonTakeBomber.Click += new System.EventHandler(this.buttonTakeBomber_Click);
            // 
            // groupBoxTake
            // 
            this.groupBoxTake.Controls.Add(this.maskedTextBoxPlace);
            this.groupBoxTake.Controls.Add(this.labelPlace);
            this.groupBoxTake.Controls.Add(this.buttonTakeBomber);
            this.groupBoxTake.Location = new System.Drawing.Point(759, 231);
            this.groupBoxTake.Name = "groupBoxTake";
            this.groupBoxTake.Size = new System.Drawing.Size(217, 137);
            this.groupBoxTake.TabIndex = 3;
            this.groupBoxTake.TabStop = false;
            this.groupBoxTake.Text = "Забрать";
            // 
            // maskedTextBoxPlace
            // 
            this.maskedTextBoxPlace.Location = new System.Drawing.Point(81, 48);
            this.maskedTextBoxPlace.Name = "maskedTextBoxPlace";
            this.maskedTextBoxPlace.Size = new System.Drawing.Size(120, 20);
            this.maskedTextBoxPlace.TabIndex = 0;
            // 
            // labelPlace
            // 
            this.labelPlace.AutoSize = true;
            this.labelPlace.Location = new System.Drawing.Point(36, 51);
            this.labelPlace.Name = "labelPlace";
            this.labelPlace.Size = new System.Drawing.Size(39, 13);
            this.labelPlace.TabIndex = 1;
            this.labelPlace.Text = "Место";
            // 
            // pictureBoxAirport
            // 
            this.pictureBoxAirport.Location = new System.Drawing.Point(0, -1);
            this.pictureBoxAirport.Name = "pictureBoxAirport";
            this.pictureBoxAirport.Size = new System.Drawing.Size(761, 543);
            this.pictureBoxAirport.TabIndex = 4;
            this.pictureBoxAirport.TabStop = false;
            // 
            // FormAirport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 543);
            this.Controls.Add(this.pictureBoxAirport);
            this.Controls.Add(this.groupBoxTake);
            this.Controls.Add(this.buttonSetWarPlane);
            this.Controls.Add(this.buttonSetBomber);
            this.Name = "FormAirport";
            this.Text = "Аэропорт";
            this.groupBoxTake.ResumeLayout(false);
            this.groupBoxTake.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAirport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSetBomber;
        private System.Windows.Forms.Button buttonSetWarPlane;
        private System.Windows.Forms.Button buttonTakeBomber;
        private System.Windows.Forms.GroupBox groupBoxTake;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxPlace;
        private System.Windows.Forms.Label labelPlace;
        private System.Windows.Forms.PictureBox pictureBoxAirport;
    }
}