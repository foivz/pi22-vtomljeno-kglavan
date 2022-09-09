using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace PI___INVENTura___kglavan_vtomljeno
{
    public partial class nakonprijave : Form
    {

        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        private string gost = "Guest";
        public nakonprijave()
        {
            InitializeComponent();
            Home_majice.BackColor = Color.FromArgb(255, 215, 114);
            Home_hlace.BackColor = Color.FromArgb(255, 215, 114);
            Home_haljine.BackColor = Color.FromArgb(255, 215, 114);
            Home_obuca.BackColor = Color.FromArgb(255, 215, 114);
            cn = new MySqlConnection();
            cn.ConnectionString = "server=localhost; user id=root; password=; database=probaslika";
        }

        private void nakonprijave_Load(object sender, EventArgs e)
        {
            if(Prijava.welcomeuser == " ")
            {
                Dobrodošlica.Text = "Prijavljeni ste kao Guest";
                
            }
            else
            {
                Dobrodošlica.Text = "Prijavljeni ste kao: " + Prijava.welcomeuser;
            }
            
            Home_panel.Height = 44;
        }

        private void Home_pomoc_MouseHover(object sender, EventArgs e)
        {
            Home_tooltip.IsBalloon = true;
            Home_tooltip.ToolTipIcon = ToolTipIcon.Info;
            Home_tooltip.Show("Dobrodošli!\nPritiskom na ikonu kuće doći ćete upravo ovdje, izbor odjeće dobivate pritiskom na ikonu haljine, a svoj profil na ikonu čovjeka! Odjava Vam je omogućena pritiskom na vrata!", Home_pomoc);
            Home_tooltip.InitialDelay = 200;
            Home_tooltip.ReshowDelay = 100;
        }

        private void Home_home_Click(object sender, EventArgs e)
        {
            this.Hide();
            nakonprijave nakonprijave = new nakonprijave();
            nakonprijave.Show();
        }

        private void Home_artikli_Click(object sender, EventArgs e)
        {
            if (Home_panel.Height == 208)
            {
                Home_panel.Height = 44;
            }
            else
            {
                Home_panel.Height = 208;
            }
        }

        private void Home_majice_Click(object sender, EventArgs e)
        {
            this.Hide();
            Majice majice = new Majice();
            majice.Show();
        }

        private void Home_hlace_Click(object sender, EventArgs e)
        {
            this.Hide();
            Hlace hlace = new Hlace();
            hlace.Show();
        }

        private void Home_haljine_Click(object sender, EventArgs e)
        {
            this.Hide();
            Haljine haljine = new Haljine();
            haljine.Show();
        }

        private void Home_obuca_Click(object sender, EventArgs e)
        {
            this.Hide();
            Obuca obuca = new Obuca();
            obuca.Show();
        }

        private void Home_kosarica_Click(object sender, EventArgs e)
        {
            this.Hide();
            Kosarica kosarica = new Kosarica();
            kosarica.Show();
        }

        private void Home_odjava_Click(object sender, EventArgs e)
        {
            Kosarica kosarica = new Kosarica();
            kosarica.Kosarica_dataGridView1.ClearSelection();
            this.Hide();
            Prijava prijava = new Prijava();
            prijava.Show();
            
        }
    }
}
