using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace end_of_day {
    public partial class add_customer_form : Form {
        public add_customer_form() {
            InitializeComponent();
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e) {
            base.OnClosing(e);
            // ensure's that list is cleared when form closes so that when form reopens the old list is not still stored in listbox
            String postdata = "&addCustomer=true";
            postdata += "&firstname=" + textBox1.Text;
            postdata += "&lastname=" + textBox2.Text;
            postdata += "&email=" + textBox3.Text;
            postdata += "&street1=" + textBox4.Text;
            postdata += "&street2=" + textBox5.Text;
            postdata += "&city=" + textBox6.Text;
            postdata += "&postcode=" + textBox7.Text + (!String.IsNullOrWhiteSpace(textBox11.Text) ? "-" + textBox11.Text : "-0000");
            postdata += "&telephone=" + textBox8.Text + "-" + textBox9.Text+ "-" + textBox10.Text;

            Form1 o = (Form1)this.Owner;
            o.Show();
            o.sendPost(postdata);
            //textBox1.Clear();
            //textBox2.Clear();
           // textBox3.Clear();
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }


    }
}
