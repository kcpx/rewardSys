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

public partial class CEOVendors : System.Web.UI.Page
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



    protected void removeBtn_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in vendorList.Items)
        {
            if (item.Selected)
            {
                string id = item.Text;

                SqlConnection sc = new SqlConnection();
                sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
                sc.Open();
                SqlCommand insert = new SqlCommand();
                insert.Connection = sc;
                insert.CommandText = "UPDATE [dbo].[RewardProviderPool] SET [Status] = @Status WHERE RewardProviderID= @RewardProviderID";
                insert.Parameters.AddWithValue("@Status", "removed");
                insert.Parameters.AddWithValue("@RewardProviderID", id);
                insert.ExecuteNonQuery();
                sc.Close();

                sc.Open();
                SqlCommand ins = new SqlCommand();
                ins.Connection = sc;
                ins.CommandText = "UPDATE [dbo].[GiftCard] SET [Status] = @Status WHERE RewardProviderID = @RewardProviderID AND ([dbo].[RewardProviderPool].BusinessEntityID = @BusinesEntityID)";
                insert.Parameters.AddWithValue("@Status", "removed");
                insert.Parameters.AddWithValue("@RewardProviderID", id);
                insert.Parameters.AddWithValue("@VendorID", Session["VendorID"]);
                ins.ExecuteNonQuery();
                sc.Close();
            }
        }
        Response.Write("<script>alert('Selected vendors have been removed.')</script>");
        Response.Redirect("CEOVendors.aspx");
    }
}