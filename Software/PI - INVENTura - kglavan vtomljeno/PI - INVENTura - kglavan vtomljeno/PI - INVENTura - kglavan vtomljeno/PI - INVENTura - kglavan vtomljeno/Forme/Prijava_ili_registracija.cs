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
    public partial class Prijava_ili_registracija : Form
    {

        public Prijava_ili_registracija()
        {
            InitializeComponent();
            Prijava_ili_registracija_pri.BackColor = Color.FromArgb(255, 215, 114);
            Prijava_ili_registracija_op2.BackColor = Color.Transparent;
            Prijava_ili_registracija_reg.BackColor = Color.FromArgb(255, 215, 114);
            Prijava_ili_registracija_op2.BackColor = Color.Transparent;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Prijava_ili_registracija_help_MouseHover(object sender, EventArgs e)
        {
            Prijava_ili_registracija_toolTip1.IsBalloon = true;
            Prijava_ili_registracija_toolTip1.ToolTipIcon = ToolTipIcon.Info;
            Prijava_ili_registracija_toolTip1.Show("Ovdje Vam je omogućen odabir prijave ili kreiranja novog korisničkog računa. \nAko posjedujete račun nastavite s prijavom, u suprotnom izradite Vaš novi osobni račun.", Prijava_ili_registracija_help);
            Prijava_ili_registracija_toolTip1.InitialDelay = 250;
            Prijava_ili_registracija_toolTip1.ReshowDelay = 100;
        }

        private void Prijava_ili_registracija_pri_Click(object sender, EventArgs e)
        {
            this.Hide();
            Prijava prijava = new Prijava();
            prijava.Show();
        }

        private void Prijava_ili_registracija_reg_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registracija registracija = new Registracija();
            registracija.Show();
        }
    }
}
