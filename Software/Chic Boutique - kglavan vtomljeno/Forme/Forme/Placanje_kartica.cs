using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Braintree;
using System.Web;
using MySql.Data.MySqlClient;
using CefSharp;
using CefSharp.WinForms;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace PI___INVENTura___kglavan_vtomljeno
{
    public partial class Placanje_kartica : Form
    {
        public ChromiumWebBrowser chromeBrowser;
        string nonce;
        Thread Serv;
        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        public static string nazivhlacenext, taghlacenext;
        private Button btn;
        public static string nacin;
        //HttpListener listener = new HttpListener();
        public Placanje_kartica()
        {
            //uzmi_id = data;
            InitializeComponent();
            var clientToken = gateway.ClientToken.Generate(
            new ClientTokenRequest
            {

            }
            );
            InitializeChromium(clientToken);
            cn = new MySqlConnection();
            cn.ConnectionString = "server=localhost; user id=root; password=; database=probaslika2";
        }

        public static BraintreeGateway gateway = new BraintreeGateway
        {
            Environment = Braintree.Environment.SANDBOX,
            MerchantId = "nxzdb5pj7dhzf92b",
            PublicKey = "qjt38fk4sgrnmtbd",
            PrivateKey = "1f64e33c6b80632d3ce0c02a3a91d633"
        };

        public void InitializeChromium(string token)
        {
            if (!Cef.IsInitialized)
            {
                CefSettings settings = new CefSettings();
                // Initialize cef with the provided settings
                Cef.Initialize(settings);
            }

            // Create a browser component
            chromeBrowser = new ChromiumWebBrowser("http://localhost/PI_ChicBoutique/index.php?token=" + token);
            // Add it to the form and fill it to the form window.
            this.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;
        }

        public void server()
        {
            Thread.Sleep(3000);
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add($"http://localhost:8000/");
            listener.Start();
            Console.WriteLine("Listening...");
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;
            string documentContents;
            using (Stream receiveStream = request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    documentContents = readStream.ReadToEnd();
                }
            }
            nonce = documentContents;
            string[] noncesplit = nonce.Split('=');
            nonce = noncesplit[1];
            var Trrequest = new TransactionRequest
            {
                Amount = 10.00M,
                PaymentMethodNonce = nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };
            Result<Transaction> result = gateway.Transaction.Sale(Trrequest);
            if (result.IsSuccess())
            {
                string responseString = "<HTML><BODY> Payment successful! Returning to home page...</BODY></HTML>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
                Thread.Sleep(1000);
                cn.Open();

                foreach (var item in shareddata.Items)
                {
                    if (item.Size == "S")
                    {
                        cm = new MySqlCommand("update artikli set S = S- '" + item.Quantity + "' where Artikli_naziv = '" + item.Item + "' ", cn);
                        cm.ExecuteNonQuery();
                    }
                    else if (item.Size == "M")
                    {
                        cm = new MySqlCommand("update artikli set M = M-'" + item.Quantity + "' where Artikli_naziv = '" + item.Item + "' ", cn);
                        cm.ExecuteNonQuery();
                    }
                    else if (item.Size == "L")
                    {
                        cm = new MySqlCommand("update artikli set L = L-'" + item.Quantity + "' where Artikli_naziv = '" + item.Item + "' ", cn);
                        cm.ExecuteNonQuery();
                    }

                }
                chromeBrowser.Delete();
                listener.Stop();
                listener.Close();

                if (InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        Form hvala = new Hvala();
                        this.Hide();
                        this.Close();
                        Serv.Abort();
                        hvala.ShowDialog();
                        Izbor_placanja izbor_placanja = new Izbor_placanja();
                        Kosarica kosarica = new Kosarica();
                        kosarica.Kosarica_dataGridView1.Rows.Clear();
                    }));
                }
                else
                {
                    Form nakonprijave = new nakonprijave();
                    this.Hide();
                    this.Close();
                    //nakonprijave.Show();
                    chromeBrowser.Delete();
                    listener.Stop();
                    listener.Close();
                }
            }
            chromeBrowser.Delete();
            listener.Stop();
            listener.Close();
        }

        private void Placanje_kartica_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Placanje_kartica_Load_1(object sender, EventArgs e)
        {

        }

        private void Placanje_kartica_Load_2(object sender, EventArgs e)
        {
            Serv = new Thread(server);
            Serv.Start();
        }
        
    }
}






