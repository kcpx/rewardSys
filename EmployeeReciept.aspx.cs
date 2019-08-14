using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Default2 : System.Web.UI.Page
{
 
    protected void Page_Load(object sender, EventArgs e)
    {


        Random rnd = new Random();
        confirmation.Text = rnd.Next(5, 100).ToString(); // creates a number between 1 and 12
        labelgName.Text = Session["loggedIn"].ToString();
        labelgdate.Text = DateTime.Now.ToString();
        personid.Text = Session["ID"].ToString();







    }


  



    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeReward.aspx");

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}