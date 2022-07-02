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
using System.Net;


namespace Fix_InvoiceS
{

    


    public partial class MajorForm : Form
    {

        //SqlConnection con_cu_test = new SqlConnection();
        //SqlConnection con_eg_test = new SqlConnection();
        //SqlConnection con_gr_test = new SqlConnection();
        //SqlConnection con_mt_test = new SqlConnection();

        //SqlConnection con_cu = new SqlConnection();
        //SqlConnection con_do = new SqlConnection();
        //SqlConnection con_ma = new SqlConnection();
        //SqlConnection con_mx = new SqlConnection();
        //SqlConnection con_eg = new SqlConnection();
        //SqlConnection con_gr = new SqlConnection();
        //SqlConnection con_cn = new SqlConnection();
        //SqlConnection con_du = new SqlConnection();
        //SqlConnection con_a  = new SqlConnection();
        //SqlConnection con_cr = new SqlConnection();
        //SqlConnection con_om = new SqlConnection();
        //SqlConnection con_ca = new SqlConnection();
        //SqlConnection con_mt = new SqlConnection();
        //SqlConnection con_cy = new SqlConnection();
        //SqlConnection con_iq = new SqlConnection();
        //SqlConnection con_tu = new SqlConnection();
        //SqlConnection con_lb = new SqlConnection();


        public string username_2;

        SqlConnection con = null;

        string prefix;
        string sqlcom_a;
        string sqlcom_b;
        string sqlexist;
        int unsp;
        string seleSP;
        int counter ;
        DateTime dt = new DateTime();


        /// <summary>
               
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        
        /// </summary>





        public MajorForm()
        {
            
            InitializeComponent();
            //con_cu_test.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase;MultipleActiveResultSets=True";
            //con_eg_test.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphraseMultipleActiveResultSets=True";
            //con_gr_test.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase";
            //con_mt_test.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase";

            //con_cu.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase";
            //con_do.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase";
            //con_ma.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase";
            //con_mx.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase";
            //con_eg.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase";
            //con_gr.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase";
            //con_cn.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase";
            //con_du.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase";
            //con_cr.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase";
            //con_om.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase";
            //con_a.ConnectionString  = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase";
            //con_ca.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase";
            //con_mt.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase";
            //con_cy.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase";
            //con_iq.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase";
            //con_tu.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase";
            //con_lb.ConnectionString = "Data Source=xx.xx.xx.xx\\xx,port,;Initial Catalog=dbname;Persist Security Info=True;User ID=SPM_TOOL;Password=passphrase";





            //timer1.Enabled = true;
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000;


            LoginForm frm1 = (LoginForm)Application.OpenForms["LoginForm"];
            username_2 = frm1.textBox1.Text;
            

            string host = Dns.GetHostName();
            IPHostEntry ip = Dns.GetHostEntry(host);
            string CIP = (ip.AddressList[0].ToString());
            string pubIp = new System.Net.WebClient().DownloadString("https://api.ipify.org");
            SqlConnection con_log = null;
            con_log = new SqlConnection(ConfigurationManager.ConnectionStrings["Log_S"].ConnectionString);
            string query = "insert into [dbo].[LogInfo]([LoginName],[Int_IP_Address],[Ext_IP_Address],[Action]) values('" + username_2 + "','" + CIP + "','" + pubIp + "','" + "Major Form Loaded')";
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
            
        }

                

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

 

            string comboValue = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
            string host = Dns.GetHostName();
            IPHostEntry ip = Dns.GetHostEntry(host);
            string CIP = (ip.AddressList[0].ToString());
            string pubIp = new System.Net.WebClient().DownloadString("https://api.ipify.org");
            SqlConnection con_log = null;
            con_log = new SqlConnection(ConfigurationManager.ConnectionStrings["Log_S"].ConnectionString);
            con_log.Open();
            string query = "insert into [dbo].[LogInfo]([LoginName],[Int_IP_Address],[Ext_IP_Address],[Action]) values('" + username_2 + "','" + CIP + "','" + pubIp + "','" + comboValue + " Connection Selected')";
            SqlDataAdapter adapter = new SqlDataAdapter();


            if (comboValue== "Cuba")
            {
                prefix = "CU";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Cuba"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();

            }
            else if (comboValue == "Dominican Republic")
            {
                prefix = "DO";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Dom_Rep"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();

            }
            else if (comboValue == "Morocco")
            {
                prefix = "MA";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Morocco"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();

            }
            else if (comboValue == "Mexico")
            {
                prefix = "MX";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mexico"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();

            }
            else if (comboValue == "Egypt")
            {
                prefix = "EG";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Egypt"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();

            }
            else if (comboValue == "Greece")
            {
                prefix = "GR";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Greece"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();
            }
            else if (comboValue == "Spain")
            {
                prefix = "CN";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Spain"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();

            }
            else if (comboValue == "Dubai")
            {
                prefix = "DU";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Dubai"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();

            }
            else if (comboValue == "Asia")
            {
                prefix = "A";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Asia"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();
            }
            else if (comboValue == "Croatia")
            {
                prefix = "CR";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Croatia"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();
            }
            else if (comboValue == "Oman")
            {
                prefix = "OM";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Oman"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();
            }
            else if (comboValue == "Italy")
            {
                prefix = "CA";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Italy"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();
            }
            else if (comboValue == "Malta")
            {
                prefix = "MT";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Malta"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();
            }
            else if (comboValue == "Cyprus")
            {
                prefix = "CY";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Cyprus"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();

            }
            else if (comboValue == "Turkey")
            {
                prefix = "IQ";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Turkey"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();

            }
            else if (comboValue == "Tunisia")
            {
                prefix = "TU";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Tunisia"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();

            }
            else if (comboValue == "Lebanon")
            {
                prefix = "LB";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Lebanon"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();

            }
            else if (comboValue == "Gambia")
            {
                prefix = "GM";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Gambia"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();

            }
            else if (comboValue == "Bahrain")
            {
                prefix = "BH";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Bahrain"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();

            }


            else if (comboValue == "Test Malta")
            {

                prefix = "MT";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Test_Malta"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();
            }
            else if (comboValue == "Test Greece")
            {

                prefix = "GR";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Test_Greece"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();

            }
            else if (comboValue == "Test Cuba")
            {
                            
                prefix = "CU";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Test_Cuba"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();
            }
            else if (comboValue == "Test Egypt")
            {

                prefix = "EG";

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Test_Egypt"].ConnectionString);
                try
                {

                    adapter.InsertCommand = new SqlCommand(query, con_log);
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
                con_log.Close();

            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string comboValue = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
            string host = Dns.GetHostName();
            IPHostEntry ip = Dns.GetHostEntry(host);
            string CIP = (ip.AddressList[0].ToString());
            string pubIp = new System.Net.WebClient().DownloadString("https://api.ipify.org");
            SqlConnection con_log = null;
            con_log = new SqlConnection(ConfigurationManager.ConnectionStrings["Log_S"].ConnectionString);



            counter = 120;
                string resID = textBox1.Text;
                string combotext = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);

            if (!string.IsNullOrEmpty(resID) && !string.IsNullOrEmpty(combotext))
            {
                int parsedValue;
                if (!int.TryParse(textBox1.Text, out parsedValue))
                {
                    MessageBox.Show(resID + " is not a number !! This is a number only field ! Please input value in correct format ...");
                }
                else
                {

                    sqlexist = "select VoucherID from [My_" + prefix + "_Reservation_2011].[dbo].[Res_Reservation] where RefID =" + resID;

                    con.Open();

 
                        SqlCommand cmd_exist = new SqlCommand(sqlexist, con);
                        SqlDataReader reader = cmd_exist.ExecuteReader();
                        if (reader.Read())
                        {
                            // The command returns Row(s)
                            //MessageBox.Show("The command returns Row(s)... ");
                            if (radioButton1.Checked)
                            {
                                unsp = 1;
                                seleSP = " with Special Price";
                            }
                            else if (radioButton2.Checked)
                            {
                                unsp = 0;
                                seleSP = " without Special Price";

                            }
                            //sqlcom_a = "EXECUTE [dbo].[SPrOC_lock_special_price] " + resID + "," + unsp.ToString();
                            sqlcom_a = "declare @voucher2unlock nvarchar(200) declare @unlockspecialprice int declare @refID2unlock int declare @side2unlock nvarchar(10) set @refID2unlock = " + resID + " set @unlockspecialprice = " + unsp.ToString() + " set @voucher2unlock = (select VoucherID from [My_" + prefix + "_Reservation_2011].[dbo].[Res_Reservation] where RefID = @refID2unlock) if @unlockspecialprice = 1 Begin update [My_" + prefix + "_Accounting_2011].[dbo].[Inv_InvoiceDetails] set InvoiceID = InvoiceID + 10000000 where VoucherID like @voucher2unlock update [My_" + prefix + "_Accounting_2011].[dbo].[Inv_Proforma] set Indicator = 'Norm' where RefID = @refID2unlock and Indicator = 'Spe' end if @unlockspecialprice = 0 Begin update [My_" + prefix + "_Accounting_2011].[dbo].[Inv_InvoiceDetails] set InvoiceID = InvoiceID + 10000000 where VoucherID like @voucher2unlock end";
                            SqlCommand cmd_dest_a = new SqlCommand(sqlcom_a, con);
                            cmd_dest_a.ExecuteNonQuery();

                        string query = "insert into [dbo].[LogInfo]([LoginName],[Int_IP_Address],[Ext_IP_Address],[Action]) values('" + username_2 + "','" + CIP + "','" + pubIp + "','ReFID " +  resID + " unlocking execution on " + comboValue  + seleSP+ "')";
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
                        timer1.Enabled = true;
                        button2.Enabled = true;

                        }
                        else
                        {
                            // No Row has been returned.
                            MessageBox.Show("RefID " + resID + " is invalid! Please check...");
                        }
                    }
                }



            //sqlcom_a = "declare @voucher2unlock nvarchar(200) declare @unlockspecialprice int declare @refID2unlock int declare @side2unlock nvarchar(10) set @refID2unlock = " + resID + " set @unlockspecialprice = " + unsp.ToString() + " set @voucher2unlock = (select VoucherID from [My_" + prefix + "_Reservation_2011].[dbo].[Res_Reservation] where RefID = @refID2unlock) if @unlockspecialprice = 1 Begin update [My_" + prefix + "_Accounting_2011].[dbo].[Inv_InvoiceDetails] set InvoiceID = InvoiceID + 10000000 where VoucherID like @voucher2unlock update [My_" + prefix + "_Accounting_2011].[dbo].[Inv_Proforma] set Indicator = 'Norm' where RefID = @refID2unlock and Indicator = 'Spe' end if @unlockspecialprice = 0 Begin update [My_" + prefix + "_Accounting_2011].[dbo].[Inv_InvoiceDetails] set InvoiceID = InvoiceID + 10000000 where VoucherID like @voucher2unlock end";
            //sqlcom_b = "declare @voucher2unlock nvarchar(200) declare @unlockspecialprice int declare @refID2unlock int declare @side2unlock nvarchar(10) set @refID2unlock = " + resID + " set @unlockspecialprice = " + unsp.ToString() + " set @voucher2unlock = (select VoucherID from [My_" + prefix + "_Reservation_2011].[dbo].[Res_Reservation] where RefID = @refID2unlock) if @unlockspecialprice = 1 Begin update [My_" + prefix + "_Accounting_2011].[dbo].[Inv_InvoiceDetails] set InvoiceID = InvoiceID - 10000000 where VoucherID like @voucher2unlock end if @unlockspecialprice = 0 Begin update [My_" + prefix + "_Accounting_2011].[dbo].[Inv_InvoiceDetails] set InvoiceID = InvoiceID - 10000000 where VoucherID like @voucher2unlock end";
            //SqlCommand cmd_dest_b = new SqlCommand(sqlcom_b, con_cu_test);
            //timer1.Enabled = true; // Enable the timer.            
            //cmd_dest_b.ExecuteNonQuery();
            //timer1.Stop();
            //timer1.Enabled = false;
            //cmd_dest.ExecuteNonQuery();
            //textBox2.Text = counter.ToString();
            //timer1.Enabled = false;

            //-------------Second--------------
            //timer1.Enabled = true;
            //timer1.Start();
            //timer1.Interval = 10000;
            //timer1.Tick += new EventHandler(timer1_Tick);
            //-------------Second--------------

            else if (string.IsNullOrEmpty(resID) && !string.IsNullOrEmpty(combotext))
            {
                MessageBox.Show("RefID Field is empty! Please input a valid value ...");
            }
            else if (!string.IsNullOrEmpty(resID) && string.IsNullOrEmpty(combotext))
            {
                MessageBox.Show("None Connection to GwG destination is selected !! Please select one ...  ");
            }
            else
            {
                MessageBox.Show("Please select Meeting Point Destination and RefID ... ");
            }

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string comboValue = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
            string host = Dns.GetHostName();
            IPHostEntry ip = Dns.GetHostEntry(host);
            string CIP = (ip.AddressList[0].ToString());
            string pubIp = new System.Net.WebClient().DownloadString("https://api.ipify.org");
            SqlConnection con_log = null;
            con_log = new SqlConnection(ConfigurationManager.ConnectionStrings["Log_S"].ConnectionString);


            comboBox1.Enabled = false;
            textBox1.Enabled = false;
            button1.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            //Perform one step...                      
            if (counter >= 0)
            {

                textBox2.Text = dt.AddSeconds(counter).ToString("mm:ss");
                counter--;
            }
            else
            {
                string resID = textBox1.Text;
                sqlcom_b = "declare @voucher2unlock nvarchar(200) declare @unlockspecialprice int declare @refID2unlock int declare @side2unlock nvarchar(10) set @refID2unlock = " + resID + " set @unlockspecialprice = " + unsp.ToString() + " set @voucher2unlock = (select VoucherID from [My_" + prefix + "_Reservation_2011].[dbo].[Res_Reservation] where RefID = @refID2unlock) if @unlockspecialprice = 1 Begin update [My_" + prefix + "_Accounting_2011].[dbo].[Inv_InvoiceDetails] set InvoiceID = InvoiceID - 10000000 where VoucherID like @voucher2unlock end if @unlockspecialprice = 0 Begin update [My_" + prefix + "_Accounting_2011].[dbo].[Inv_InvoiceDetails] set InvoiceID = InvoiceID - 10000000 where VoucherID like @voucher2unlock end";
                //sqlcom_b = "EXECUTE [dbo].[SPrOC_Unlock_special_price] " + resID + "," + unsp.ToString();
                SqlCommand cmd_dest_b = new SqlCommand(sqlcom_b, con);
                cmd_dest_b.ExecuteNonQuery();
                con.Close();

                string query = "insert into [dbo].[LogInfo]([LoginName],[Int_IP_Address],[Ext_IP_Address],[Action]) values('" + username_2 + "','" + CIP + "','" + pubIp + "','ReFID " + resID + " locking execution on " + comboValue + seleSP + "')";
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

                timer1.Enabled = false;
                textBox1.Enabled = true;
                button1.Enabled = true;
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                comboBox1.Enabled = true;
                button2.Enabled = false;
                MessageBox.Show("RefID " + resID + " on " + comboBox1.SelectedItem + " has been locked !");

            }



        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    unsp = 1;
                }

            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    unsp = 0;
                }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string comboValue = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
            string host = Dns.GetHostName();
            IPHostEntry ip = Dns.GetHostEntry(host);
            string CIP = (ip.AddressList[0].ToString());
            string pubIp = new System.Net.WebClient().DownloadString("https://api.ipify.org");
            SqlConnection con_log = null;
            con_log = new SqlConnection(ConfigurationManager.ConnectionStrings["Log_S"].ConnectionString);

            timer1.Enabled = false;
            textBox2.Text = "00:00";
            counter = 120;
            string resID = textBox1.Text;
            sqlcom_b = "declare @voucher2unlock nvarchar(200) declare @unlockspecialprice int declare @refID2unlock int declare @side2unlock nvarchar(10) set @refID2unlock = " + resID + " set @unlockspecialprice = " + unsp.ToString() + " set @voucher2unlock = (select VoucherID from [My_" + prefix + "_Reservation_2011].[dbo].[Res_Reservation] where RefID = @refID2unlock) if @unlockspecialprice = 1 Begin update [My_" + prefix + "_Accounting_2011].[dbo].[Inv_InvoiceDetails] set InvoiceID = InvoiceID - 10000000 where VoucherID like @voucher2unlock end if @unlockspecialprice = 0 Begin update [My_" + prefix + "_Accounting_2011].[dbo].[Inv_InvoiceDetails] set InvoiceID = InvoiceID - 10000000 where VoucherID like @voucher2unlock end";
            //sqlcom_b = "EXECUTE [dbo].[SPrOC_Unlock_special_price] " + resID + "," + unsp.ToString();
            SqlCommand cmd_dest_b = new SqlCommand(sqlcom_b, con);
            cmd_dest_b.ExecuteNonQuery();
            con.Close();
            button2.Enabled = false;
            textBox1.Enabled = true;
            button1.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            comboBox1.Enabled = true;
            MessageBox.Show("RefID " + resID + " on " + comboBox1.SelectedItem + " has been locked !");

            string query = "insert into [dbo].[LogInfo]([LoginName],[Int_IP_Address],[Ext_IP_Address],[Action]) values('" + username_2 + "','" + CIP + "','" + pubIp + "','ReFID " + resID + " locking execution on " + comboValue + seleSP + "')";
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string comboValue = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
            string host = Dns.GetHostName();
            IPHostEntry ip = Dns.GetHostEntry(host);
            string CIP = (ip.AddressList[0].ToString());
            string pubIp = new System.Net.WebClient().DownloadString("https://api.ipify.org");
            SqlConnection con_log = null;
            con_log = new SqlConnection(ConfigurationManager.ConnectionStrings["Log_S"].ConnectionString);
            string query = "insert into [dbo].[LogInfo]([LoginName],[Int_IP_Address],[Ext_IP_Address],[Action]) values('" + username_2 + "','" + CIP + "','" + pubIp + "','Exiting application')";
            SqlDataAdapter adapter = new SqlDataAdapter();

            if (button2.Enabled == false)
            {
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

                Application.Exit();
            }
            else
            {
                MessageBox.Show("Please First Lock Input RefID to exit application...");
            }


        }
    }
}
