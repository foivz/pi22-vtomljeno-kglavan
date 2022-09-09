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
    public partial class Obuca : Form
    {

        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        private PictureBox pic;
        private Label Obuca_cijena;
        private Label Obuca_naziv;
        public static string nazivobucanext, tagobucanext;

        public Obuca()
        {
            InitializeComponent();
            Obuca_panel2.BackColor = Color.FromArgb(255, 215, 114);
            Obuca_artikli_majice.BackColor = Color.FromArgb(255, 215, 114);
            Obuca_artikli_hlace.BackColor = Color.FromArgb(255, 215, 114);
            Obuca_artikli_haljine.BackColor = Color.FromArgb(255, 215, 114);
            Obuca_artikli_obuca.BackColor = Color.FromArgb(255, 215, 114);
            cn = new MySqlConnection();
            cn.ConnectionString = "server=localhost;user id=root;password=;database=probaslika2";
        }

        private void obuca_Load(object sender, EventArgs e)
        {
            Obuca_panel1.Height = 44;
            GetData();
        }

        private void GetData()
        {
            Obuca_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_id, Artikli_slika, Artikli_naziv, Artikli_cijena, Artikli_tip from artikli where Artikli_oznaka = 'Obuca' and Artikli_naziv like '%" + Obuca_pretraga_input.Text + "%' order by Artikli_naziv", cn);
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
                Obuca_cijena = new Label();
                Obuca_cijena.Text = dr["Artikli_cijena"].ToString();
                Obuca_cijena.BackColor = Color.FromArgb(255, 121, 121);
                Obuca_cijena.TextAlign = ContentAlignment.MiddleCenter;
                Obuca_cijena.Width = 50;
                Obuca_cijena.Dock = DockStyle.Bottom;
                Obuca_cijena.Tag = dr["Artikli_id"].ToString();
                Obuca_naziv = new Label();
                Obuca_naziv.Text = dr["Artikli_naziv"].ToString();
                Obuca_naziv.BackColor = Color.FromArgb(255, 121, 121);
                Obuca_naziv.TextAlign = ContentAlignment.MiddleLeft;
                Obuca_naziv.Dock = DockStyle.Top;
                Obuca_naziv.Tag = dr["Artikli_naziv"].ToString();
                MemoryStream ms = new MemoryStream(array);
                Bitmap bitmap = new Bitmap(ms);
                pic.BackgroundImage = bitmap;
                pic.Controls.Add(Obuca_cijena);
                pic.Controls.Add(Obuca_naziv);
                Obuca_flowLayoutPanel1.Controls.Add(pic);
                pic.Click += new EventHandler(OnClick);
            }
            dr.Close();
            cn.Close();
        }

        private void OnClick(object sender, EventArgs e)
        {
            tagobucanext = ((PictureBox)sender).Tag.ToString();

            this.Hide();
            Pregled_obuca pregled_obuca = new Pregled_obuca();
            pregled_obuca.Show();
        }

        private void Obuca_home_Click(object sender, EventArgs e)
        {
            this.Hide();
            nakonprijave frm4 = new nakonprijave();
            frm4.Show();
        }

        private void Obuca_artikli_Click(object sender, EventArgs e)
        {
            if (Obuca_panel1.Height == 208)
            {
                Obuca_panel1.Height = 44;
            }
            else
            {
                Obuca_panel1.Height = 208;
            }
        }

        private void Obuca_artikli_majice_Click(object sender, EventArgs e)
        {
            this.Hide();
            Majice frm7 = new Majice();
            frm7.Show();
        }

        private void Obuca_artikli_hlace_Click(object sender, EventArgs e)
        {
            this.Hide();
            Hlace frm8 = new Hlace();
            frm8.Show();
        }

        private void Obuca_artikli_haljine_Click(object sender, EventArgs e)
        {
            this.Hide();
            Haljine frm9 = new Haljine();
            frm9.Show();
        }

        private void Obuca_artikli_obuca_Click(object sender, EventArgs e)
        {
            this.Hide();
            Obuca frm0 = new Obuca();
            frm0.Show();
        }

        private void Obuca_kosarica_Click(object sender, EventArgs e)
        {
            this.Hide();
            Kosarica kosarica = new Kosarica();
            kosarica.Show();
        }

        private void Obuca_odjava_Click(object sender, EventArgs e)
        {
            this.Hide();
            Prijava prijava = new Prijava();
            prijava.Show();
        }

        private void Obuca_pomoc_MouseHover(object sender, EventArgs e)
        {
            Obuca_toolTip1.IsBalloon = false;
            Obuca_toolTip1.ToolTipIcon = ToolTipIcon.Info;
            Obuca_toolTip1.Show("Ovdje Vam je omogućen pregled odjevnih artikala Obuća!\nIste artikle moguće je pretražiti po nazivu i sortirati prema cijeni.\nKlikom na proizvod odlazite na formu za izbor veličina i dodavanja u košaricu!", Obuca_pomoc);
            Obuca_toolTip1.InitialDelay = 200;
            Obuca_toolTip1.ReshowDelay = 100;
        }

        private void Obuca_pregledcijena_Click(object sender, EventArgs e)
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

        private void Obuca_cijena_uzlazno_Click(object sender, EventArgs e)
        {
            Obuca_uzlazno();
        }

        private void Obuca_uzlazno()
        {
            Obuca_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_oznaka = 'Obuca' order by Artikli_cijena asc", cn);
            dr = cm.ExecuteReader();
            GetData2();
            dr.Close();
            cn.Close();
        }

        private void Obuca_cijena_silazno_Click(object sender, EventArgs e)
        {
            Obuca_silazno();
        }

        private void Obuca_silazno()
        {
            Obuca_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_oznaka = 'Obuca' order by Artikli_cijena desc", cn);
            dr = cm.ExecuteReader();
            GetData2();
            dr.Close();
            cn.Close();
        }

        private void Obuca_pretraga_input_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void Obuca_tenisice_Click(object sender, EventArgs e)
        {
            Filter_tenisice();
        }

        private void Filter_tenisice()
        {
            Obuca_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_oznaka = 'Obuca' and Artikli_tip = 'Tenisice' order by Artikli_naziv", cn);
            dr = cm.ExecuteReader();
            GetData2();
            dr.Close();
            cn.Close();
        }

        private void Obuca_tenisice_MouseEnter(object sender, EventArgs e)
        {
            Obuca_tenisice.ForeColor = Color.FromArgb(176, 92, 224);
        }

        private void Obuca_tenisice_MouseLeave(object sender, EventArgs e)
        {
            Obuca_tenisice.ForeColor = Color.FromArgb(0, 0, 0);
        }

        private void Obuca_sandale_Click(object sender, EventArgs e)
        {
            Filteri_sandale();
        }

        private void Filteri_sandale()
        {
            Obuca_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_oznaka = 'Obuca' and Artikli_tip = 'Sandale' order by Artikli_naziv", cn);
            dr = cm.ExecuteReader();
            GetData2();
            dr.Close();
            cn.Close();
        }

        private void Obuca_sandale_MouseEnter(object sender, EventArgs e)
        {
            Obuca_sandale.ForeColor = Color.FromArgb(176, 92, 224);
        }

        private void Obuca_sandale_MouseLeave(object sender, EventArgs e)
        {
            Obuca_sandale.ForeColor = Color.FromArgb(0, 0, 0);
        }

        private void Obuca_stikle_Click(object sender, EventArgs e)
        {
            Filteri_stikle();
        }

        private void Filteri_stikle()
        {
            Obuca_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_oznaka = 'Obuca' and Artikli_tip = 'Štikle' order by Artikli_naziv", cn);
            dr = cm.ExecuteReader();
            GetData2();
            dr.Close();
            cn.Close();
        }

        private void Obuca_stikle_MouseEnter(object sender, EventArgs e)
        {
            Obuca_stikle.ForeColor = Color.FromArgb(176, 92, 224);
        }

        private void Obuca_stikle_MouseLeave(object sender, EventArgs e)
        {
            Obuca_stikle.ForeColor = Color.FromArgb(0, 0, 0);
        }

        private void Obuca_refresh_Click(object sender, EventArgs e)
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
                Obuca_cijena = new Label();
                Obuca_cijena.Text = dr["Artikli_cijena"].ToString();
                Obuca_cijena.BackColor = Color.FromArgb(255, 121, 121);
                Obuca_cijena.TextAlign = ContentAlignment.MiddleCenter;
                Obuca_cijena.Width = 50;
                Obuca_cijena.Dock = DockStyle.Bottom;
                Obuca_naziv = new Label();
                Obuca_naziv.Text = dr["Artikli_naziv"].ToString();
                Obuca_naziv.BackColor = Color.FromArgb(255, 121, 121);
                Obuca_naziv.TextAlign = ContentAlignment.MiddleLeft;
                Obuca_naziv.Dock = DockStyle.Top;
                MemoryStream ms = new MemoryStream(array);
                Bitmap bitmap = new Bitmap(ms);
                pic.BackgroundImage = bitmap;
                pic.Controls.Add(Obuca_cijena);
                pic.Controls.Add(Obuca_naziv);
                Obuca_flowLayoutPanel1.Controls.Add(pic);
            }
        }
    }
}
