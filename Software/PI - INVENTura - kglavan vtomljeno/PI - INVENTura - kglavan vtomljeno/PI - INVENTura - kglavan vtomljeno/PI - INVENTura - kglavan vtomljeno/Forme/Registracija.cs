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
    public partial class Registracija : Form
    {
        public Registracija()
        {
            InitializeComponent();
            Registracija_regbutton.BackColor = Color.FromArgb(255, 215, 114);
        }

        private void Registracija_Load(object sender, EventArgs e)
        {
            label8.Visible = false;
        }

        private void Registracija_pomoc_MouseHover(object sender, EventArgs e)
        {
            Registracija_pomoc_toolTip1.IsBalloon = true;
            Registracija_pomoc_toolTip1.ToolTipIcon = ToolTipIcon.Info;
            Registracija_pomoc_toolTip1.Show("Ovdje Vam je omogućena registracija koju vršite unosom željenog maila, korisničkog imena te lozinke.\nRegistraijca je preduvjet za uspješnu Prijavu.", Registracija_pomoc);
            Registracija_pomoc_toolTip1.InitialDelay = 200;
            Registracija_pomoc_toolTip1.ReshowDelay = 100;
        }

        void Clear()
        {
            txtUname.Text = txtUpassword.Text = txtUrepassword.Text = Registracija_uEmail.Text = "";
        }

        private void Registracija_uEmail_Leave(object sender, EventArgs e)
        {
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(Registracija_uEmail.Text, pattern))
            {
                errorProvider1.Clear();
            }
            else
            {
                errorProvider1.SetError(this.Registracija_uEmail, "Ovo nije ispravna mail adresa!");
                return;
            }
        }

        private void Registracija_nazad_Click(object sender, EventArgs e)
        {
            this.Hide();
            Prijava_ili_registracija prijava_ili_registracija = new Prijava_ili_registracija();
            prijava_ili_registracija.Show();
        }

        private void Registracija_korisnika()
        {
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (txtUname.Text == "" || Registracija_uEmail.Text == "")
            {
                MessageBox.Show("Molimo ispunite prazna polja!");
                Registracija_uEmail.Focus();
                txtUname.Focus();
                return;
            }
            else if (txtUpassword.Text != txtUrepassword.Text)
            {
                MessageBox.Show("Lozinke se ne podudaraju!");
                txtUrepassword.Focus();
                return;
            }
            else if (!Regex.IsMatch(Registracija_uEmail.Text, pattern))
            {
                errorProvider1.SetError(this.Registracija_uEmail, "Ovo nije ispravna mail adresa!");
                Registracija_uEmail.Focus();
                MessageBox.Show("Unesite ispravnu mail adresu!");
                return;
            }
            else
            {
                string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);
                string sqlquery = "insert into [dbo].[Registracija_korisnika] values(@Uname, @Upassword, @Urepassword, @Uemail)";
                sqlconn.Open();
                SqlCommand CommandProvjeraDostupnostiImena = new SqlCommand("select Uname from Registracija_korisnika where Uname = '" + txtUname.Text + "'", sqlconn);
                string proba = (string)CommandProvjeraDostupnostiImena.ExecuteScalar();
                if (proba == txtUname.Text)
                {
                    MessageBox.Show("Vec postoji");
                }
                else
                {
                    SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                    sqlcomm.Parameters.AddWithValue("@Uname", txtUname.Text);
                    sqlcomm.Parameters.AddWithValue("@Upassword", txtUpassword.Text);
                    sqlcomm.Parameters.AddWithValue("@Urepassword", txtUrepassword.Text);
                    sqlcomm.Parameters.AddWithValue("@Uemail", Registracija_uEmail.Text);
                    sqlcomm.ExecuteNonQuery();
                    label7.Text = "Korisnik " + txtUname.Text + " je uspješno stvoren";
                    label8.Visible = true;
                    label8.Text = "Korisnički podaci uspješno su poslani na: " + Registracija_uEmail.Text;
                    var fromAddr = new MailAddress("chic.boutiquePI@gmail.com", "Chic Boutique");
                    var toAddr = new MailAddress(Registracija_uEmail.Text, "");
                    const string fromPass = "loiwwrfgzuhetmmk";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new NetworkCredential(fromAddr.Address, fromPass),
                        Timeout = 20000
                    };

                    MailMessage mm = new MailMessage();
                    mm.IsBodyHtml = true;
                    mm.Subject = "Chic Boutique Korisnicki podaci za prijavu";
                    mm.Body = txtUname.Text + "\n" + txtUrepassword.Text;
                    mm.From = new MailAddress(fromAddr.ToString());
                    mm.To.Add(toAddr);
                    {
                        smtp.Send(mm);
                    }
                    sqlconn.Close();
                    Clear();
                }
            }
        }

        private void Registracija_regbutton_Click(object sender, EventArgs e)
        {
            Registracija_korisnika();
        }

    }
}

//podaci za gmail prijavu:
//mail: chic.boutiquePI@gmail.com
//password chicboutiquePI123
//user ChicBoutique PI
//nazalost google je moneygrabber i od 30.5. bez admin accounta ne dopusta less secure apps gmail access
//zaporka aplikacije za uredaj loiwwrfgzuhetmmk