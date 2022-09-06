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
    public partial class Pregled_haljine : Form
    {

        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        private PictureBox pic;
        public static string nazivhaljinenext, taghaljinenext;
        public static PictureBox pic2;
        private Button btn;
        public static string proba;

        public Pregled_haljine()
        {
            InitializeComponent();

            Pregled_hlace_artikli_majice.BackColor = Color.FromArgb(255, 215, 114);
            Pregled_hlace_artikli_hlace.BackColor = Color.FromArgb(255, 215, 114);
            Pregled_hlace_artikli_haljine.BackColor = Color.FromArgb(255, 215, 114);
            Pregled_hlace_artikli_obuca.BackColor = Color.FromArgb(255, 215, 114);
            Pregled_haljine_dodaj.BackColor = Color.FromArgb(255, 215, 114);
            label2.BorderStyle = BorderStyle.FixedSingle;
            Pregled_haljine_velicinaS.Tag = "S";
            Pregled_haljine_velicinaM.Tag = "M";
            Pregled_haljine_velicinaL.Tag = "L";
            cn = new MySqlConnection();
            cn.ConnectionString = "server=localhost; user id=root; password=; database=probaslika2";

        }

        private void Pregled_haljine_Load(object sender, EventArgs e)
        {
            Pregled_haljine_ID.Text = "Odabrali ste proizvod broj: " + Haljine.taghaljinenext + "\n";
            GetData();
            label1.Visible = false;
        }

        private void GetData()
        {
            cn.Open();
            cm = new MySqlCommand("select Artikli_id, Artikli_slika, Artikli_naziv, Artikli_cijena from artikli where Artikli_id like '" + Haljine.taghaljinenext + "' ", cn);
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
                Pregled_haljine_Naziv.Text = dr["Artikli_naziv"].ToString();
                Pregled_haljine_Naziv.TextAlign = ContentAlignment.MiddleLeft;
                Pregled_haljine_Naziv.Tag = dr["Artikli_naziv"].ToString();
                Pregled_haljine_Cijena.Text = dr["Artikli_cijena"].ToString();
                Pregled_haljine_Cijena.TextAlign = ContentAlignment.MiddleLeft;
                Pregled_haljine_Cijena.Tag = dr["Artikli_cijena"].ToString();
                MemoryStream ms = new MemoryStream(array);
                Bitmap bitmap = new Bitmap(ms);
                pic.BackgroundImage = bitmap;
                Pregled_haljine_flowLayoutPanel2.Controls.Add(pic);
                label2.Visible = false;
            }
            dr.Close();
            cn.Close();
        }

        private void Pregled_haljine_home_Click(object sender, EventArgs e)
        {
            this.Hide();
            nakonprijave nakonprijave = new nakonprijave();
            nakonprijave.Show();
        }

        private void Pregled_haljine_artikli_Click(object sender, EventArgs e)
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

        private void Pregled_hlace_artikli_majice_Click(object sender, EventArgs e)
        {
            this.Hide();
            Majice majice = new Majice();
            majice.Show();
        }

        private void Pregled_hlace_artikli_hlace_Click(object sender, EventArgs e)
        {
            this.Hide();
            Hlace hlace = new Hlace();
            hlace.Show();
        }

        private void Pregled_hlace_artikli_haljine_Click(object sender, EventArgs e)
        {
            this.Hide();
            Haljine haljine = new Haljine();
            haljine.Show();
        }

        private void Pregled_hlace_artikli_obuca_Click(object sender, EventArgs e)
        {
            this.Hide();
            Obuca obuca = new Obuca();
            obuca.Show();
        }

        private void Pregled_haljine_kosarica_Click(object sender, EventArgs e)
        {
            this.Hide();
            Kosarica kosarica = new Kosarica();
            kosarica.Show();
        }

        private void Pregled_haljine_odjava_Click(object sender, EventArgs e)
        {
            this.Hide();
            Prijava prijava = new Prijava();
            prijava.Show();
        }

        private void Pregled_haljine_pomoc_MouseHover(object sender, EventArgs e)
        {
            Pregled_haljine_toolTip1.IsBalloon = true;
            Pregled_haljine_toolTip1.ToolTipIcon = ToolTipIcon.Info;
            Pregled_haljine_toolTip1.Show("Ovdje Vam je omogućen detaljan prikaz odabranog odjevnog artikla, hlače!\nOmogućeno je biranje veličine i količine za istu (ako je dostupna) te dodavanje u Vašu košaricu!", Pregled_haljine_pomoc);
            Pregled_haljine_toolTip1.InitialDelay = 200;
            Pregled_haljine_toolTip1.ReshowDelay = 100;
        }

        private void Pregled_haljine_dodaj_Click(object sender, EventArgs e)
        {
            if (Pregled_haljine_velicinaS.Checked == true)
            {
                if (Pregled_haljine_numericUpDown1.Value <= 0 || Pregled_haljine_numericUpDown1.Value > 99 || Pregled_haljine_numericUpDown1.Value > Convert.ToInt32(label2.Text))
                {
                    MessageBox.Show("Izaberite dozvoljenu količinu: (max " + label2.Text + " komada)");

                }
                else if (Pregled_haljine_numericUpDown1.Value >= 1 || Pregled_haljine_numericUpDown1.Value <= 99)
                {

                    decimal total;
                    total = Pregled_haljine_numericUpDown1.Value * Decimal.Parse(Pregled_haljine_Cijena.Text);
                    itemmodel model = new itemmodel

                    {
                        Item = Pregled_haljine_Naziv.Text,
                        Quantity = Pregled_haljine_numericUpDown1.Value,
                        Price = Decimal.Parse(Pregled_haljine_Cijena.Text),
                        Size = Pregled_haljine_velicinaS.Tag.ToString(),
                        Total = total,
                    };
                    shareddata.Items.Add(model);
                    label1.Visible = true;
                    label1.Text = "Proizvod " + Pregled_haljine_Naziv.Text + " uspješno je dodan u Vašu košaricu!";
                }
            }
            else if (Pregled_haljine_velicinaM.Checked == true)
            {
                if (Pregled_haljine_velicinaM.Checked == true)
                {
                    if (Pregled_haljine_numericUpDown1.Value <= 0 || Pregled_haljine_numericUpDown1.Value > 99 || Pregled_haljine_numericUpDown1.Value > Convert.ToInt32(label2.Text))
                    {
                        MessageBox.Show("Izaberite dozvoljenu količinu: (max " + label2.Text + " komada)");

                    }
                    else if (Pregled_haljine_numericUpDown1.Value >= 1 || Pregled_haljine_numericUpDown1.Value <= 99)
                    {
                        decimal total;
                        total = Pregled_haljine_numericUpDown1.Value * Decimal.Parse(Pregled_haljine_Cijena.Text);
                        itemmodel model = new itemmodel
                        {
                            Item = Pregled_haljine_Naziv.Text,
                            Quantity = Pregled_haljine_numericUpDown1.Value,
                            Price = Decimal.Parse(Pregled_haljine_Cijena.Text),
                            Size = Pregled_haljine_velicinaM.Tag.ToString(),
                            Total = total,
                        };
                        shareddata.Items.Add(model);
                        label1.Visible = true;
                        label1.Text = "Proizvod " + Pregled_haljine_Naziv.Text + " uspješno je dodan u Vašu košaricu!";
                    }

                }
            }
            else if (Pregled_haljine_velicinaL.Checked == true)
            {
                if (Pregled_haljine_velicinaL.Checked == true)
                {
                    if (Pregled_haljine_numericUpDown1.Value <= 0 || Pregled_haljine_numericUpDown1.Value > 99 || Pregled_haljine_numericUpDown1.Value > Convert.ToInt32(label2.Text))
                    {
                        MessageBox.Show("Izaberite dozvoljenu količinu: (max " + label2.Text + " komada)");

                    }
                    else if (Pregled_haljine_numericUpDown1.Value >= 1 || Pregled_haljine_numericUpDown1.Value <= 99)
                    {
                        decimal total;
                        total = Pregled_haljine_numericUpDown1.Value * Decimal.Parse(Pregled_haljine_Cijena.Text);
                        itemmodel model = new itemmodel

                        {
                            Item = Pregled_haljine_Naziv.Text,
                            Quantity = Pregled_haljine_numericUpDown1.Value,
                            Price = Decimal.Parse(Pregled_haljine_Cijena.Text),
                            Size = Pregled_haljine_velicinaL.Tag.ToString(),
                            Total = total,
                        };
                        shareddata.Items.Add(model);
                        label1.Visible = true;
                        label1.Text = "Proizvod " + Pregled_haljine_Naziv.Text + " uspješno je dodan u Vašu košaricu!";
                    }
                }
            }
            else
            {
                MessageBox.Show("Pozdrav! Odaberite veličinu");
            }
        }

        private void Pregled_haljine_velicinaS_Click(object sender, EventArgs e)
        {
            Pregled_haljine_dodaj.Enabled = true;
            cn.Open();
            cm = new MySqlCommand("select S from artikli where Artikli_naziv like '%" + Pregled_haljine_Naziv.Text + "%' ", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                label2.Visible = true;
                label2.Text = dr["S"].ToString();
            }
            if (label2.Text == "0")
            {
                MessageBox.Show("Nažalost proizvod " + Pregled_haljine_Naziv.Text + " u veličini S trenutno nije dostupan\n:(");
                Pregled_haljine_dodaj.Enabled = false;
            }
            dr.Close();
            cn.Close();
        }

        private void Pregled_haljine_velicinaM_Click(object sender, EventArgs e)
        {
            Pregled_haljine_dodaj.Enabled = true;
            cn.Open();
            cm = new MySqlCommand("select M from artikli where Artikli_naziv like '%" + Pregled_haljine_Naziv.Text + "%' ", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                label2.Visible = true;
                label2.Text = dr["M"].ToString();
            }
            if (label2.Text == "0")
            {
                MessageBox.Show("Nažalost proizvod " + Pregled_haljine_Naziv.Text + " u veličini M trenutno nije dostupan\n:(");
                Pregled_haljine_dodaj.Enabled = false;
            }
            dr.Close();
            cn.Close();
        }

        private void Pregled_haljine_velicinaL_Click(object sender, EventArgs e)
        {
            Pregled_haljine_dodaj.Enabled = true;
            cn.Open();
            cm = new MySqlCommand("select L from artikli where Artikli_naziv like '%" + Pregled_haljine_Naziv.Text + "%' ", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                label2.Visible = true;
                label2.Text = dr["L"].ToString();
            }
            if (label2.Text == "0")
            {
                MessageBox.Show("Nažalost proizvod " + Pregled_haljine_Naziv.Text + " u veličini L trenutno nije dostupan\n:(");
                Pregled_haljine_dodaj.Enabled = false;
            }
            dr.Close();
            cn.Close();
        }

        private void Pregled_haljine_nazad_Click(object sender, EventArgs e)
        {
            this.Hide();
            Haljine haljine = new Haljine();
            haljine.Show();
        }
    }
}


