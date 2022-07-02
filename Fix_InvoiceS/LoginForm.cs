using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fix_InvoiceS
{

    public partial class LoginForm : Form
    {

        public string username;

        public LoginForm()
        {
            InitializeComponent();
            string host = Dns.GetHostName();
            IPHostEntry ip = Dns.GetHostEntry(host);
            string CIP = (ip.AddressList[0].ToString());
            string pubIp = new System.Net.WebClient().DownloadString("https://api.ipify.org");
            SqlConnection con_log = null;
            con_log = new SqlConnection(ConfigurationManager.ConnectionStrings["Log_S"].ConnectionString);
            //con_log.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port;Initial Catalog=dbname;Persist Security Info=True;User ID=dbuser;Password=passphrase";
            string query = "insert into [dbo].[LogInfo]([Int_IP_Address],[Ext_IP_Address],[Action]) values('" + CIP + "','" + pubIp + "','"  + "Application Started')";
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                con_log.Open();
                adapter.InsertCommand = new SqlCommand(query, con_log);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());

            }
            con_log.Close();
            con_log.Dispose();

            

        }

        private int _failedAttempts = 0;
        private int _is_locked;


        private void button1_Click(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port;Initial Catalog=dbname;Persist Security Info=True;User ID=dbuser;Password=passphrase";
            SqlConnection con_log = null;
            con_log = new SqlConnection(ConfigurationManager.ConnectionStrings["Log_S"].ConnectionString);
            con_log.Open();
            username = textBox1.Text;
            string password = textBox2.Text;
            SqlCommand cmd = new SqlCommand("select LoginName,PasswordHash,Locked from [dbo].[Users] where LoginName='" + username + "'and PasswordHash=HASHBYTES('SHA2_512',N'" + password + "')", con_log);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                int is_locked = Int32.Parse(dt.Rows[0]["Locked"].ToString());
                if (is_locked == 0)
                {
                    MessageBox.Show("Login Success! Please Continue... ");
                    //System.Diagnostics.Process.Start("");
                    string host = Dns.GetHostName();
                    IPHostEntry ip = Dns.GetHostEntry(host);
                    string CIP = (ip.AddressList[0].ToString());
                    string pubIp = new System.Net.WebClient().DownloadString("https://api.ipify.org");
                    //SqlConnection con_log = new SqlConnection();
                    //con_log.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port;Initial Catalog=dbname;Persist Security Info=True;User ID=dbuser;Password=passphrase";
                    string query = "insert into [dbo].[LogInfo]([LoginName],[Int_IP_Address],[Ext_IP_Address],[Action]) values('" + username + "','" + CIP + "','" + pubIp + "','" + "Login Succeed')";
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    try
                    {
                        //con_log.Open();
                        adapter.InsertCommand = new SqlCommand(query, con_log);
                        adapter.InsertCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());

                    }
                    con_log.Close();
                    con_log.Dispose();
                    MajorForm majorform = new MajorForm();
                    majorform.Show();
                    this.Hide();
                    

                }
                else if (is_locked == 1)
                {
                    MessageBox.Show("Your account is blocked, Please Contact Support Team");
                    string host = Dns.GetHostName();
                    IPHostEntry ip = Dns.GetHostEntry(host);
                    string CIP = (ip.AddressList[0].ToString());
                    string pubIp = new System.Net.WebClient().DownloadString("https://api.ipify.org");
                    //SqlConnection con_log = new SqlConnection();
                    //con_log.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port;Initial Catalog=dbname;Persist Security Info=True;User ID=dbuser;Password=passphrase";
                    string query = "insert into [dbo].[LogInfo]([LoginName],[Int_IP_Address],[Ext_IP_Address],[Action]) values('" + username + "','" + CIP + "','" + pubIp + "','" + "Login Blocked, User is locked')";
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    try
                    {
                        //con_log.Open();
                        adapter.InsertCommand = new SqlCommand(query, con_log);
                        adapter.InsertCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());

                    }

                }
                con_log.Close();
                con_log.Dispose();
            }
            else
            {
                _failedAttempts++;
                MessageBox.Show("Invalid Login, Please check Username and Password! \n\r" + (4 - _failedAttempts) + " Attempts Remaining!");
                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string CIP = (ip.AddressList[0].ToString());
                string pubIp = new System.Net.WebClient().DownloadString("https://api.ipify.org");
                //SqlConnection con_log = new SqlConnection();
                //con_log.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port;Initial Catalog=dbname;Persist Security Info=True;User ID=dbuser;Password=passphrase";
                string query = "insert into [dbo].[LogInfo]([LoginName],[Int_IP_Address],[Ext_IP_Address],[Action]) values('" + username + "','" + CIP + "','" + pubIp + "','" + "Attempt Failed," + (4 - _failedAttempts) + " Attempts Remaining,Pass was: " + password +"')";
                SqlDataAdapter adapter = new SqlDataAdapter();
                try
                {
                    //con_log.Open();
                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                    con_log.Close();
                    //con_log.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }

                if (_failedAttempts == 4)
                {
                   con_log.Open();
                   SqlCommand cmd_chk = new SqlCommand("select LoginName from Users where LoginName='" + username + "'", con_log);
                   SqlDataAdapter dat = new SqlDataAdapter(cmd_chk);
                    DataTable dt_chk = new DataTable();
                    dat.Fill(dt_chk);
                    if (dt_chk.Rows.Count > 0)
                    {
                        MessageBox.Show("Your account was locked, Please Contact Support Team");
                        string host1 = Dns.GetHostName();
                        IPHostEntry ip1 = Dns.GetHostEntry(host);
                        string CIP1 = (ip1.AddressList[0].ToString());
                        string pubIp1 = new System.Net.WebClient().DownloadString("https://api.ipify.org");
                        //SqlConnection con_log1 = new SqlConnection();
                        //con_log1.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port;Initial Catalog=dbname;Persist Security Info=True;User ID=dbuser;Password=passphrase";
                        string query1 = "insert into [dbo].[LogInfo]([LoginName],[Int_IP_Address],[Ext_IP_Address],[Action]) values('" + username + "','" + CIP + "','" + pubIp + "','" + "4 Attempts were reached, User is locked')";
                        SqlDataAdapter adapter1 = new SqlDataAdapter();
                        try
                        {
                            //con_log1.Open();
                            adapter1.InsertCommand = new SqlCommand(query1, con_log);
                            adapter1.InsertCommand.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());

                        }
                        //con_log1.Close();
                        //con_log1.Dispose();
                        SqlCommand cmd_upd = new SqlCommand("update Users set Locked = 1 where LoginName ='" + username + "'", con_log);
                        cmd_upd.ExecuteNonQuery();
                        con_log.Close();
                        con_log.Dispose();
                        Application.Exit();

                    }
                    else
                    {
                        MessageBox.Show("You Reached Allowed Attempts! \n\r Login Window is closing...");
                        string host2 = Dns.GetHostName();
                        IPHostEntry ip2 = Dns.GetHostEntry(host);
                        string CIP2 = (ip2.AddressList[0].ToString());
                        string pubIp2 = new System.Net.WebClient().DownloadString("https://api.ipify.org");
                        //SqlConnection con_log2 = new SqlConnection();
                        //con_log2.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port;Initial Catalog=dbname;Persist Security Info=True;User ID=dbuser;Password=passphrase";
                        string query2 = "insert into [dbo].[LogInfo]([LoginName],[Int_IP_Address],[Ext_IP_Address],[Action]) values('" + username + "','" + CIP + "','" + pubIp + "','" + "Reached Allowed Attempts, Invalid Username')";
                        SqlDataAdapter adapter2 = new SqlDataAdapter();
                        try
                        {
                            //con_log.Open();
                            adapter2.InsertCommand = new SqlCommand(query2, con_log);
                            adapter2.InsertCommand.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());

                        }
                        //con_log2.Close();
                        //con_log2.Dispose();
                        con_log.Close();
                        con_log.Dispose();
                        Application.Exit();

                    }

                }

            }
            
        }




        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }


        

    }
}