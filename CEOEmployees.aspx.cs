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

public partial class CEOEmployees : System.Web.UI.Page
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

    //protected void removeBtn_Click(object sender, EventArgs e)
    //{
    //    foreach (ListItem item in removeEmployeesCB.Items)
    //    {
    //        if (item.Selected)
    //        {
    //            string id = item.Text;

    //            SqlConnection sc = new SqlConnection();
    //            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
    //            sc.Open();
    //            SqlCommand insert = new SqlCommand();
    //            insert.Connection = sc;
    //            insert.CommandText = "UPDATE [dbo].[Person] SET [Status] = @Status WHERE PersonID= @PersonID AND BusinessEntityID = @BusinessEntityID";
    //            insert.Parameters.AddWithValue("@Status", "deactivated");
    //            insert.Parameters.AddWithValue("@PersonID", id);
    //            insert.Parameters.AddWithValue("@BusinessEntityID", Session["BusinessEntityID"]);
    //            insert.ExecuteNonQuery();
    //            sc.Close();
    //        }
    //    }
    //    Response.Write("<script>alert('Selected employees have been removed.')</script>");
    //    Response.Redirect("CEOEmployees.aspx");
    //}
}