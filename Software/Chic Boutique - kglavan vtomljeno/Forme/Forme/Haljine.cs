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
    public partial class Haljine : Form
    {

        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        private PictureBox pic;
        private Label Haljine_cijena;
        private Label Haljine_naziv;
        public static string nazivhaljinenext, taghaljinenext;

        public Haljine()
        {
            InitializeComponent();
            Haljine_artikli_majice.BackColor = Color.FromArgb(255, 215, 114);
            Haljine_artikli_hlace.BackColor = Color.FromArgb(255, 215, 114);
            Haljine_artikli_haljine.BackColor = Color.FromArgb(255, 215, 114);
            Haljine_artikli_obuca.BackColor = Color.FromArgb(255, 215, 114);
            Haljine_panel2.BackColor = Color.FromArgb(255, 215, 114);
            cn = new MySqlConnection();
            cn.ConnectionString = "server=localhost;user id=root;password=;database=probaslika2";
        }

        private void haljine_Load(object sender, EventArgs e)
        {

            GetData();
        }

        private void GetData()
        {
            Haljine_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_id, Artikli_slika, Artikli_naziv, Artikli_cijena, Artikli_tip from artikli where Artikli_oznaka = 'Haljine' and Artikli_naziv like '%" + Haljine_pretraga_input.Text + "%' order by Artikli_naziv", cn);
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
                Haljine_cijena = new Label();
                Haljine_cijena.Text = dr["Artikli_cijena"].ToString();
                Haljine_cijena.BackColor = Color.FromArgb(255, 121, 121);
                Haljine_cijena.TextAlign = ContentAlignment.MiddleCenter;
                Haljine_cijena.Width = 50;
                Haljine_cijena.Dock = DockStyle.Bottom;
                Haljine_cijena.Tag = dr["Artikli_id"].ToString();
                Haljine_naziv = new Label();
                Haljine_naziv.Text = dr["Artikli_naziv"].ToString();
                Haljine_naziv.BackColor = Color.FromArgb(255, 121, 121);
                Haljine_naziv.TextAlign = ContentAlignment.MiddleLeft;
                Haljine_naziv.Dock = DockStyle.Top;
                Haljine_naziv.Tag = dr["Artikli_naziv"].ToString();
                MemoryStream ms = new MemoryStream(array);
                Bitmap bitmap = new Bitmap(ms);
                pic.BackgroundImage = bitmap;
                pic.Controls.Add(Haljine_cijena);
                pic.Controls.Add(Haljine_naziv);
                Haljine_flowLayoutPanel1.Controls.Add(pic);
                pic.Click += new EventHandler(OnClick);
            }
            dr.Close();
            cn.Close();
        }

        private void OnClick(object sender, EventArgs e)
        {
            taghaljinenext = ((PictureBox)sender).Tag.ToString();
            this.Hide();
            Pregled_haljine pregled_haljine = new Pregled_haljine();
            pregled_haljine.Show();
        }

        private void Haljine_odjava_Click(object sender, EventArgs e)
        {
            this.Hide();
            Prijava prijava = new Prijava();
            prijava.Show();
        }

        private void Haljine_artikli_Click(object sender, EventArgs e)
        {
            if (Haljine_panel1.Height == 208)
            {
                Haljine_panel1.Height = 44;
            }
            else
            {
                Haljine_panel1.Height = 208;
            }
        }

        private void Haljine_artikli_majice_Click(object sender, EventArgs e)
        {
            this.Hide();
            Majice majice = new Majice();
            majice.Show();
        }

        private void Haljine_artikli_hlace_Click(object sender, EventArgs e)
        {
            this.Hide();
            Hlace hlace = new Hlace();
            hlace.Show();
        }

        private void Haljine_artikli_haljine_Click(object sender, EventArgs e)
        {
            this.Hide();
            Haljine haljine = new Haljine();
            haljine.Show();
        }

        private void Haljine_artikli_obuca_Click(object sender, EventArgs e)
        {
            this.Hide();
            Obuca obuca = new Obuca();
            obuca.Show();
        }

        private void Haljine_kosarica_Click(object sender, EventArgs e)
        {
            this.Hide();
            Kosarica kosarica = new Kosarica();
            kosarica.Show();
        }

        private void Haljine_pomoc_MouseHover(object sender, EventArgs e)
        {
            Haljine_toolTip1.IsBalloon = false;
            Haljine_toolTip1.ToolTipIcon = ToolTipIcon.Info;
            Haljine_toolTip1.Show("Ovdje Vam je omogućen pregled odjevnih artikala Haljine!\nIste artikle moguće je pretražiti po nazivu i sortirati prema cijeni.\nKlikom na proizvod odlazite na formu za izbor veličina i dodavanja u košaricu!", Haljine_pomoc);
            Haljine_toolTip1.InitialDelay = 200;
            Haljine_toolTip1.ReshowDelay = 100;
        }

        private void Haljine_pregledcijena_Click(object sender, EventArgs e)
        {
            if (panel3.Width == 200)
            {
                panel3.Width = 75;
            }
            else
            {
                panel3.Width = 200;
            }
        }

        private void Haljine_cijena_uzlazno_Click(object sender, EventArgs e)
        {
            Haljine_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_oznaka = 'Haljine' order by Artikli_cijena asc", cn);
            dr = cm.ExecuteReader();
            Haljine_sort_uzlazno();
            dr.Close();
            cn.Close();

        }

        private void Haljine_sort_uzlazno()
        {
            GetData2();
        }

        private void Haljine_cijena_silazno_Click(object sender, EventArgs e)
        {
            Haljine_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_oznaka = 'Haljine' order by Artikli_cijena desc", cn);
            dr = cm.ExecuteReader();
            Haljine_sort_silazno();
            dr.Close();
            cn.Close();
        }

        private void Haljine_pretraga_input_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void Haljine_haljine2_Click(object sender, EventArgs e)
        {
            Haljine_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_oznaka = 'Haljine' and Artikli_tip = 'Haljina' order by Artikli_naziv", cn);
            dr = cm.ExecuteReader();
            Haljine_sort();
            dr.Close();
            cn.Close();
        }

        private void Haljine_sort()
        {
            GetData2();
        }

        private void Haljine_suknje_Click(object sender, EventArgs e)
        {
            Suknje_sort();
        }

        private void Suknje_sort()
        {
            Haljine_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_oznaka = 'Haljine' and Artikli_tip = 'Suknja' order by Artikli_naziv", cn);
            dr = cm.ExecuteReader();
            GetData2();
            dr.Close();
            cn.Close();
        }

        private void Haljine_haljine2_MouseEnter(object sender, EventArgs e)
        {
            Haljine_haljine2.ForeColor = Color.FromArgb(176, 92, 224);
        }

        private void Haljine_haljine2_MouseLeave(object sender, EventArgs e)
        {
            Haljine_haljine2.ForeColor = Color.FromArgb(0, 0, 0);
        }

        private void Haljine_suknje_MouseEnter(object sender, EventArgs e)
        {
            Haljine_suknje.ForeColor = Color.FromArgb(176, 92, 224);
        }

        private void Haljine_suknje_MouseLeave(object sender, EventArgs e)
        {
            Haljine_suknje.ForeColor = Color.FromArgb(0, 0, 0);
        }

        private void Haljine_sort_silazno()
        {
            GetData2();
        }

        private void Haljine_refresh_Click(object sender, EventArgs e)
        {
            GetData();
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
                Haljine_cijena = new Label();
                Haljine_cijena.Text = dr["Artikli_cijena"].ToString();
                Haljine_cijena.BackColor = Color.FromArgb(255, 121, 121);
                Haljine_cijena.TextAlign = ContentAlignment.MiddleCenter;
                Haljine_cijena.Width = 50;
                Haljine_cijena.Dock = DockStyle.Bottom;
                Haljine_naziv = new Label();
                Haljine_naziv.Text = dr["Artikli_naziv"].ToString();
                Haljine_naziv.BackColor = Color.FromArgb(255, 121, 121);
                Haljine_naziv.TextAlign = ContentAlignment.MiddleLeft;
                Haljine_naziv.Dock = DockStyle.Top;
                MemoryStream ms = new MemoryStream(array);
                Bitmap bitmap = new Bitmap(ms);
                pic.BackgroundImage = bitmap;
                pic.Controls.Add(Haljine_cijena);
                pic.Controls.Add(Haljine_naziv);
                Haljine_flowLayoutPanel1.Controls.Add(pic);
            }
        }
    }
}
