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
    public partial class Hlace : Form
    {

        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        private PictureBox pic;
        private Label Hlace_cijena;
        private Label Hlace_naziv;
        public static string taghlacenext;

        public Hlace()
        {
            InitializeComponent();
            Hlace_panel2.BackColor = Color.FromArgb(255, 215, 114);
            Hlace_kratki_nogav.Text = "Kratki nogav";
            Hlace_kratki_nogav.Width = 150;
            Hlace_kratki_nogav.Height = 25;
            Hlace_kratki_nogav.BackgroundImageLayout = ImageLayout.Stretch;
            Hlace_dugi_nogav.Text = "Dugi nogav";
            Hlace_dugi_nogav.Width = 150;
            Hlace_dugi_nogav.Height = 25;
            Hlace_dugi_nogav.BackgroundImageLayout = ImageLayout.Stretch;
            Hlace_artikli_majice.BackColor = Color.FromArgb(255, 215, 114);
            Hlace_artikli_hlace.BackColor = Color.FromArgb(255, 215, 114);
            Hlace_artikli_haljine.BackColor = Color.FromArgb(255, 215, 114);
            Hlace_artikli_obuca.BackColor = Color.FromArgb(255, 215, 114);
            cn = new MySqlConnection();
            cn.ConnectionString = "server=localhost;user id=root;password=;database=probaslika2";
        }

        private void hlace_Load(object sender, EventArgs e)
        {
            GetData();
        }
        private void GetData()
        {
            Hlace_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_id, Artikli_slika, Artikli_naziv, Artikli_cijena, Artikli_tip from artikli where Artikli_oznaka = 'Hlace' and Artikli_naziv like '%" + Hlace_pretraga_input.Text + "%' order by Artikli_naziv", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                long len = dr.GetBytes(1, 0, null, 0, 0);
                byte[] array = new byte[System.Convert.ToInt32(len) + 1];
                dr.GetBytes(1, 0, array, 0, System.Convert.ToInt32(len));
                pic = new PictureBox();
                pic.Width = 200;
                pic.Height = 315;
                pic.BackgroundImageLayout = ImageLayout.Stretch;
                pic.BorderStyle = BorderStyle.FixedSingle;
                pic.Tag = dr["Artikli_id"].ToString();
                Hlace_cijena = new Label();
                Hlace_cijena.Text = dr["Artikli_cijena"].ToString();
                Hlace_cijena.BackColor = Color.FromArgb(255, 121, 121);
                Hlace_cijena.TextAlign = ContentAlignment.MiddleCenter;
                Hlace_cijena.Width = 50;
                Hlace_cijena.Dock = DockStyle.Bottom;
                Hlace_cijena.Tag = dr["Artikli_id"].ToString();
                Hlace_naziv = new Label();
                Hlace_naziv.Text = dr["Artikli_naziv"].ToString();
                Hlace_naziv.BackColor = Color.FromArgb(255, 121, 121);
                Hlace_naziv.TextAlign = ContentAlignment.MiddleLeft;
                Hlace_naziv.Dock = DockStyle.Top;
                Hlace_naziv.Tag = dr["Artikli_naziv"].ToString();
                MemoryStream ms = new MemoryStream(array);
                Bitmap bitmap = new Bitmap(ms);
                pic.BackgroundImage = bitmap;
                pic.Controls.Add(Hlace_cijena);
                pic.Controls.Add(Hlace_naziv);
                Hlace_flowLayoutPanel1.Controls.Add(pic);
                pic.Click += new EventHandler(OnClick);
            }
            dr.Close();
            cn.Close();
        }

        private void OnClick(object sender, EventArgs e)
        {
            taghlacenext = ((PictureBox)sender).Tag.ToString();
            this.Hide();
            Pregled_hlace pregled_hlace = new Pregled_hlace();
            pregled_hlace.Show();
        }

        private void Hlace_home_Click(object sender, EventArgs e)
        {
            this.Hide();
            nakonprijave nakonprijave = new nakonprijave();
            nakonprijave.Show();
        }

        private void Hlace_artikli_Click(object sender, EventArgs e)
        {
            if (Hlace_panel1.Height == 208)
            {
                Hlace_panel1.Height = 44;
            }
            else
            {
                Hlace_panel1.Height = 208;
            }
        }

        private void Hlace_artikli_majice_Click(object sender, EventArgs e)
        {
            this.Hide();
            Majice majice = new Majice();
            majice.Show();
        }

        private void Hlace_artikli_hlace_Click(object sender, EventArgs e)
        {
            this.Hide();
            Hlace hlace = new Hlace();
            hlace.Show();
        }

        private void Hlace_artikli_haljine_Click(object sender, EventArgs e)
        {
            this.Hide();
            Haljine haljine = new Haljine();
            haljine.Show();
        }

        private void Hlace_artikli_obuca_Click(object sender, EventArgs e)
        {
            this.Hide();
            Obuca obuca = new Obuca();
            obuca.Show();
        }

        private void Hlace_kosarica_Click(object sender, EventArgs e)
        {
            this.Hide();
            Kosarica kosarica = new Kosarica();
            kosarica.Show();
        }

        private void Hlace_odjava_Click(object sender, EventArgs e)
        {
            this.Hide();
            Prijava prijava = new Prijava();
            prijava.Show();
        }

        private void Hlace_pomoc_MouseHover(object sender, EventArgs e)
        {
            Hlace_toolTip1.IsBalloon = false;
            Hlace_toolTip1.ToolTipIcon = ToolTipIcon.Info;
            Hlace_toolTip1.Show("Ovdje Vam je omogućen pregled odjevnih artikala Hlače!\nIste artikle moguće je pretražiti po nazivu i sortirati prema cijeni.\nKlikom na proizvod odlazite na formu za izbor veličina i dodavanja u košaricu!", Hlace_pomoc);
            Hlace_toolTip1.InitialDelay = 200;
            Hlace_toolTip1.ReshowDelay = 100;
        }

        private void Hlace_pregledcijena_Click(object sender, EventArgs e)
        {
            if (Hlace_panel3.Width == 200)
            {
                Hlace_panel3.Width = 75;
            }
            else
            {
                Hlace_panel3.Width = 200;
            }
        }

        private void Hlace_cijena_uzlazno_Click(object sender, EventArgs e)
        {
            Cijena_uzlazno_sort();
        }

        private void Cijena_uzlazno_sort()
        {
            Hlace_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_oznaka = 'Hlace' order by Artikli_cijena asc", cn);
            dr = cm.ExecuteReader();
            GetData2();
            dr.Close();
            cn.Close();
        }

        private void Hlace_cijena_silazno_Click(object sender, EventArgs e)
        {
            Cijena_silazno_sort();
        }

        private void Cijena_silazno_sort()
        {
            Hlace_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_oznaka = 'Hlace' order by Artikli_cijena desc", cn);
            dr = cm.ExecuteReader();
            GetData2();
            dr.Close();
            cn.Close();
        }

        private void Hlace_pretraga_input_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void Hlace_kratki_nogav_Click(object sender, EventArgs e)
        {
            Kratki_nogav_sort();
        }

        private void Kratki_nogav_sort()
        {
            Hlace_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_oznaka = 'Hlace' and Artikli_tip = 'Kratki nogav' order by Artikli_naziv", cn);
            dr = cm.ExecuteReader();
            GetData2();
            cn.Close();
        }

        private void Hlace_kratki_nogav_MouseEnter(object sender, EventArgs e)
        {
            Hlace_kratki_nogav.ForeColor = Color.FromArgb(176, 92, 224);
        }

        private void Hlace_kratki_nogav_MouseLeave(object sender, EventArgs e)
        {
            Hlace_kratki_nogav.ForeColor = Color.FromArgb(0, 0, 0);
        }

        private void Hlace_dugi_nogav_Click(object sender, EventArgs e)
        {
            Dugi_nogav_sort();
        }

        private void Dugi_nogav_sort()
        {
            Hlace_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_oznaka = 'Hlace' and Artikli_tip = 'Dugi nogav' order by Artikli_naziv", cn);
            dr = cm.ExecuteReader();
            GetData2();
            dr.Close();
            cn.Close();
        }

        private void Hlace_dugi_nogav_MouseEnter(object sender, EventArgs e)
        {
            Hlace_dugi_nogav.ForeColor = Color.FromArgb(176, 92, 224);
        }

        private void Hlace_dugi_nogav_MouseLeave(object sender, EventArgs e)
        {
            Hlace_dugi_nogav.ForeColor = Color.FromArgb(0, 0, 0);
        }

        private void GetData2()
        {
            while (dr.Read())
            {
                long len = dr.GetBytes(0, 0, null, 0, 0);
                byte[] array = new byte[System.Convert.ToInt32(len) + 1];
                dr.GetBytes(0, 0, array, 0, System.Convert.ToInt32(len));
                pic = new PictureBox();
                pic.Width = 200;
                pic.Height = 315;
                pic.BackgroundImageLayout = ImageLayout.Stretch;
                pic.BorderStyle = BorderStyle.FixedSingle;
                Hlace_cijena = new Label();
                Hlace_cijena.Text = dr["Artikli_cijena"].ToString();
                Hlace_cijena.BackColor = Color.FromArgb(255, 121, 121);
                Hlace_cijena.TextAlign = ContentAlignment.MiddleCenter;
                Hlace_cijena.Width = 50;
                Hlace_cijena.Dock = DockStyle.Bottom;
                Hlace_naziv = new Label();
                Hlace_naziv.Text = dr["Artikli_naziv"].ToString();
                Hlace_naziv.BackColor = Color.FromArgb(255, 121, 121);
                Hlace_naziv.TextAlign = ContentAlignment.MiddleLeft;
                Hlace_naziv.Dock = DockStyle.Top;
                MemoryStream ms = new MemoryStream(array);
                Bitmap bitmap = new Bitmap(ms);
                pic.BackgroundImage = bitmap;
                pic.Controls.Add(Hlace_cijena);
                pic.Controls.Add(Hlace_naziv);
                Hlace_flowLayoutPanel1.Controls.Add(pic);
            }
        }

        private void Hlace_refresh_Click(object sender, EventArgs e)
        {
            GetData();
        }

    }
}
