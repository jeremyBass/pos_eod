using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Web;


namespace end_of_day
{
    public class quries
    {
        public static SqlConnection create_concection()
        {
            Boolean hasConection =true;
            SqlConnection myConnection = new SqlConnection(
                "Data Source=HTPOS-RBRAIN\\PCAMERICA;" +
                "Trusted_Connection=true;" +

                "Initial Catalog=harbortouch; " +
                "connection timeout=30"
            );
            try
            {
                myConnection.Open();
            }
            catch
            {
                try
                {
                    myConnection = new SqlConnection(
                            "Data Source=localhost\\SqlExpress;" +
                            "Trusted_Connection=true;" +

                            "Initial Catalog=harbortouch; " +
                            "connection timeout=30"
                        );
                    myConnection.Open();
                }
                catch (Exception er)
                {
                    hasConection = false;
                    DialogResult dlgRes = MessageBox.Show(er.ToString(), "Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                }


            }
            if (hasConection) {
                //Form1 form = new Form1();
                //form.MyMessage = "made it"; 
            }
            return myConnection;
        }


        static void Main(SqlConnection db)
        {
            
        }
    }
}
