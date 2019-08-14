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
using System.IO;


public partial class AdminCreateCompany : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
        HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
        HttpContext.Current.Response.AddHeader("Expires", "0");
        if (Session["loggedIn"] == null || (Session["Position"].ToString() != "ADMIN"))
        {
            Session.Clear();
            Response.Redirect("default.aspx");
        }





    }

    protected void BtnCommit_Click(object sender, EventArgs e)
    {
        Company company = new Company(txtCompanyName.Text,txtPhoneNumber.Text, txtEmail.Text);
        company.setLastUpdatedBy((string)(Session["FirstName"]));
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();
        SqlCommand insert = new SqlCommand();
        insert.Connection = sc;



        insert.CommandText = "select [Email] from [BusinessEntity] where [Email] = @Email";
        insert.Parameters.AddWithValue("@Email", company.getEmail());
        SqlDataReader reader = insert.ExecuteReader();
        DateTime LastUpdated = DateTime.Today;

        if (reader.HasRows)
        {
            Response.Write("<script>alert('Email record already exists in the Database for a company')</script>");
            reader.Close();
            sc.Close();
        }
        else
        {
            reader.Close();
            insert.CommandText = "INSERT INTO [dbo].[BusinessEntity] ([BusinessEntityName],[PhoneNumber],[Email],[LastUpdated],[LastUpdatedBy]) VALUES" +
           "(@BusinessEntityName,@PhoneNumber,@Email,@LastUpdated,@LastUpdatedBy)";
            insert.Parameters.AddWithValue("@BusinessEntityName", company.getBusinessEntityName());
            insert.Parameters.AddWithValue("@PhoneNumber", company.getPhoneNumber());
            insert.Parameters.AddWithValue("@LastUpdated", company.getLastUpdated());
            insert.Parameters.AddWithValue("@LastUpdatedBy", company.getLastUpdatedBy());
            insert.ExecuteNonQuery();

            string businessEntityID = "";
            insert.Parameters.Clear();

            insert.CommandText = "select [dbo].[BusinessEntity].[BusinessEntityID] from [dbo].[BusinessEntity] where Email = @Email";
            insert.Parameters.AddWithValue("@Email", company.getEmail());
            SqlDataReader BIDReader = insert.ExecuteReader();
            if (BIDReader.HasRows)
            {
                BIDReader.Read();
                businessEntityID = BIDReader["BusinessEntityID"].ToString();
                BIDReader.Close();
                sc.Close();
                Response.Write("<script>alert('Business Account: " + company.getBusinessEntityName() + "" + " created succesfully')</script>");
                createCEO(businessEntityID);
            }


        }
    }
    public void createCEO(string BID)
    {
        try
        {
            Person employee = new Person(txtFirstName.Text, txtLastName.Text, txtEmail.Text);
            employee.setLastUpdatedBy((string)(Session["FirstName"]));
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insert = new SqlCommand();
            insert.Connection = sc;
            insert.CommandText = "select [E-mail] from [Person] where [E-mail] = @Email";
            insert.Parameters.AddWithValue("@Email", employee.getEmail());
            SqlDataReader reader = insert.ExecuteReader();

            if (reader.HasRows)
            {
                Response.Write("<script>alert('Email record has already existed in Database')</script>");
                reader.Close();
                sc.Close();
            }
            else
            {
                reader.Close();
                insert.CommandText = "INSERT INTO [dbo].[Person] ([FirstName],[LastName],[MI],[E-mail],[Position],[Password],[UserName],[PointsBalance],[PendingPoints],[LastUpdated],[LastUpdatedBy],[BusinessEntityID],[loginCount],[Status]) VALUES" +
               "(@FirstName,@LastName,@MI,@Email,@Position,@Password,@UserName,@PointsBalance,@PendingPoints,@LastUpdated,@LastUpdatedBy,@BusinessEntityID,0,@Status)";
                insert.Parameters.AddWithValue("@FirstName", employee.getFirstName());
                insert.Parameters.AddWithValue("@LastName", employee.getLastName());
                insert.Parameters.AddWithValue("@Position", "CEO");
                insert.Parameters.AddWithValue("@PointsBalance", employee.getPointsBalance());
                insert.Parameters.AddWithValue("@PendingPoints", employee.getPendingPoints());
                insert.Parameters.AddWithValue("@BusinessEntityID", BID);
                insert.Parameters.AddWithValue("@LastUpdatedBy", employee.getLastUpdatedBy());
                insert.Parameters.AddWithValue("@LastUpdated", employee.getLastUpdated());
                insert.Parameters.AddWithValue("@Status", "active");

                if (txtMI.Text.Trim() == "")
                {
                    insert.Parameters.AddWithValue("@MI", DBNull.Value);
                }
                else
                {
                    insert.Parameters.AddWithValue("@MI", txtMI.Text.Trim());
                }
                string password = System.Web.Security.Membership.GeneratePassword(8, 6);
                string passwordHashNew = SimpleHash.ComputeHash(password, "MD5", null);

                insert.Parameters.AddWithValue("@Password", passwordHashNew);
                insert.Parameters.AddWithValue("@UserName", employee.getEmail());
                insert.ExecuteNonQuery();
                sc.Close();
                Send_Mail(employee.getEmail(), employee.getEmail(), password);

                Response.Write("<script>alert('CEO Account: " + employee.getFirstName() + "" + employee.getMI() + " " + employee.getLastName() + " created succesfully')</script>");
                txtFirstName.Text = string.Empty;
                txtMI.Text = string.Empty;
                txtLastName.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtPhoneNumber.Text = string.Empty;
                txtCeoEmail.Text = string.Empty;
                txtCompanyName.Text = string.Empty;



            }

    }
        catch
        {
            Response.Write("<script>alert('Error When Creating CEO Account (CONTACT DEVS)')</script>");
        }

    }

    public void Send_Mail(String email, String Name, String Password)
    {
        String message = "Dear CEO: \n";
        message += "Your Company Has Been Added To Our System!!\n";
        message += "Please login with UserName and Password provided below:\n";
        message += "UserName:  " + Name + "\n PassWord: " + Password + "\n";
        MailMessage mail = new MailMessage("elkmessage@gmail.com", email, "Your Account Has been Created", message);
        SmtpClient client = new SmtpClient();
        client.EnableSsl = true;
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Credentials = new System.Net.NetworkCredential("elkmessage@gmail.com", "javapass");
        client.Host = "smtp.gmail.com";
        client.Send(mail);
    }

}