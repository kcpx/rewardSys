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



public partial class VendorAddGC : System.Web.UI.Page
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
    protected void BtnCommit_Click(object sender, EventArgs e)
    {
        byte[] gcImage = new byte[0];

        try
        {
            HttpPostedFile postedFile = PictureUpload.PostedFile;
            string filename = Path.GetFileName(postedFile.FileName);
            string extension = Path.GetExtension(filename);
            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".bmp" || extension.ToLower() == ".gif" || extension.ToLower() == ".png")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] bytes = binaryReader.ReadBytes((int)stream.Length);
                gcImage = bytes;


            }
            else
            {
                Response.Write("<script>alert('Only .jpg/.bmp/.gif/.png file can be upload')</script>");

            }
        }
        catch
        {
            Response.Write("<script>alert('Picture Size is exceeding the database can take')</script>");
        }

        try
        {
            string[] companyArray = new string[ListBox1.Items.Count];
            int itemCount = ListBox1.Items.Count;
            for (int i = 0; i < itemCount; i++)
            {
                companyArray[i] = ListBox1.Items[i].Value.ToString();
            }

            

                
                GiftCard gc = new GiftCard(txtValue.Text, txtGCDescription.Text);
                gc.setLastUpdatedBy((string)(Session["loggedIn"]));

                SqlConnection sc = new SqlConnection();
                sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
                sc.Open();
                SqlCommand insert = new SqlCommand();
                insert.Connection = sc;
            foreach (string element in companyArray)
            {
                insert.Parameters.Clear();


                insert.CommandText = "INSERT INTO [dbo].[GiftCard] ([RewardProviderID],[Value],[Description], [DatePosted],[Status],[DateRemoved],[LastUpdated],[LastUpdatedBy],[VoucherNumber],[ImageGC],[BusinessEntityID]) VALUES" +
                  "(@RewardProviderID,@Value,@Description, @DatePosted, @Status,@DateRemoved,@LastUpdated,@LastUpdatedBy,@VoucherNumber,@ImageGC,@BID)";
                insert.Parameters.AddWithValue("@RewardProviderID", Session["ID"]);
                insert.Parameters.AddWithValue("@Value", gc.getValue());
                insert.Parameters.AddWithValue("@Description", gc.getNotes());
                insert.Parameters.AddWithValue("@DatePosted", DateTime.Today.ToShortDateString());
                insert.Parameters.AddWithValue("@Status", "pending");
                insert.Parameters.AddWithValue("@DateRemoved", DBNull.Value);
                insert.Parameters.AddWithValue("@LastUpdated", gc.getLastUpdated());
                insert.Parameters.AddWithValue("@LastUpdatedBy", gc.getLastUpdatedBy());
                insert.Parameters.AddWithValue("@VoucherNumber", DBNull.Value);
                insert.Parameters.AddWithValue("@ImageGC", gcImage);
                insert.Parameters.AddWithValue("@BID", element);


                insert.ExecuteNonQuery();
                
                Send_Mail();

                Response.Write("<script>alert('Gift Card Has Been Submited To Our System And Is Pending Approval By Our CEO')</script>");

            }
            txtValue.Text = string.Empty;
            txtGCDescription.Text = string.Empty;
            sc.Close();





    }

        catch
        {
            //Response.Write("<script>alert('Error Creating GiftCard Please Contact the CEO')</script>");
        }


    }

    //protected void BtnGenerate_Click(object sender, EventArgs e)
    //{
    //    string VoucherCode = System.Web.Security.Membership.GeneratePassword(15, 6);
    //    txtVoucherCode.Text = VoucherCode;
    //}


    public void Send_Mail()
    {
        String message = "Dear CEO: \n";
        message += "A Gift Card has been submited and is waiting for your approval!";
        MailMessage mail = new MailMessage("elkmessage@gmail.com", "darrenwoodwarda@gmail.com", "Gift Card Pending Your Approval", message);
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
}
