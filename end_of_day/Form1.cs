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
using System.Net.Mail;
using System.IO;
using System.Threading;
using System.Diagnostics;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Reflection;
using System.Net;
using System.Drawing.Imaging;
using System.Collections;
using System.Web;
using HtmlAgilityPack;
using System.Security.Cryptography;
using System.Collections.Specialized;
using System.Configuration;

namespace end_of_day {





    public partial class Form1 : Form {

        //private add_customer_form childForm = new add_customer_form(this);

        public Form1() {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) => {
                string resourceName = new AssemblyName(args.Name).Name + ".dll";
                string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource)) {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };
            InitializeComponent();
            this.Resize += new EventHandler(textBox1_TextChanged);


            //this.FormBorderStyle = FormBorderStyle.None;
            //this.DoubleBuffered = true;
            //this.SetStyle(ControlStyles.ResizeRedraw, true); 

        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            webBrowser1.Width = this.Width - 20;
            webBrowser1.Height = this.flowLayoutPanel2.Height;
        }




        private const int cGrip = 16;      // Grip size 
        private const int cCaption = 25;   // Caption bar height; 

        protected override void OnPaint(PaintEventArgs e) {
            //System.Drawing.Rectangle rc = new System.Drawing.Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip); 
            //ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc); 
            //rc = new System.Drawing.Rectangle(0, 0, this.ClientSize.Width, 32); 
            //e.Graphics.FillRectangle(Brushes.DarkBlue, rc); 
        }

        protected override void WndProc(ref Message m) {
            if (m.Msg == 0x84) {  // Trap WM_NCHITTEST 
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption) {
                    m.Result = (IntPtr)2;  // HTCAPTION 
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip) {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT 
                    return;
                }
            }
            base.WndProc(ref m);
        }





        private void button4_Click(object sender, EventArgs e) {
            SqlConnection myConnection = quries.create_concection();

            DialogResult dlgRes = MessageBox.Show("Exculde Videos?",
                 "Options",
                MessageBoxButtons.YesNoCancel,
                 MessageBoxIcon.Question);

            String SQL_op = "";
            if (dlgRes == DialogResult.Yes) {
                SQL_op = " AND NOT Dept_ID = 'video'";
            }

            String mess = "done";
            try {
                //This is the absolute path to the PDF that we will create 

                iTextSharp.text.Font fontTinyReg = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font fontTinyBold = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font header = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, BaseColor.WHITE);

                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

                iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 8);
                String date = DateTime.Now.ToShortDateString().Replace('\\', '-').Replace('/', '-');
                string outputFile = @"outofstock_" + date + ".pdf";//Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Sample.pdf"); 
                int i = 0;
                //Create a standard .Net FileStream for the file, setting various flags 
                using (FileStream fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.None)) {
                    //Create a new PDF document setting the size to A4 
                    using (Document doc = new Document(PageSize.A8)) {
                        //Bind the PDF document to the FileStream using an iTextSharp PdfWriter 
                        using (PdfWriter w = PdfWriter.GetInstance(doc, fs)) {
                            //Open the document for writing 
                            doc.Open();
                            doc.SetMargins(0f, 0f, 0f, 0f);
                            doc.SetMarginMirroring(false);

                            //Create a table with two columns 
                            PdfPTable t = new PdfPTable(3);
                            t.WidthPercentage = 100;
                            //fix the absolute width of the table
                            //t.LockedWidth = true;
                            var colWidthPercentages = new[] { 60f, 30f, 10f };
                            t.SetWidths(colWidthPercentages);

                            t.HorizontalAlignment = 0;
                            t.SkipFirstHeader = true;
                            //Borders are drawn by the individual cells, not the table itself. 
                            //Tell the default cell that we do not want a border drawn 
                            t.DefaultCell.Border = 1;
                            t.DefaultCell.Phrase = new Phrase() { Font = times };

                            PdfPCell theCell = new PdfPCell(new Paragraph("name", fontTinyBold));
                            t.AddCell(theCell);
                            theCell = new PdfPCell(new Paragraph("sku", fontTinyBold));
                            t.AddCell(theCell);
                            theCell = new PdfPCell(new Paragraph("stock", fontTinyBold));
                            t.AddCell(theCell);

                            SqlDataReader myReader = null;
                            SqlCommand myCommand = new SqlCommand("SELECT Dept_ID,Description FROM Departments WHERE NOT Dept_ID = 'discount' " + SQL_op + "",
                                         myConnection);
                            myReader = myCommand.ExecuteReader();
                            List<string[]> list = new List<string[]>();


                            while (myReader.Read()) {
                                list.Add(new string[] { myReader["Dept_ID"].ToString().ToUpper(), myReader["Description"].ToString() });
                            }
                            myReader.Close();

                            i = 0;
                            int all_count = 0;
                            foreach (string[] item in list) {

                                PdfPCell cell = new PdfPCell(new Paragraph(item[1].ToString(), header));
                                cell.BackgroundColor = BaseColor.BLACK;
                                cell.Colspan = 3;
                                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                                t.AddCell(cell);

                                myCommand = new SqlCommand("SELECT ItemName,In_Stock,Vendor_Part_Num FROM Inventory WHERE In_Stock<1 AND IsDeleted = 'False' AND Dept_ID = '" + item[0] + "' ORDER BY Last_Sold",
                                                                         myConnection);
                                myReader = myCommand.ExecuteReader();

                                int stock_count = 0;
                                int j = 0;
                                while (myReader.Read()) {
                                    theCell = new PdfPCell(new Paragraph(myReader["ItemName"].ToString(), fontTinyReg));
                                    t.AddCell(theCell);
                                    theCell = new PdfPCell(new Paragraph(myReader["Vendor_Part_Num"].ToString(), fontTinyBold));
                                    t.AddCell(theCell);
                                    theCell = new PdfPCell(new Paragraph(myReader["In_Stock"].ToString(), fontTinyReg));
                                    t.AddCell(theCell);

                                    stock_count = stock_count + int.Parse(myReader["In_Stock"].ToString());
                                    j++;
                                    i++;
                                }
                                myReader.Close();

                                cell = new PdfPCell(new Paragraph("Totals (products:" + j + ") --  " + stock_count + " items", header));
                                cell.BackgroundColor = BaseColor.GRAY;
                                cell.Colspan = 3;
                                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                                t.AddCell(cell);

                                all_count = all_count + stock_count;

                            }

                            PdfPCell fcell = new PdfPCell(new Paragraph("Totals (products:" + i + ") --  " + all_count + " items", header));
                            fcell.BackgroundColor = BaseColor.GRAY;
                            fcell.Colspan = 3;
                            fcell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                            t.AddCell(fcell);

                            //Add the table to our document 
                            doc.Add(t);

                            //Close our document 
                            doc.Close();
                        }
                    }
                }

                Process mydoc = new Process();
                mydoc.StartInfo.FileName = @"outofstock_" + date + ".pdf";
                mydoc.Start();

                mess = "done";
                dlgRes = MessageBox.Show("Had " + i + "items including: \r\n" + mess + "\r\n\r\nEmail this?",
                 "Confirm Document Close",
                MessageBoxButtons.YesNoCancel,
                 MessageBoxIcon.Question);


                if (dlgRes == DialogResult.Yes) {
                    MailMessage theMailMessage = new MailMessage("jeremybass26@gmail.com", "jeremybass@cableone.net");
                    theMailMessage.Body = "body email message here \r\n MESSAGE \r\n" + mess;

                    theMailMessage.Attachments.Add(new Attachment("outofstock_" + date + ".pdf"));
                    theMailMessage.Subject = "Subject here";

                    SmtpClient theClient = new SmtpClient("smtp.gmail.com", 587);
                    theClient.EnableSsl = true;
                    theClient.UseDefaultCredentials = false;
                    theClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    theClient.Credentials = new System.Net.NetworkCredential("jeremybass26@gmail.com", "bA03s17s82!");
                    theClient.Send(theMailMessage);
                }














            } catch (Exception er) {

                dlgRes = MessageBox.Show(er.ToString(), "Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);


            }
        }

        private void button3_Click(object sender, EventArgs e) {
            SqlConnection myConnection = quries.create_concection();

            DialogResult dlgRes = MessageBox.Show("Exculde Videos?",
                 "Options",
                MessageBoxButtons.YesNoCancel,
                 MessageBoxIcon.Question);

            String SQL_op = "";
            if (dlgRes == DialogResult.Yes) {
                SQL_op = " AND NOT Dept_ID = 'video'";
            }

            String mess = "done";
            try {
                //This is the absolute path to the PDF that we will create 

                iTextSharp.text.Font fontTinyReg = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font fontTinyBold = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font header = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD, BaseColor.WHITE);

                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

                iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 8);
                String date = DateTime.Now.ToShortDateString().Replace('\\', '-').Replace('/', '-');
                string outputFile = @"instock_" + date + ".pdf";//Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Sample.pdf"); 
                int i = 0;
                //Create a standard .Net FileStream for the file, setting various flags 
                using (FileStream fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.None)) {
                    //Create a new PDF document setting the size to A4 
                    using (Document doc = new Document(PageSize.A8)) {
                        //Bind the PDF document to the FileStream using an iTextSharp PdfWriter 
                        using (PdfWriter w = PdfWriter.GetInstance(doc, fs)) {
                            //Open the document for writing 
                            doc.Open();
                            doc.SetMargins(0f, 0f, 0f, 0f);
                            doc.SetMarginMirroring(false);

                            //Create a table with two columns 
                            PdfPTable t = new PdfPTable(3);
                            t.WidthPercentage = 100;
                            //fix the absolute width of the table
                            //t.LockedWidth = true;
                            var colWidthPercentages = new[] { 60f, 30f, 10f };
                            t.SetWidths(colWidthPercentages);

                            t.HorizontalAlignment = 0;
                            t.SkipFirstHeader = true;
                            //Borders are drawn by the individual cells, not the table itself. 
                            //Tell the default cell that we do not want a border drawn 
                            t.DefaultCell.Border = 1;
                            t.DefaultCell.Phrase = new Phrase() { Font = times };

                            PdfPCell theCell = new PdfPCell(new Paragraph("name", fontTinyBold));
                            t.AddCell(theCell);
                            theCell = new PdfPCell(new Paragraph("sku", fontTinyBold));
                            t.AddCell(theCell);
                            theCell = new PdfPCell(new Paragraph("stock", fontTinyBold));
                            t.AddCell(theCell);

                            SqlDataReader myReader = null;
                            SqlCommand myCommand = new SqlCommand("SELECT Dept_ID,Description FROM Departments WHERE NOT Dept_ID = 'discount' " + SQL_op + "",
                                         myConnection);
                            myReader = myCommand.ExecuteReader();
                            List<string[]> list = new List<string[]>();


                            while (myReader.Read()) {
                                list.Add(new string[] { myReader["Dept_ID"].ToString().ToUpper(), myReader["Description"].ToString() });
                            }
                            myReader.Close();

                            i = 0;
                            int all_count = 0;
                            foreach (string[] item in list) {

                                myCommand = new SqlCommand("SELECT ItemName,In_Stock,Vendor_Part_Num FROM Inventory WHERE In_Stock>0 AND IsDeleted = 'False' AND Dept_ID = '" + item[0] + "' ORDER BY Last_Sold",
                                                                         myConnection);
                                myReader = myCommand.ExecuteReader();

                                PdfPCell cell = new PdfPCell(new Paragraph(item[1].ToString(), header));
                                cell.BackgroundColor = BaseColor.BLACK;
                                cell.Colspan = 3;
                                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                                t.AddCell(cell);

                                int stock_count = 0;
                                int j = 0;
                                while (myReader.Read()) {
                                    theCell = new PdfPCell(new Paragraph(myReader["ItemName"].ToString(), fontTinyReg));
                                    t.AddCell(theCell);
                                    theCell = new PdfPCell(new Paragraph(myReader["Vendor_Part_Num"].ToString(), fontTinyBold));
                                    t.AddCell(theCell);
                                    theCell = new PdfPCell(new Paragraph(myReader["In_Stock"].ToString(), fontTinyReg));
                                    t.AddCell(theCell);

                                    try {
                                        stock_count = stock_count + int.Parse(myReader["In_Stock"].ToString());
                                    } catch (Exception er) {
                                        string count_er_mess = "This item " + myReader["ItemName"].ToString() + " has a qty that is not correct.  Write this down sku (" + myReader["Vendor_Part_Num"].ToString() + ") and address it before starting the inventory " + myReader["In_Stock"].ToString() + "\r\rSKU: " + myReader["Vendor_Part_Num"].ToString() + "\rStock:" + myReader["In_Stock"].ToString();
                                        dlgRes = MessageBox.Show(count_er_mess + "\r\r\r [" + er.ToString() + "]", "Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                    }
                                    j++;
                                    i++;
                                }
                                myReader.Close();

                                cell = new PdfPCell(new Paragraph("Totals (products:" + j + ") --  " + stock_count + " items", header));
                                cell.BackgroundColor = BaseColor.GRAY;
                                cell.Colspan = 3;
                                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                                t.AddCell(cell);

                                all_count = all_count + stock_count;

                            }


                            PdfPCell fcell = new PdfPCell(new Paragraph("Totals (products:" + i + ") --  " + all_count + " items", header));
                            fcell.BackgroundColor = BaseColor.GRAY;
                            fcell.Colspan = 3;
                            fcell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                            t.AddCell(fcell);
                            //Add the table to our document 
                            doc.Add(t);

                            //Close our document 
                            doc.Close();
                        }
                    }
                }







                Process mydoc = new Process();
                mydoc.StartInfo.FileName = @"instock_" + date + ".pdf";
                mydoc.Start();

                mess = "done";
                dlgRes = MessageBox.Show("Had " + i + "items including: \r\n" + mess + "\r\n\r\nEmail this?",
                 "Confirm Document Close",
                MessageBoxButtons.YesNoCancel,
                 MessageBoxIcon.Question);


                if (dlgRes == DialogResult.Yes) {
                    MailMessage theMailMessage = new MailMessage("jeremybass26@gmail.com", "jeremybass@cableone.net");
                    theMailMessage.Body = "body email message here \r\n MESSAGE \r\n" + mess;

                    theMailMessage.Attachments.Add(new Attachment("instock_" + date + ".pdf"));
                    theMailMessage.Subject = "Subject here";

                    SmtpClient theClient = new SmtpClient("smtp.gmail.com", 587);
                    theClient.EnableSsl = true;
                    theClient.UseDefaultCredentials = false;
                    theClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    String username = ConfigurationManager.AppSettings["email_username"];
                    String pass = ConfigurationManager.AppSettings["email_pass"];
                    theClient.Credentials = new System.Net.NetworkCredential(username, pass);
                    theClient.Send(theMailMessage);
                }















            } catch (Exception er) {

                dlgRes = MessageBox.Show(er.ToString(), "Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);


            }
        }

        private void button2_Click(object sender, EventArgs e) {
            SqlConnection myConnection = quries.create_concection();
            //images/small/
            SqlDataReader myReader = null;


            DialogResult dlgRes = MessageBox.Show("Exculde Videos?",
                 "Options",
                MessageBoxButtons.YesNoCancel,
                 MessageBoxIcon.Question);

            String SQL_op = "";
            if (dlgRes == DialogResult.Yes) {
                SQL_op = "AND NOT Dept_ID = 'video'";
            }
            SqlCommand myCommand = new SqlCommand("SELECT Vendor_Part_Num,Picture FROM Inventory",
                                                     myConnection);
            myReader = myCommand.ExecuteReader();
            List<string[]> list = new List<string[]>();


            while (myReader.Read()) {
                list.Add(new string[] { myReader["Vendor_Part_Num"].ToString().ToUpper(), myReader["Picture"].ToString() });
            }
            myReader.Close();


            int i = 0;
            foreach (string[] item in list) {
                String SKU = item[0].ToString().ToUpper();

                if (String.IsNullOrWhiteSpace(item[1].ToString()) && !String.IsNullOrWhiteSpace(SKU)) {
                    string destination = "ftp://aphrodite.eldorado.net/images/small/";

                    string file = SKU + ".jpg";
                    string extention = Path.GetExtension(file);
                    string fileName = file.Remove(file.Length - extention.Length);
                    string fileNameCopy = fileName;

                    if (CheckFileExists(GetRequest(destination + "//" + fileNameCopy + extention))) {
                        var filePath = "ftp://aphrodite.eldorado.net/images/small/" + SKU + ".jpg";
                        var request = WebRequest.Create(filePath);
                        request.Credentials = new NetworkCredential("39Ple", "428ure");
                        using (var response = request.GetResponse())
                        using (var stream = response.GetResponseStream())
                        using (var img = System.Drawing.Image.FromStream(stream)) {
                            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                            String local_file = path + "\\product_imgs\\" + SKU + ".jpg";
                            img.Save(local_file, ImageFormat.Jpeg);
                            SqlCommand insertCommand = new SqlCommand("UPDATE Inventory SET Picture = '" + local_file + "' WHERE Vendor_Part_Num = '" + SKU + "'", myConnection);
                            insertCommand.ExecuteNonQuery();
                            i++;
                        }
                    }
                }
            }
        }

        private static FtpWebRequest GetRequest(string uriString) {
            var request = (FtpWebRequest)WebRequest.Create(uriString);
            request.Credentials = new NetworkCredential("39Ple", "428ure");
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            return request;
        }

        private static bool CheckFileExists(FtpWebRequest request) {
            try {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return true;
            } catch (WebException ex) {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable) {
                    return false;
                } else {
                    return false;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e) {
            DialogResult dlgRes = MessageBox.Show("add user?",
                 "Options",
                MessageBoxButtons.YesNoCancel,
                 MessageBoxIcon.Question);
            if (dlgRes == DialogResult.Yes) {
                this.Hide();
                add_customer_form childForm = new add_customer_form();
                childForm.Show(this);
            }

            if (dlgRes == DialogResult.No) {
                var myValue = Microsoft.VisualBasic.Interaction.InputBox("What is the sku of the itme you wish to find", "Look product", "");
                if (myValue != "") {
                    sendPost("&sku=" + myValue);
                }
            }
        }

        public void sendPost(String postData) {
            //step 1 talk with site
            WebRequest req = WebRequest.Create("http://adultpleasures.xxx/quick_look.php");
            String username = ConfigurationManager.AppSettings["restConStr_username"];
            String pass = ConfigurationManager.AppSettings["restConStr_pass"];
            string MainPostData = "username=" + username + "&pass=" + pass;

            byte[] send = Encoding.Default.GetBytes(MainPostData + (!String.IsNullOrWhiteSpace(postData) ? "&" + postData.TrimStart('&') : ""));
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = send.Length;

            Stream sout = req.GetRequestStream();
            sout.Write(send, 0, send.Length);
            sout.Flush();
            sout.Close();

            WebResponse res = req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream());
            string returnvalue = sr.ReadToEnd();
            HtmlAgilityPack.HtmlDocument hDoc = new HtmlAgilityPack.HtmlDocument();

            webBrowser1.Navigate("about:blank");
            webBrowser1.Document.OpenNew(true);
            webBrowser1.Document.Write("<html><body>" + returnvalue + "</body></html>");
            webBrowser1.Stop();
        }



    }
}
