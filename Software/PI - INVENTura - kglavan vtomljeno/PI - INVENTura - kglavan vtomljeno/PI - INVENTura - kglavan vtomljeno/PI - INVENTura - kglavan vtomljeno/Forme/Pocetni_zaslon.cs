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
    public partial class Pocetni_zaslon : Form
    {
        public Pocetni_zaslon()
        {
            InitializeComponent();
            Pocetni_zaslon_kreni.BackColor = Color.FromArgb(255, 215, 114) ;
            Pocetni_zaslon_naslov.BackColor = Color.Transparent;
        }

        private void Pocetni_zaslon_Load(object sender, EventArgs e)
        {

        }
        private void Pocetni_zaslon_help_MouseHover(object sender, EventArgs e)
        {
            Pocetni_zaslon_toolTip1.IsBalloon = true;
            Pocetni_zaslon_toolTip1.ToolTipIcon = ToolTipIcon.Info;
            Pocetni_zaslon_toolTip1.Show("Aplikacija \"Chic Boutique\" kupcima omogućuje pregled odjevnih artikala i narudžbu \ndok administratori imaju stalan uvid u stvarno stanje zaliha istih.", Pocetni_zaslon_help);
            Pocetni_zaslon_toolTip1.InitialDelay = 200;
            Pocetni_zaslon_toolTip1.ReshowDelay = 100;
        }

        private void Pocetni_zaslon_kreni_Click(object sender, EventArgs e)
        {
            this.Hide();
            Prijava_ili_registracija prijava_ili_registracija = new Prijava_ili_registracija();
            prijava_ili_registracija.Show();
        }
    }
}
