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

public partial class PendingGiftCards : System.Web.UI.Page
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



    protected void acceptBtn_Click(object sender, EventArgs e)
    {


        foreach (ListItem item in pendingList.Items)
        {
            if (item.Selected)
            {
                string active = "active";
                string id = item.Text;

                SqlConnection sc = new SqlConnection();
                sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
                sc.Open();
                SqlCommand insert = new SqlCommand();
                insert.Connection = sc;
                insert.CommandText = "UPDATE [dbo].[GiftCard] SET [Status] = @Status WHERE GiftCardID= @GiftCardID";
                insert.Parameters.AddWithValue("@Status", active);
                insert.Parameters.AddWithValue("@GiftCardID", id);
                insert.ExecuteNonQuery();
                sc.Close();
            }
        }
        Response.Write("<script>alert('Selected gift cards have been approved.')</script>");
        Response.Redirect("PendingGiftCards.aspx");

    }



    protected void rejectBtn_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in pendingList.Items)
        {
            if (item.Selected)
            {
                string rejected = "rejected";
                string id = item.Text;

                SqlConnection sc = new SqlConnection();
                sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
                sc.Open();
                SqlCommand insert = new SqlCommand();
                insert.Connection = sc;
                insert.CommandText = "UPDATE [dbo].[GiftCard] SET [Status] = @Status WHERE GiftCardID= @GiftCardID";
                insert.Parameters.AddWithValue("@Status", rejected);
                insert.Parameters.AddWithValue("@GiftCardID", id);
                insert.ExecuteNonQuery();
                sc.Close();
            }
        }
        Response.Write("<script>alert('Selected gift cards have been rejected.')</script>");
        Response.Redirect("PendingGiftCards.aspx");
    }
}