
namespace PI___INVENTura___kglavan_vtomljeno
{
    partial class Pocetni_zaslon
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pocetni_zaslon));
            this.Pocetni_zaslon_kreni = new System.Windows.Forms.Button();
            this.Pocetni_zaslon_naslov = new System.Windows.Forms.Label();
            this.Pocetni_zaslon_cvijet1 = new System.Windows.Forms.PictureBox();
            this.Pocetni_zaslon_help = new System.Windows.Forms.PictureBox();
            this.Pocetni_zaslon_toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Pocetni_zaslon_cvijet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pocetni_zaslon_help)).BeginInit();
            this.SuspendLayout();
            // 
            // Pocetni_zaslon_kreni
            // 
            this.Pocetni_zaslon_kreni.BackColor = System.Drawing.Color.Transparent;
            this.Pocetni_zaslon_kreni.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pocetni_zaslon_kreni.Location = new System.Drawing.Point(336, 256);
            this.Pocetni_zaslon_kreni.Name = "Pocetni_zaslon_kreni";
            this.Pocetni_zaslon_kreni.Size = new System.Drawing.Size(152, 48);
            this.Pocetni_zaslon_kreni.TabIndex = 0;
            this.Pocetni_zaslon_kreni.Text = "KRENI U TRGOVINU";
            this.Pocetni_zaslon_kreni.UseVisualStyleBackColor = false;
            this.Pocetni_zaslon_kreni.Click += new System.EventHandler(this.Pocetni_zaslon_kreni_Click);
            // 
            // Pocetni_zaslon_naslov
            // 
            this.Pocetni_zaslon_naslov.AutoSize = true;
            this.Pocetni_zaslon_naslov.BackColor = System.Drawing.Color.Transparent;
            this.Pocetni_zaslon_naslov.Font = new System.Drawing.Font("Matura MT Script Capitals", 33F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pocetni_zaslon_naslov.Location = new System.Drawing.Point(248, 176);
            this.Pocetni_zaslon_naslov.Name = "Pocetni_zaslon_naslov";
            this.Pocetni_zaslon_naslov.Size = new System.Drawing.Size(316, 59);
            this.Pocetni_zaslon_naslov.TabIndex = 1;
            this.Pocetni_zaslon_naslov.Text = "Chic Boutique\r\n";
            // 
            // Pocetni_zaslon_cvijet1
            // 
            this.Pocetni_zaslon_cvijet1.Image = ((System.Drawing.Image)(resources.GetObject("Pocetni_zaslon_cvijet1.Image")));
            this.Pocetni_zaslon_cvijet1.Location = new System.Drawing.Point(176, 0);
            this.Pocetni_zaslon_cvijet1.Name = "Pocetni_zaslon_cvijet1";
            this.Pocetni_zaslon_cvijet1.Size = new System.Drawing.Size(480, 456);
            this.Pocetni_zaslon_cvijet1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pocetni_zaslon_cvijet1.TabIndex = 2;
            this.Pocetni_zaslon_cvijet1.TabStop = false;
            // 
            // Pocetni_zaslon_help
            // 
            this.Pocetni_zaslon_help.Image = ((System.Drawing.Image)(resources.GetObject("Pocetni_zaslon_help.Image")));
            this.Pocetni_zaslon_help.Location = new System.Drawing.Point(760, 16);
            this.Pocetni_zaslon_help.Name = "Pocetni_zaslon_help";
            this.Pocetni_zaslon_help.Size = new System.Drawing.Size(32, 32);
            this.Pocetni_zaslon_help.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pocetni_zaslon_help.TabIndex = 3;
            this.Pocetni_zaslon_help.TabStop = false;
            this.Pocetni_zaslon_help.MouseHover += new System.EventHandler(this.Pocetni_zaslon_help_MouseHover);
            // 
            // Pocetni_zaslon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Pocetni_zaslon_help);
            this.Controls.Add(this.Pocetni_zaslon_naslov);
            this.Controls.Add(this.Pocetni_zaslon_kreni);
            this.Controls.Add(this.Pocetni_zaslon_cvijet1);
            this.Name = "Pocetni_zaslon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pocetni_zaslon";
            this.Load += new System.EventHandler(this.Pocetni_zaslon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Pocetni_zaslon_cvijet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pocetni_zaslon_help)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Pocetni_zaslon_kreni;
        private System.Windows.Forms.Label Pocetni_zaslon_naslov;
        private System.Windows.Forms.PictureBox Pocetni_zaslon_cvijet1;
        private System.Windows.Forms.PictureBox Pocetni_zaslon_help;
        private System.Windows.Forms.ToolTip Pocetni_zaslon_toolTip1;
    }
}

