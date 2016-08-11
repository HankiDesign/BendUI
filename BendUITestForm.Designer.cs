namespace BendUI
{
	partial class BendUITestForm
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
			this.bendUIButton1 = new BendUI.Controls.BendUIButton();
			this.SuspendLayout();
			// 
			// bendUIButton1
			// 
			this.bendUIButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bendUIButton1.BackColor = System.Drawing.Color.DarkRed;
			this.bendUIButton1.Location = new System.Drawing.Point(12, 12);
			this.bendUIButton1.Margin = new System.Windows.Forms.Padding(0);
			this.bendUIButton1.Name = "bendUIButton1";
			this.bendUIButton1.Size = new System.Drawing.Size(842, 509);
			this.bendUIButton1.TabIndex = 0;
			this.bendUIButton1.Text = "bendUIButton1";
			// 
			// BendUITestForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(863, 530);
			this.Controls.Add(this.bendUIButton1);
			this.Name = "BendUITestForm";
			this.ShowIcon = false;
			this.Text = "BendUI Test";
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.BendUIButton bendUIButton1;
	}
}

