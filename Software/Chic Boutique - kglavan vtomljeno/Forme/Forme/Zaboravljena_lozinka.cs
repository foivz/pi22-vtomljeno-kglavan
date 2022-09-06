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

namespace PI___INVENTura___kglavan_vtomljeno
{
    public partial class Zaboravljena_lozinka : Form
    {

        public Zaboravljena_lozinka()
        {
            InitializeComponent();
            Zaboravljena_lozinka_send.BackColor = Color.FromArgb(255, 215, 114);
        }

        private void Zaboravljena_lozinka_Load(object sender, EventArgs e)
        {

        }

        private void Zaboravljena_lozinka_nazad_Click(object sender, EventArgs e)
        {
            this.Hide();
            Prijava prijava = new Prijava();
            prijava.Show();
        }

        private void Zaboravljena_lozinka_help_MouseHover(object sender, EventArgs e)
        {
            Zab_lozinka.IsBalloon = true;
            Zab_lozinka.ToolTipIcon = ToolTipIcon.Info;
            Zab_lozinka.Show("Ovdje Vam je omogućen povrat zaboravljene lozinke.\nUpišite Vašu ispravnu mail adresu te korisničko ime koje ste koristili za prijavu, a na isti mail će Vam dospjeti zaboravljena lozinka za navedenog korisnika!", Zaboravljena_lozinka_help);
            Zab_lozinka.InitialDelay = 200;
            Zab_lozinka.ReshowDelay = 100;
        }

        private void Zaboravljena_lozinka_send_Click(object sender, EventArgs e)
        {
            Zaboravljena_lozinka_provjera();
        }

        private void Zaboravljena_lozinka_provjera()
        {
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Zaboravljena_lozinka_uNamezab.Text == "" || Zaboravljena_lozinka_uEmailzab.Text == "")
            {
                MessageBox.Show("Molimo ispunite prazna polja!");
                Zaboravljena_lozinka_uEmailzab.Focus();
                return;
            }
            else if (!Regex.IsMatch(Zaboravljena_lozinka_uEmailzab.Text, pattern))
            {
                Zaboravljena_lozinka_errorProvider1.SetError(this.Zaboravljena_lozinka_uEmailzab, "Ovo nije ispravna mail adresa!");
                Zaboravljena_lozinka_uEmailzab.Focus();
                MessageBox.Show("Unesite ispravnu mail adresu!");
                return;
            }
            else
            {
                string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);
                sqlconn.Open();
                SqlCommand cmd = new SqlCommand("select Upassword from Registracija_korisnika where Uname = '" + Zaboravljena_lozinka_uNamezab.Text + "'", sqlconn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string ponovnalozinka = reader[0].ToString();
                    var fromAddr = new MailAddress("chic.boutiquePI@gmail.com", "Izgubljeni podaci za prijavu");
                    var toAddr = new MailAddress(Zaboravljena_lozinka_uEmailzab.Text, "");
                    const string fromPass = "loiwwrfgzuhetmmk";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new NetworkCredential(fromAddr.Address, fromPass),
                        Timeout = 100000
                    };

                    MailMessage mm = new MailMessage();
                    mm.IsBodyHtml = true;
                    mm.Subject = "Podaci za prijavu";
                    mm.Body = "Vaša lozinka za " + Zaboravljena_lozinka_uNamezab.Text + ": " + ponovnalozinka;
                    mm.From = new MailAddress(fromAddr.ToString());
                    mm.To.Add(toAddr);
                    {
                        smtp.Send(mm);
                    }
                    label7.Text = "Podaci za: " + Zaboravljena_lozinka_uNamezab.Text + " uspješno su poslani na " + Zaboravljena_lozinka_uEmailzab.Text;
                }
                else
                {
                    MessageBox.Show("Neispravni parametri!");
                }
                sqlconn.Close();
            }
        }
    }
}
