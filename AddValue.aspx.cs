using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class AddValue : System.Web.UI.Page
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
        try
        {
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insert = new SqlCommand();
            insert.Connection = sc;
            insert.CommandText = "select [ValueName] from [Value] where [ValueName] = @ValueName";
            insert.Parameters.AddWithValue("@ValueName", txtValueName.Text);
            insert.Parameters.AddWithValue("@ValueDescription", txtValueDescription.Text);
            SqlDataReader reader = insert.ExecuteReader();

            if (reader.HasRows)
            {
                Response.Write("<script>alert('Value Name: " + txtValueName.Text + " Already Exists!')</script>");
                reader.Close();
                sc.Close();
            }
            else
            {
                reader.Close();
                insert.CommandText = "INSERT INTO [dbo].[Value] VALUES (@ValueName,@ValueDescription,'" + DateTime.Today + "','"
                    + Session["FirstName"] + "'," + Session["BusinessEntityID"] + ")";
                insert.ExecuteNonQuery();
                sc.Close();

                Response.Write("<script>alert('New Value: " + txtValueName.Text + " Has Been Added!')</script>");
                txtValueName.Text = string.Empty;
                txtValueDescription.Text = string.Empty;
            }
        }
        catch
        {
            Response.Write("<script>alert('System Error. Please Check your Inputs..')</script>");
        }
    }
}