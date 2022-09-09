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

namespace PI___INVENTura___kglavan_vtomljeno
{
    public partial class Prijava : Form
    {
        public static string welcomeuser = " ";
        public Prijava()
        {
            InitializeComponent();
            Prijava_uName.Focus();
            Prijava_privbutton.BackColor = Color.FromArgb(255, 215, 114);
        }

        private void Prijava_Load(object sender, EventArgs e)
        {
            Prijava_uName.Focus();
        }

        private void InitializeMyControl()
        {
            // Set to no text.
            Prijava_uPassword.Text = "";
            // The password character is an asterisk.
            Prijava_uPassword.PasswordChar = '*';
            // The control will allow no more than 14 characters.
            Prijava_uPassword.MaxLength = 14;
        }

        private void Prijava_help_MouseHover(object sender, EventArgs e)
        {
            Prijava_toolTip1.IsBalloon = true;
            Prijava_toolTip1.ToolTipIcon = ToolTipIcon.Info;
            Prijava_toolTip1.Show("Ovdje Vam je omogućena prijava koju vršite unosom svog korisničkog imena te lozinke.\nPrijavu možete izvršiti jedino ako posjedujete korisniči račun kojeg ste prethodno izradili\nili ukoliko posjedujete administratorske ovlasti.", Prijava_help);
            Prijava_toolTip1.InitialDelay = 200;
            Prijava_toolTip1.ReshowDelay = 100;
        }

        private void Prijava_zab_lozinka_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Zaboravljena_lozinka zaboravljena_lozinka = new Zaboravljena_lozinka();
            zaboravljena_lozinka.Show();
        }

        private void Prijava_nazad_Click(object sender, EventArgs e)
        {
            this.Hide();
            Prijava_ili_registracija prijava_ili_registracija = new Prijava_ili_registracija();
            prijava_ili_registracija.Show();
        }

        private void Prijava_privbutton_Click(object sender, EventArgs e)
        {
            Provjera_prijave();
        }

        private void Provjera_prijave()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "select * from [dbo].[Registracija_korisnika] where Uname = @Uname and Upassword = @Upassword";
            sqlconn.Open();
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlcomm.Parameters.AddWithValue("@Uname", Prijava_uName.Text);
            sqlcomm.Parameters.AddWithValue("@Upassword", Prijava_uPassword.Text);
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sqlcomm.ExecuteNonQuery();
            if (dt.Rows.Count > 0)
            {
                welcomeuser = Prijava_uName.Text;
                nakonprijave nknp = new nakonprijave();
                nknp.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Neispravni parametri!");
            }
        }

        public void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            nakonprijave nakonprijave = new nakonprijave();
            nakonprijave.Show();

        }
    }
}
