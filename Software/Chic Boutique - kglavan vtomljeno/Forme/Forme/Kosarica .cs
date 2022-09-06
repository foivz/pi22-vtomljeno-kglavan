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
    public partial class Kosarica : Form
    {
        MySqlConnection cn, cn2;
        MySqlCommand cm, cm2;
        MySqlDataReader dr, dr2;
        public DataGridView grid2;
        public PictureBox pic;
        private Pregled_majice probamajicenext;
        public Label proba;
        public static Kosarica instance;
        public static decimal sum;
        public Kosarica()
        {
            InitializeComponent();
            instance = this;
            
            Kosarice_panel2.BackColor = Color.FromArgb(255, 215, 114);
            //BindingListView<shareddata> view = new BindingListView<shareddata>;
            Kosarica_dataGridView1.DataSource = shareddata.Items;
            Kosarica_artikli_majice.BackColor = Color.FromArgb(255, 215, 114);
            Kosarica_artikli_hlace.BackColor = Color.FromArgb(255, 215, 114);
            Kosarica_artikli_haljine.BackColor = Color.FromArgb(255, 215, 114);
            Kosarica_artikli_obuca.BackColor = Color.FromArgb(255, 215, 114);
            Kosarica_dataGridView1.GridColor = Color.FromArgb(255, 215, 114);
            Kosarica_ukloni.BackColor = Color.FromArgb(255, 215, 114);
            Kosarica_isprazni.BackColor = Color.FromArgb(255, 215, 114);
            Kosarica_placanje.BackColor = Color.FromArgb(255, 215, 114);
            Kosarica_dataGridView1.BackgroundColor = Color.FromArgb(225, 205, 124);
            Kosarica_dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            cn = new MySqlConnection();
            cn.ConnectionString = "server=localhost; user id=root; password=; database=probaslika";
            label1.Visible = false;
        }

        private void kosarica_Load(object sender, EventArgs e)
        {
            Kosarica_dataGridView1.DataSource = shareddata.Items;
            sum = 0;
            
            for (int i = 0; i < Kosarica_dataGridView1.Rows.Count; ++i)
            {
                sum += Convert.ToDecimal(Kosarica_dataGridView1.Rows[i].Cells[4].Value);
            }
            Kosarica_Ukunpo_za_platiti.Text = "Ukupno za platiti: " + sum.ToString() + " HRK";
            
            decimal proba = sum;
            if(Prijava.welcomeuser == " ")
            {
                label1.Visible = true;
            }
        }

        private void Kosarica_home_Click(object sender, EventArgs e)
        {
            this.Hide();
            nakonprijave nakonprijave = new nakonprijave();
            nakonprijave.Show();
        }

        private void Kosarica_artikli_Click(object sender, EventArgs e)
        {
            if (Kosarica_panel1.Height == 208)
            {
                Kosarica_panel1.Height = 44;
            }
            else
            {
                Kosarica_panel1.Height = 208;
            }
        }

        private void Kosarica_artikli_majice_Click(object sender, EventArgs e)
        {
            this.Hide();
            Majice majice = new Majice();
            majice.Show();
        }

        private void Kosarica_artikli_hlace_Click(object sender, EventArgs e)
        {
            this.Hide();
            Hlace hlace = new Hlace();
            hlace.Show();
        }

        private void Kosarica_artikli_haljine_Click(object sender, EventArgs e)
        {
            this.Hide();
            Haljine haljine = new Haljine();
            haljine.Show();
        }

        private void Kosarica_artikli_obuca_Click(object sender, EventArgs e)
        {
            this.Hide();
            Obuca obuca = new Obuca();
            obuca.Show();
        }

        private void Kosarica_kosarica_Click(object sender, EventArgs e)
        {
            this.Hide();
            Kosarica kosarica = new Kosarica();
            kosarica.Show();
        }

        private void Kosarica_odjava_Click(object sender, EventArgs e)
        {
            this.Hide();
            Prijava prijava = new Prijava();
            prijava.Show();
        }

        private void Kosarica_pomoc_MouseHover(object sender, EventArgs e)
        {
            Kosarica_toolTip1.IsBalloon = false;
            Kosarica_toolTip1.ToolTipIcon = ToolTipIcon.Info;
            Kosarica_toolTip1.Show("Ovdje se nalazi prikaz trenutnog stanja Vaše košarice! Ona može biti dupkom puna, polu puna, polu prazna ili u potupnosti prazna! Red je na Vama da je organizirate prema vlastitim željama s našim artiklima.\nOmogućeno Vam je uklanjanje određenog artikla ili pražnjenje cijele košarice.", Kosarica_pomoc);
            Kosarica_toolTip1.InitialDelay = 200;
            Kosarica_toolTip1.ReshowDelay = 100;
        }

        private void Kosarica_ukloni_Click(object sender, EventArgs e)
        {
            if (Kosarica_dataGridView1.Rows.Count <= 1)
            {
                MessageBox.Show("Vaša košarica je trenutno prazna!\nPosjetite stranicu artikala i odaberite željeni!");
            }
            else
            {
                int rowIndex = Kosarica_dataGridView1.CurrentCell.RowIndex;
                Kosarica_dataGridView1.Rows.RemoveAt(rowIndex);
                decimal sum = 0;
                for (int i = 0; i < Kosarica_dataGridView1.Rows.Count; ++i)
                {
                    sum += Convert.ToInt32(Kosarica_dataGridView1.Rows[i].Cells[4].Value);
                }
                Kosarica_Ukunpo_za_platiti.Text = "Ukupno za platiti: " + sum.ToString() + " HRK";
            }
        }

        private void Kosarica_isprazni_Click(object sender, EventArgs e)
        {
            if (Kosarica_dataGridView1.Rows.Count <= 1)
            {
                MessageBox.Show("Vaša košarica je trenutno prazna!\nPosjetite stranicu artikala i odaberite željeni!");
            }
            else
            {
                Kosarica_dataGridView1.Rows.Clear();
                decimal sum = 0;
                
                for (int i = 0; i < Kosarica_dataGridView1.Rows.Count; ++i)
                {
                    sum += Convert.ToInt32(Kosarica_dataGridView1.Rows[i].Cells[4].Value);
                    
                }
                Kosarica_Ukunpo_za_platiti.Text = "Ukupno za platiti: " + sum.ToString() + " HRK";
                decimal proba = sum;
            }
        }

        private void Kosarica_placanje_Click(object sender, EventArgs e)
        {

            if (Kosarica_dataGridView1.Rows.Count <= 1)
            {
                MessageBox.Show("Vaša košarica je trenutno prazna!\nPosjetite stranicu artikala i odaberite željeni!");
            }
            else if (Prijava.welcomeuser == " ")
            {
                Kosarica_placanje.Enabled = false;
                MessageBox.Show("Prijavljeni ste kao Guest\nIzradite račun ili se prijavte za nastavak kupovine i plaćanje!");
            }
            else
            {
                this.Hide();
                Izbor_placanja izbor_placanja = new Izbor_placanja();
                izbor_placanja.Show();
            }
        }
    }
}
