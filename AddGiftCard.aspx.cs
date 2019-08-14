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

public partial class AddGiftCard : System.Web.UI.Page
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

    


    protected void SaveBtn_Click(object sender, EventArgs e)
    {
        SqlConnection sc = new SqlConnection();
        sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        sc.Open();

        string provider = ProviderDrop.SelectedValue;
        string notes = DescriptionTxt.Text;
        string value = valueTxt.Text;

        GiftCard card = new GiftCard("1", value, notes, DateTime.Today, DateTime.Today, DateTime.Today, Session["loggedIn"].ToString());



    }

    public void clear()
    {
        DescriptionTxt.Text = "";
    }
}