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

public partial class CEOGiftCard : System.Web.UI.Page
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



    //protected void AddGiftCard_Click(object sender, EventArgs e)
    //{

    //}

    //protected void removeBtn_Click(object sender, EventArgs e)
    //{



    //    foreach (ListItem item in activeList.Items)
    //    {
    //        if (item.Selected)
    //        {
    //            string remove = "removed";
    //            string id = item.Text;

    //            SqlConnection sc = new SqlConnection();
    //            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
    //            sc.Open();
    //            SqlCommand insert = new SqlCommand();
    //            insert.Connection = sc;
    //            insert.CommandText = "UPDATE [dbo].[GiftCard] SET [Status] = @Status WHERE GiftCardID= @GiftCardID";
    //            insert.Parameters.AddWithValue("@Status", remove);
    //            insert.Parameters.AddWithValue("@GiftCardID", id);
    //            insert.ExecuteNonQuery();
    //            sc.Close();
    //        }
    //    }
    //    Response.Write("<script>alert('Selected gift cards have been removed.')</script>");
    //    Response.Redirect("CEOGiftCard.aspx");
    //}

    //protected void pendingBtn_Click(object sender, EventArgs e)
    //{
    //    foreach (ListItem item in activeList.Items)
    //    {
    //        if (item.Selected)
    //        {
    //            string pending = "pending";
    //            string id = item.Text;

    //            SqlConnection sc = new SqlConnection();
    //            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
    //            sc.Open();
    //            SqlCommand insert = new SqlCommand();
    //            insert.Connection = sc;
    //            insert.CommandText = "UPDATE [dbo].[GiftCard] SET [Status] = @Status WHERE GiftCardID= @GiftCardID";
    //            insert.Parameters.AddWithValue("@Status", pending);
    //            insert.Parameters.AddWithValue("@GiftCardID", id);
    //            insert.ExecuteNonQuery();
    //            sc.Close();
    //        }
    //    }
    //    Response.Write("<script>alert('Selected gift cards have been moved to pending.')</script>");
    //    Response.Redirect("CEOGiftCard.aspx");
    //}
}