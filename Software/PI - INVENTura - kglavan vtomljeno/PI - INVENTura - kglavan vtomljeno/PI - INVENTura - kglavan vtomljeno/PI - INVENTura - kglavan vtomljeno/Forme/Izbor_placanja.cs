using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PI___INVENTura___kglavan_vtomljeno
{
    public partial class Izbor_placanja : Form
    {

        public Izbor_placanja()
        {
            InitializeComponent();
            Izbor_placanja_panel2.BackColor = Color.FromArgb(255, 215, 114);
            Izbor_placanja_placanje_pouzece.BackColor = Color.FromArgb(255, 215, 114);
            Izbor_placanja_placanje_kartica.BackColor = Color.FromArgb(255, 215, 114);
            Kosarica kosarica = Kosarica.instance;
            
            Izbor_placanja_ukupnoplatiti.Text = kosarica.Kosarica_Ukunpo_za_platiti.Text;
        }

        private void Izbor_placanja_pomoc_MouseHover(object sender, EventArgs e)
        {
            Izbor_placanja_toolTip1.IsBalloon = false;
            Izbor_placanja_toolTip1.ToolTipIcon = ToolTipIcon.Info;
            Izbor_placanja_toolTip1.Show("Ovdje Vam je omogućen izbor plaćanja: Kartično plaćanje!\nUkoliko ste sigurno da želite nastaviti pritisnite na 'Placanje karticom'.", Izbor_placanja_pomoc);
            Izbor_placanja_toolTip1.InitialDelay = 200;
            Izbor_placanja_toolTip1.ReshowDelay = 100;
        }

        private void Izbor_placanja_nazad_Click(object sender, EventArgs e)
        {
            this.Hide();
            Kosarica kosarica = new Kosarica();
            kosarica.Show();
        }

        private void Izbor_placanja_placanje_kartica_Click(object sender, EventArgs e)
        {
            
            Placanje_kartica placanje_kartica = new Placanje_kartica();
            placanje_kartica.ShowDialog();

        }

        private void Izbor_placanja_Load(object sender, EventArgs e)
        {
            Izbor_placanja_panel1.Visible = false;
        }
    }
}

