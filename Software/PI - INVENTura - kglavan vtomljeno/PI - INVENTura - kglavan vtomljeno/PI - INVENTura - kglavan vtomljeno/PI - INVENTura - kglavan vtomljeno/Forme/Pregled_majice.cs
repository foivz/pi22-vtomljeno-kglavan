using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace PI___INVENTura___kglavan_vtomljeno
{
    public partial class Pregled_majice : Form
    {

        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        private Label Majice_cijena;
        private Label Majice_naziv;

        private PictureBox pic;
        public static string nazivmajicenext, tagmajicenext;
        public static PictureBox pic2;
        private Button btn;
        public static string proba;

        public Pregled_majice()
        {
            InitializeComponent();
            button1.BackColor = Color.FromArgb(255, 215, 114);
            button2.BackColor = Color.FromArgb(255, 215, 114);
            button3.BackColor = Color.FromArgb(255, 215, 114);
            button4.BackColor = Color.FromArgb(255, 215, 114);
            Pregled_majice_dodaj.BackColor = Color.FromArgb(255, 215, 114);
            Pregled_majice_dostupnoKolicine.BorderStyle = BorderStyle.FixedSingle;
            cn = new MySqlConnection();
            cn.ConnectionString = "server=localhost; user id=root; password=; database=probaslika2";
            Pregled_majice_velicinaS.Tag = "S";
            Pregled_majice_velicinaM.Tag = "M";
            Pregled_majice_velicinaL.Tag = "L";
        }

        public void probamajicenext_Load(object sender, EventArgs e)
        {
            Pregled_majice_ID.Text = "Odabrali ste proizvod broj: " + Majice.tagmajicenext + "\n";
            GetData();
            label1.Visible = false;
        }

        private void GetData()
        {
            cn.Open();
            cm = new MySqlCommand("select Artikli_id, Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_id like '" + Majice.tagmajicenext + "' ", cn);
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

                Pregled_majice_Naziv.Text = dr["Artikli_naziv"].ToString();
                Pregled_majice_Naziv.TextAlign = ContentAlignment.MiddleLeft;
                Pregled_majice_Naziv.Tag = dr["Artikli_naziv"].ToString();
                Pregled_majice_Cijena.Text = dr["Artikli_cijena"].ToString();
                Pregled_majice_Cijena.TextAlign = ContentAlignment.MiddleLeft;
                Pregled_majice_Cijena.Tag = dr["Artikli_cijena"].ToString();
                MemoryStream ms = new MemoryStream(array);
                Bitmap bitmap = new Bitmap(ms);
                pic.BackgroundImage = bitmap;
                Pregled_majice_flowLayoutPanel2.Controls.Add(pic);
                Pregled_majice_dostupnoKolicine.Visible = false;
            }
            dr.Close();
            cn.Close();
        }

        private void Pregled_majice_home_Click(object sender, EventArgs e)
        {
            this.Hide();
            nakonprijave home = new nakonprijave();
            home.Show();
        }

        private void Pregled_majice_artikli_Click(object sender, EventArgs e)
        {
            if (panel1.Height == 208)
            {
                panel1.Height = 44;
            }
            else
            {
                panel1.Height = 208;
            }
        }

        private void Pregled_majice_artiklmajice_Click(object sender, EventArgs e)
        {
            this.Hide();
            Majice majice = new Majice();
            majice.Show();
        }

        private void Pregled_majice_artiklhace_Click(object sender, EventArgs e)
        {
            this.Hide();
            Hlace hlace = new Hlace();
            hlace.Show();
        }

        private void Pregled_majice_artiklhaljine_Click(object sender, EventArgs e)
        {
            this.Hide();
            Haljine haljine = new Haljine();
            haljine.Show();
        }

        private void Pregled_majice_artiklobuca_Click(object sender, EventArgs e)
        {
            this.Hide();
            Obuca obuca = new Obuca();
            obuca.Show();
        }

        private void Pregled_majice_kosarica_Click(object sender, EventArgs e)
        {
            this.Hide();
            Kosarica kosarica = new Kosarica();
            kosarica.Show();
        }

        private void Pregled_majice_odjava_Click(object sender, EventArgs e)
        {
            this.Hide();
            Prijava prijava = new Prijava();
            prijava.Show();
        }

        private void Pregled_majice_pomoc_MouseHover(object sender, EventArgs e)
        {
            Pregled_majice_toolTip1.IsBalloon = true;
            Pregled_majice_toolTip1.ToolTipIcon = ToolTipIcon.Info;
            Pregled_majice_toolTip1.Show("Ovdje Vam je omogućen detaljan prikaz odabranog odjevnog artikla, hlače!\nOmogućeno je biranje veličine i količine za istu (ako je dostupna) te dodavanje u Vašu košaricu!", Pregled_majice_pomoc);
            Pregled_majice_toolTip1.InitialDelay = 200;
            Pregled_majice_toolTip1.ReshowDelay = 100;
        }

        private void Pregled_majice_nazad_Click(object sender, EventArgs e)
        {
            this.Hide();
            Majice majice = new Majice();
            majice.Show();
        }

        private void Pregled_majice_velicinaS_CheckedChanged(object sender, EventArgs e)
        {
            //cn.Open();
            //cm = new MySqlCommand("select S from majice where Majice_id = '%"+ Pregled_majice_ID + "%' ", cn);
            //dr = cm.ExecuteReader();
            //dr.Read();
            //label2.Text = dr["S"].ToString();
            //dr.Close();
            //cn.Close();
        }

        private void Pregled_majice_velicinaS_Click(object sender, EventArgs e)
        {
            Pregled_majice_dodaj.Enabled = true;
            cn.Open();
            cm = new MySqlCommand("select S from artikli where Artikli_naziv like '%" + Pregled_majice_Naziv.Text + "%' ", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                Pregled_majice_dostupnoKolicine.Visible = true;
                Pregled_majice_dostupnoKolicine.Text = dr["S"].ToString();
            }
            if (Pregled_majice_dostupnoKolicine.Text == "0")
            {
                MessageBox.Show("Nažalost proizvod " + Pregled_majice_Naziv.Text + " u veličini S trenutno nije dostupan\n:(");
                Pregled_majice_dodaj.Enabled = false;
            }

            dr.Close();
            cn.Close();
        }

        private void Pregled_majice_velicinaM_Click(object sender, EventArgs e)
        {
            Pregled_majice_dodaj.Enabled = true;
            cn.Open();
            cm = new MySqlCommand("select M from artikli where Artikli_naziv like '%" + Pregled_majice_Naziv.Text + "%' ", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                Pregled_majice_dostupnoKolicine.Visible = true;
                Pregled_majice_dostupnoKolicine.Text = dr["M"].ToString();
            }
            if (Pregled_majice_dostupnoKolicine.Text == "0")
            {
                MessageBox.Show("Nažalost proizvod " + Pregled_majice_Naziv.Text + " u veličini M trenutno nije dostupan\n:(");
                Pregled_majice_dodaj.Enabled = false;
            }
            dr.Close();
            cn.Close();
        }

        private void Pregled_majice_velicinaL_Click(object sender, EventArgs e)
        {
            Pregled_majice_dodaj.Enabled = true;
            cn.Open();
            cm = new MySqlCommand("select L from artikli where Artikli_naziv like '%" + Pregled_majice_Naziv.Text + "%' ", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                Pregled_majice_dostupnoKolicine.Visible = true;
                Pregled_majice_dostupnoKolicine.Text = dr["L"].ToString();
            }
            if (Pregled_majice_dostupnoKolicine.Text == "0")
            {
                MessageBox.Show("Nažalost proizvod " + Pregled_majice_Naziv.Text + " u veličini L trenutno nije dostupan\n:(");
                Pregled_majice_dodaj.Enabled = false;
            }
            dr.Close();
            cn.Close();
        }

        private void Pregled_majice_dodaj_Click(object sender, EventArgs e)
        {

            if (Pregled_majice_velicinaS.Checked == true)
            {
                if (Pregled_majice_numericUpDown1.Value <= 0 || Pregled_majice_numericUpDown1.Value > 99 || Pregled_majice_numericUpDown1.Value > Convert.ToInt32(Pregled_majice_dostupnoKolicine.Text))
                {
                    MessageBox.Show("Izaberite dozvoljenu količinu: (max " + Pregled_majice_dostupnoKolicine.Text +" komada)");

                }

                else if (Pregled_majice_numericUpDown1.Value >= 1 || Pregled_majice_numericUpDown1.Value <= 99)
                {

                    decimal total;
                    total = Pregled_majice_numericUpDown1.Value * Decimal.Parse(Pregled_majice_Cijena.Text);
                    itemmodel model = new itemmodel

                    {
                        Item = Pregled_majice_Naziv.Text,
                        Quantity = Pregled_majice_numericUpDown1.Value,
                        Price = Decimal.Parse(Pregled_majice_Cijena.Text),
                        Size = Pregled_majice_velicinaS.Tag.ToString(),
                        Total = total,
                    };
                    shareddata.Items.Add(model);
                    label1.Visible = true;
                    label1.Text = "Proizvod " + Pregled_majice_Naziv.Text + " uspješno je dodan u Vašu košaricu!";
                }
            }
            else if (Pregled_majice_velicinaM.Checked == true)
            {
                if (Pregled_majice_velicinaM.Checked == true)
                {
                    if (Pregled_majice_numericUpDown1.Value <= 0 || Pregled_majice_numericUpDown1.Value > 99 || Pregled_majice_numericUpDown1.Value > Convert.ToInt32(Pregled_majice_dostupnoKolicine.Text))
                    {
                        MessageBox.Show("Izaberite dozvoljenu količinu: (max " + Pregled_majice_dostupnoKolicine.Text + " komada)");

                    }
                    else if (Pregled_majice_numericUpDown1.Value >= 1 || Pregled_majice_numericUpDown1.Value <= 99)
                    {
                        decimal total;
                        total = Pregled_majice_numericUpDown1.Value * Decimal.Parse(Pregled_majice_Cijena.Text);
                        itemmodel model = new itemmodel

                        {
                            Item = Pregled_majice_Naziv.Text,
                            Quantity = Pregled_majice_numericUpDown1.Value,
                            Price = Decimal.Parse(Pregled_majice_Cijena.Text),
                            Size = Pregled_majice_velicinaM.Tag.ToString(),
                            Total = total,
                        };
                        shareddata.Items.Add(model);
                        label1.Visible = true;
                        label1.Text = "Proizvod " + Pregled_majice_Naziv.Text + " uspješno je dodan u Vašu košaricu!";
                    }

                }
            }
            else if (Pregled_majice_velicinaL.Checked == true)
            {
                if (Pregled_majice_velicinaL.Checked == true)
                {
                    if (Pregled_majice_numericUpDown1.Value <= 0 || Pregled_majice_numericUpDown1.Value > 99 || Pregled_majice_numericUpDown1.Value > Convert.ToInt32(Pregled_majice_dostupnoKolicine.Text))
                    {
                        MessageBox.Show("Izaberite dozvoljenu količinu: (max " + Pregled_majice_dostupnoKolicine.Text + " komada)");

                    }
                    else if (Pregled_majice_numericUpDown1.Value >= 1 || Pregled_majice_numericUpDown1.Value <= 99)
                    {
                        decimal total;
                        total = Pregled_majice_numericUpDown1.Value * Decimal.Parse(Pregled_majice_Cijena.Text);
                        itemmodel model = new itemmodel

                        {
                            Item = Pregled_majice_Naziv.Text,
                            Quantity = Pregled_majice_numericUpDown1.Value,
                            Price = Decimal.Parse(Pregled_majice_Cijena.Text),
                            Size = Pregled_majice_velicinaL.Tag.ToString(),
                            Total = total,
                        };
                        shareddata.Items.Add(model);
                        label1.Visible = true;
                        label1.Text = "Proizvod " + Pregled_majice_Naziv.Text + " uspješno je dodan u Vašu košaricu!";
                    }
                }
            }
            else
            {
                MessageBox.Show("Pozdrav! Odaberite veličinu");
            }
        }
    }
}
