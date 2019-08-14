using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.Windows.Forms;

public partial class CEOExportExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindGrid();
            this.BindGrid1();
        }
        if (Session["loggedIn"] == null)
        {
            Response.Redirect("default.aspx");
        }

    }
    private void BindGrid()
    {
        string strConnString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM GiftCard where BusinessEntityID = @BID"))

            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@BID", Session["BusinessEntityID"]);

                    sda.SelectCommand = cmd;



                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();

                    }
                }
            }
        }
    }


    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.BindGrid();
    }

    private void BindGrid1()
    {
        string strConnString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Person where BusinessEntityID = @BID"))

            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@BID", Session["BusinessEntityID"]);

                    sda.SelectCommand = cmd;



                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridView2.DataSource = dt;
                        GridView2.DataBind();

                    }
                }
            }
        }
    }


    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=GiftCardsExport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            GridView1.AllowPaging = false;
            this.BindGrid();

            GridView1.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in GridView1.HeaderRow.Cells)
            {
                cell.BackColor = GridView1.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridView1.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    protected void ExportToExcel2(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=EmployeesExport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            GridView2.AllowPaging = false;
            this.BindGrid();

            GridView1.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in GridView2.HeaderRow.Cells)
            {
                cell.BackColor = GridView2.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in GridView2.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridView2.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridView2.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            GridView2.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
        /* Verifies that the control is rendered */
    }



    protected void btnImport_Click(object sender, EventArgs e)
    {
        //OpenFileDialog openFileDialog1 = new OpenFileDialog();
        //if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //{
            ImportDataFromExcel(Path.GetFullPath("Book1.xlsx"));
        //}
        
    }
    public void ImportDataFromExcel(string excelFilePath)
    {
        //declare variables - edit these based on your particular situation 
        string ssqltable = "Person";
        // make sure your sheet name is correct, here sheet name is sheet1, so you can change your sheet name if have    different 
        string myexceldataquery = "select personid,firstname,lastname,mi,nickname,email,position,password,useranme,pointsbalance,pendingpoints,lastupdated,lastupdatedby,profilepicture,logincount,pageland,anonymous,tempname,businessentityid,status from [Sheet1$]";
        try
        {
            //create our connection strings 
            string sexcelconnectionstring = @"provider=microsoft.jet.oledb.4.0;data source=" + excelFilePath +
            ";extended properties=" + "\"excel 8.0;hdr=yes;\"";
            //string ssqlconnectionstring = "Data Source=SAYYED;Initial Catalog=SyncDB;Integrated Security=True";
            //execute a query to erase any previous data from our destination table 
            //string sclearsql = "delete from " + ssqltable;
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            //series of commands to bulk copy data from the excel file into our sql table 
            OleDbConnection oledbconn = new OleDbConnection(sexcelconnectionstring);
            //oledbconn.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            Label1.Text = "failed";
            OleDbCommand oledbcmd = new OleDbCommand(myexceldataquery, oledbconn);
            oledbconn.Open();
            OleDbDataReader dr = oledbcmd.ExecuteReader();
            SqlBulkCopy bulkcopy = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString);
            
            bulkcopy.DestinationTableName = ssqltable;
            while (dr.Read())
            {
                bulkcopy.WriteToServer(dr);
            }
            dr.Close();
            oledbconn.Close();
            sc.Close();
            Label1.Text = "File imported into sql server.";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.ToString();
        }
    }
}