using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Windows.Forms;
using System.Configuration;

public partial class CreateVendor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
        HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
        HttpContext.Current.Response.AddHeader("Expires", "0");
        if (Session["loggedIn"] == null)
        {
            Response.Redirect("default.aspx");
        }

        
    }



    protected void BtnCommitRewardProvider_Click(object sender, EventArgs e)
    {
        try
        {

            RewardProvider company = new RewardProvider(CompanyNameText.Text, PhoneNumberText.Text, VendorEmailText.Text);
            company.setLastUpdatedBy((string)(Session["loggedIn"]));
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insert = new SqlCommand();
            insert.Connection = sc;
            insert.CommandText = "select [Email] from [RewardProvider] where [Email] = @E_mail";
            insert.Parameters.AddWithValue("@E_mail", company.getEmail());
            SqlDataReader reader = insert.ExecuteReader();

            if (reader.HasRows)
            {
                //Response.Write("<script>alert('Email record has already existed in Database')</script>");
                //reader.Close();
                //sc.Close();
            }
            else
            {
                reader.Close();
                insert.CommandText = "INSERT INTO [dbo].[RewardProvider] ([RewardProviderName],[PhoneNumber],[Email],[UserName],[Password],[Balance],[LastUpdated],[LastUpdatedBy]) VALUES" +
               "(@CompanyName,@PhoneNumber,@E_mail,@userName,@Password,@Balance,@LastUpdated,@LastUpdatedBy)";
                insert.Parameters.AddWithValue("@CompanyName", company.getCompanyName());
                insert.Parameters.AddWithValue("@PhoneNumber", company.getPhoneNumber());
                insert.Parameters.AddWithValue("@Balance", "0");
                insert.Parameters.AddWithValue("@LastUpdated", company.getLastUpdated());
                insert.Parameters.AddWithValue("@LastUpdatedBy", company.getLastUpdatedBy());

                string password = System.Web.Security.Membership.GeneratePassword(8, 6);
                string passwordHashNew = SimpleHash.ComputeHash(password, "MD5", null);

                insert.Parameters.AddWithValue("@Password", passwordHashNew);
                insert.Parameters.AddWithValue("@UserName", company.getEmail());


                insert.ExecuteNonQuery();
                sc.Close();
                Send_MailVendor(company.getEmail(), company.getCompanyName(), password);

                Response.Write("<script>alert('Vendor Added: " + company.getCompanyName() + " is created')</script>");
                CompanyNameText.Text = string.Empty;
                PhoneNumberText.Text = string.Empty;
                VendorEmailText.Text = string.Empty;
            }
            sc.Close();

            addVendorToPool(company.getEmail());
    }
        catch
        {
            Response.Write("<script>alert('Company not found in Database')</script>");
        }

    }

    public void Send_MailVendor(String email, String Name, String Password)
    {
        String message = "Dear " + Name + ", \n";
        message += " Your company has been added to the Peer to Peer reward system as a reward provider. \n";
        message += " We look forward to the great benifits that your company will provide for our employees.\n";
        message += " Below you will find your user credentials so that you can add events to our calendar and get paid! \n";
        message += " UserName:  " + Name + "\n PassWord: " + Password + "\n";
        MailMessage mail = new MailMessage("elkmessage@gmail.com", email, "Your Account Has been Created", message);
        SmtpClient client = new SmtpClient();
        client.EnableSsl = true;
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new System.Net.NetworkCredential("elkmessage@gmail.com", "javapass");
        client.Host = "smtp.gmail.com";
        client.Send(mail);
    }

    protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    public void addVendorToPool(String email)
    {
        string[] companyArray = new string[ListBox1.Items.Count];
        int itemCount = ListBox1.Items.Count;
        for (int i = 0; i < itemCount; i++)
        {
            companyArray[i] = ListBox1.Items[i].Value.ToString();
        }

        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand insert = new SqlCommand();
        insert.Connection = sc;
        insert.CommandText = "select RewardProviderID from RewardProvider where rewardprovider.email = @Email";
        insert.Parameters.AddWithValue("@Email", email);
        SqlDataReader reader = insert.ExecuteReader();
        reader.Read();
        int rpID = Convert.ToInt32(reader["RewardProviderID"]);
        reader.Close();


        try
        {
            foreach (string element in companyArray)
            {
            insert.Parameters.Clear();

            insert.CommandText = "INSERT INTO [dbo].[RewardProviderPool] ([BusinessEntityID],[RewardProviderID],[Status],[LastUpdated],[LastUpdatedBy]) VALUES" +
                   "(@BID, @RPID,@Status,@LU,@LUB)";
                insert.Parameters.AddWithValue("@BID", element);
                insert.Parameters.AddWithValue("@RPID", rpID);
                insert.Parameters.AddWithValue("@Status", "active");
                insert.Parameters.AddWithValue("@LU", DateTime.Today);
                insert.Parameters.AddWithValue("@LUB", "CEO");
                insert.ExecuteNonQuery();
                Response.Write("<script>alert('Reward Provider Added To Company Directory')</script>");


            }
        }

        catch
        {
            Response.Write("<script>alert('You made a coding error darren')</script>");
        }
        sc.Close();
       

    }
}