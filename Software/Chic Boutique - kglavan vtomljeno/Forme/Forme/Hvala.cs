using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using ceTe.DynamicPDF.PageElements.BarCoding;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace PI___INVENTura___kglavan_vtomljeno
{
    public partial class Hvala : Form
    {
        public string pomocna = Prijava.welcomeuser;
        public Hvala()
        {
            InitializeComponent();
            Placanje_kartica.nacin = "Karticno placanje";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            nakonprijave nakonprijave = new nakonprijave();
            nakonprijave.Show();
        }

        private void Hvala_Load(object sender, EventArgs e)
        {
            StvoriPDF();
            SendPDF();
        }

        public static void StvoriPDF()
        {

            Document document = new Document();
            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page);
            ceTe.DynamicPDF.PageElements.Label label1 = new ceTe.DynamicPDF.PageElements.Label("Chic Boutique", 0, 0, 504, 100, ceTe.DynamicPDF.Font.TimesBoldItalic, 36, TextAlign.Center);
            ceTe.DynamicPDF.PageElements.Label label2 = new ceTe.DynamicPDF.PageElements.Label("vam se zahvaljuje na kupnji", 0, 35, 504, 100, ceTe.DynamicPDF.Font.TimesBoldItalic, 18, TextAlign.Center);
            ceTe.DynamicPDF.PageElements.Label label3 = new ceTe.DynamicPDF.PageElements.Label("Korisnik: " + Prijava.welcomeuser, 0, 60, 504, 100, ceTe.DynamicPDF.Font.HelveticaBold, 12, TextAlign.Center);
            QrCode qrCode = new QrCode("www.github.com/foivz/pi22-vtomljeno-kglavan/wiki", 1, 1);
            ceTe.DynamicPDF.PageElements.Label label4 = new ceTe.DynamicPDF.PageElements.Label("Nacin placanja: " + Placanje_kartica.nacin, 0, 120, 504, 100, ceTe.DynamicPDF.Font.HelveticaBold, 12, TextAlign.Left);
            ceTe.DynamicPDF.PageElements.Label label5 = new ceTe.DynamicPDF.PageElements.Label("Ukupno naplaceno: " + Kosarica.sum, 0, 120, 504, 100, ceTe.DynamicPDF.Font.HelveticaBold, 12, TextAlign.Right);
            ceTe.DynamicPDF.PageElements.Table2 table2 = new ceTe.DynamicPDF.PageElements.Table2(0, 140, 600, 600);
            table2.Columns.Add(180);
            table2.Columns.Add(60);
            table2.Columns.Add(60);
            table2.Columns.Add(80);
            table2.Columns.Add(90);
            ceTe.DynamicPDF.PageElements.Row2 row1 = table2.Rows.Add(35, ceTe.DynamicPDF.Font.HelveticaBold, 12, Grayscale.Black,
           Grayscale.Gray);
            row1.Cells.Add("Artikl");
            row1.Cells.Add("Velicina");
            row1.Cells.Add("Kolicina");
            row1.Cells.Add("Cijena");
            row1.Cells.Add("Ukupno");
            foreach (var item in shareddata.Items)
            {
                ceTe.DynamicPDF.PageElements.Row2 row2 = table2.Rows.Add(25, ceTe.DynamicPDF.Font.HelveticaBold, 12, Grayscale.Black,
           Grayscale.Gray);
                row2.Cells.Add(item.item);
                row2.Cells.Add(item.size);
                row2.Cells.Add(item.qty.ToString());
                row2.Cells.Add(item.price.ToString());
                row2.Cells.Add(item.total.ToString());
            }
            page.Elements.Add(table2);
            page.Elements.Add(qrCode);
            page.Elements.Add(label1);
            page.Elements.Add(label2);
            page.Elements.Add(label3);
            page.Elements.Add(label4);
            page.Elements.Add(label5);
            document.Draw("C:\\Users\\tomlj\\Desktop\\ChicBoutique_Racun.pdf");
        }
        private void SendPDF()
        {
            Prijava prijava = new Prijava();
            string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            SqlCommand cmd = new SqlCommand("select Uemail from Registracija_korisnika where Uname = '" + pomocna + "'", sqlconn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string adresa = reader[0].ToString();
                var fromAddr = new MailAddress("chic.boutiquePI@gmail.com", "Chic Boutique Receipt");
                var toAddr = new MailAddress(adresa, "");
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
                mm.Attachments.Add(new Attachment(@"C:\\Users\\tomlj\\Desktop\\ChicBoutique_Racun.pdf"));
                mm.Subject = "Račun";
                mm.Body = "Chic Boutique Vam se zahvaljuje na povjerenju i na uspješnoj kupnji.\nU nastavku slijedi Vaš račun:";
                mm.From = new MailAddress(fromAddr.ToString());
                mm.To.Add(toAddr);
                {
                    smtp.Send(mm);
                }

            }
        }
    }
}
