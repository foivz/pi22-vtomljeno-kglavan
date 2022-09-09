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
    public partial class Majice : Form
    {
        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        private Label Majice_cijena;
        private Label Majice_naziv;
        private PictureBox pic;
        public static string nazivmajicenext, tagmajicenext;
        public static PictureBox pic2;

        public Majice()
        {
            InitializeComponent();
            Majice_panel2.BackColor = Color.FromArgb(255, 215, 114);
            Majice_kratki_rukav.Text = "Kratki rukav";
            Majice_kratki_rukav.Width = 150;
            Majice_kratki_rukav.Height = 25;
            Majice_kratki_rukav.BackgroundImageLayout = ImageLayout.Stretch;
            label5.Text = "Dugi rukav";
            label5.Width = 150;
            label5.Height = 25;
            label5.BackgroundImageLayout = ImageLayout.Stretch;
            label6.Text = "Bez rukava";
            label6.Width = 150;
            label6.Height = 25;
            label6.BackgroundImageLayout = ImageLayout.Stretch;
            Majice_artikli_majice.BackColor = Color.FromArgb(255, 215, 114);
            Majice_artikli_hlace.BackColor = Color.FromArgb(255, 215, 114);
            Majice_artikli_haljine.BackColor = Color.FromArgb(255, 215, 114);
            Majice_artikli_obuca.BackColor = Color.FromArgb(255, 215, 114);
            cn = new MySqlConnection();
            cn.ConnectionString = "server=localhost; user id=root; password=; database=probaslika2";
        }

        private void majice_Load(object sender, EventArgs e)
        {
            GetData33();
        }

        //private void GetData()
        //{
        //    Majice_flowLayoutPanel1.Controls.Clear();
        //    cn.Open();
        //    cm = new MySqlCommand("select Majice_id, Majice_slika, Majice_naziv, Majice_cijena, Majice_tip from majice where Majice_naziv like '%" + Majice_pretraga_input.Text + "%' order by Majice_naziv", cn);
        //    dr = cm.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        long len = dr.GetBytes(1, 0, null, 0, 0);
        //        byte[] array = new byte[System.Convert.ToInt32(len) + 1];
        //        dr.GetBytes(1, 0, array, 0, System.Convert.ToInt32(len));
        //        pic = new PictureBox();
        //        pic.Width = 200;
        //        pic.Height = 315;
        //        pic.BackgroundImageLayout = ImageLayout.Stretch;
        //        pic.BorderStyle = BorderStyle.FixedSingle;
        //        pic.Tag = dr["Majice_id"].ToString();
        //        Majice_cijena = new Label();
        //        Majice_cijena.Text = dr["Majice_cijena"].ToString();
        //        Majice_cijena.BackColor = Color.FromArgb(255, 121, 121);
        //        Majice_cijena.TextAlign = ContentAlignment.MiddleCenter;
        //        Majice_cijena.Width = 50;
        //        Majice_cijena.Dock = DockStyle.Bottom;
        //        Majice_cijena.Tag = dr["Majice_id"].ToString();
        //        Majice_naziv = new Label();
        //        Majice_naziv.Text = dr["Majice_naziv"].ToString();
        //        Majice_naziv.BackColor = Color.FromArgb(255, 121, 121);
        //        Majice_naziv.TextAlign = ContentAlignment.MiddleLeft;
        //        Majice_naziv.Dock = DockStyle.Top;
        //        Majice_naziv.Tag = dr["Majice_naziv"].ToString();
        //        MemoryStream ms = new MemoryStream(array);
        //        Bitmap bitmap = new Bitmap(ms);
        //        pic.BackgroundImage = bitmap;
        //        pic.Controls.Add(Majice_cijena);
        //        pic.Controls.Add(Majice_naziv);
        //        Majice_flowLayoutPanel1.Controls.Add(pic);
        //        pic.Click += new EventHandler(OnClick);
        //    }
        //    dr.Close();
        //    cn.Close();
        //}
        
        private void GetData33()
        {
            Majice_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_id, Artikli_slika, Artikli_naziv, Artikli_cijena, Artikli_tip from artikli where Artikli_oznaka = 'Majice' and Artikli_naziv like '%" + Majice_pretraga_input.Text + "%' order by Artikli_naziv", cn);
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
                Majice_cijena = new Label();
                Majice_cijena.Text = dr["Artikli_cijena"].ToString();
                Majice_cijena.BackColor = Color.FromArgb(255, 121, 121);
                Majice_cijena.TextAlign = ContentAlignment.MiddleCenter;
                Majice_cijena.Width = 50;
                Majice_cijena.Dock = DockStyle.Bottom;
                Majice_cijena.Tag = dr["Artikli_id"].ToString();
                Majice_naziv = new Label();
                Majice_naziv.Text = dr["Artikli_naziv"].ToString();
                Majice_naziv.BackColor = Color.FromArgb(255, 121, 121);
                Majice_naziv.TextAlign = ContentAlignment.MiddleLeft;
                Majice_naziv.Dock = DockStyle.Top;
                Majice_naziv.Tag = dr["Artikli_naziv"].ToString();
                MemoryStream ms = new MemoryStream(array);
                Bitmap bitmap = new Bitmap(ms);
                pic.BackgroundImage = bitmap;
                pic.Controls.Add(Majice_cijena);
                pic.Controls.Add(Majice_naziv);
                Majice_flowLayoutPanel1.Controls.Add(pic);
                pic.Click += new EventHandler(OnClick);
            }
            dr.Close();
            cn.Close();
        }

        private void OnClick(object sender, EventArgs e)
        {
            tagmajicenext = ((PictureBox)sender).Tag.ToString();

            this.Hide();
            Pregled_majice frm5 = new Pregled_majice();
            frm5.Show();
        }

        private void Majice_home_Click(object sender, EventArgs e)
        {
            this.Hide();
            nakonprijave nakonprijave = new nakonprijave();
            nakonprijave.Show();
        }

        private void Majice_artikli_Click(object sender, EventArgs e)
        {
            if (Majice_panel1.Height == 208)
            {
                Majice_panel1.Height = 44;
            }
            else
            {
                Majice_panel1.Height = 208;
            }
        }

        private void Majice_artikli_majice_Click(object sender, EventArgs e)
        {
            this.Hide();
            Majice majice = new Majice();
            majice.Show();
        }

        private void Majice_artikli_hlace_Click(object sender, EventArgs e)
        {
            this.Hide();
            Hlace hlace = new Hlace();
            hlace.Show();
        }

        private void Majice_artikli_haljine_Click(object sender, EventArgs e)
        {
            this.Hide();
            Haljine haljine = new Haljine();
            haljine.Show();
        }

        private void Majice_artikli_obuca_Click(object sender, EventArgs e)
        {
            this.Hide();
            Obuca obuca = new Obuca();
            obuca.Show();
        }

        private void Majice_kosarica_Click(object sender, EventArgs e)
        {
            this.Hide();
            Kosarica kosarica = new Kosarica();
            kosarica.Show();
        }

        private void Majice_odjava_Click(object sender, EventArgs e)
        {
            this.Hide();
            Prijava prijava = new Prijava();
            prijava.Show();
        }

        private void Majice_pomoc_MouseHover(object sender, EventArgs e)
        {
            Majice_toolTip1.IsBalloon = false;
            Majice_toolTip1.ToolTipIcon = ToolTipIcon.Info;
            Majice_toolTip1.Show("Ovdje Vam je omogućen pregled odjevnih artikala Majice!\nIste artikle moguće je pretražiti po nazivu i sortirati prema cijeni.\nKlikom na proizvod odlazite na formu za izbor veličina i dodavanja u košaricu!", Majice_profil);
            Majice_toolTip1.InitialDelay = 200;
            Majice_toolTip1.ReshowDelay = 100;
        }

        private void Kratki_rukav_Click(object sender, EventArgs e)
        {
            Kratki_rukav_sort();
        }

        private void Kratki_rukav_sort()
        {
            Majice_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_oznaka = 'Majice' and Artikli_tip = 'Kratki rukav' order by Artikli_naziv", cn);
            dr = cm.ExecuteReader();
            GetData2();
            dr.Close();
            cn.Close();
        }

        private void Majice_kratki_rukav_MouseEnter(object sender, EventArgs e)
        {
            Majice_kratki_rukav.ForeColor = Color.FromArgb(176, 92, 224);
        }

        private void Majice_kratki_rukav_MouseLeave(object sender, EventArgs e)
        {
            Majice_kratki_rukav.ForeColor = Color.FromArgb(0, 0, 0);
        }

        private void Dugi_rukav_Click(object sender, EventArgs e)
        {
            Dugi_rukav_sort();
        }

        private void Dugi_rukav_sort()
        {
            Majice_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_oznaka = 'Majice' and Artikli_tip = 'Dugi rukav' order by Artikli_naziv", cn);
            dr = cm.ExecuteReader();
            GetData2();
            dr.Close();
            cn.Close();
        }

        private void Majice_dugi_rukav_MouseEnter(object sender, EventArgs e)
        {
            label5.ForeColor = Color.FromArgb(176, 92, 224);
        }

        private void Majice_dugi_rukav_MouseLeave(object sender, EventArgs e)
        {
            label5.ForeColor = Color.FromArgb(0, 0, 0);
        }

        private void Majice_bez_rukava_Click(object sender, EventArgs e)
        {
            Bez_rukava_sort();
        }

        private void Bez_rukava_sort()
        {
            Majice_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_oznaka = 'Majice' and Artikli_tip = 'Bez rukava' order by Artikli_naziv", cn);
            dr = cm.ExecuteReader();
            GetData2();
            dr.Close();
            cn.Close();
        }

        private void Majice_bez_rukava_MouseEnter(object sender, EventArgs e)
        {
            label6.ForeColor = Color.FromArgb(176, 92, 224);
        }

        private void Majice_bez_rukava_MouseLeave(object sender, EventArgs e)
        {
            label6.ForeColor = Color.FromArgb(0, 0, 0);
        }

        private void Majice_refresh_Click(object sender, EventArgs e)
        {
            GetData33();
        }

        private void Majice_pretraga_input_TextChanged(object sender, EventArgs e)
        {
            GetData33();
        }


        private void Majice_cijena_btn_Click(object sender, EventArgs e)
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

        private void Majice_cijena_uzlazno_Click(object sender, EventArgs e)
        {
            Cijena_uzlazno_sort();
        }

        private void Cijena_uzlazno_sort()
        {
            Majice_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_oznaka = 'Majice' order by Artikli_cijena asc", cn);
            dr = cm.ExecuteReader();
            GetData2();
            dr.Close();
            cn.Close();
        }

        private void Majice_cijena_silazno_Click(object sender, EventArgs e)
        {
            Cijena_silazno_sort();
        }

        private void Cijena_silazno_sort()
        {
            Majice_flowLayoutPanel1.Controls.Clear();
            cn.Open();
            cm = new MySqlCommand("select Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_oznaka = 'Majice' order by Artikli_cijena desc", cn);
            dr = cm.ExecuteReader();
            GetData2();
            dr.Close();
            cn.Close();
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
                Majice_cijena = new Label();
                Majice_cijena.Text = dr["Artikli_cijena"].ToString();
                Majice_cijena.BackColor = Color.FromArgb(255, 121, 121);
                Majice_cijena.TextAlign = ContentAlignment.MiddleCenter;
                Majice_cijena.Width = 50;
                Majice_cijena.Dock = DockStyle.Bottom;
                Majice_naziv = new Label();
                Majice_naziv.Text = dr["Artikli_naziv"].ToString();
                Majice_naziv.BackColor = Color.FromArgb(255, 121, 121);
                Majice_naziv.TextAlign = ContentAlignment.MiddleLeft;
                Majice_naziv.Dock = DockStyle.Top;
                MemoryStream ms = new MemoryStream(array);
                Bitmap bitmap = new Bitmap(ms);
                pic.BackgroundImage = bitmap;
                pic.Controls.Add(Majice_cijena);
                pic.Controls.Add(Majice_naziv);
                Majice_flowLayoutPanel1.Controls.Add(pic);
            }
        }
    }
}