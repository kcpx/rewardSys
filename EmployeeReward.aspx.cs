using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using PayPal.Sample.Utilities;
using PayPal.Api;

namespace PayPal.Sample
{
    public partial class EmployeeReward : BaseSamplePage
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
            else
            {
                rewardpool();
                LatestUpdates();
                updatecombox();
                updatecombonickname();
                if (!IsPostBack)
                {
                    reset();
                }
                lblInfo.Text = "Welcome to Peer Review System! " + Session["FirstName"].ToString();
            }
        }

        [System.Web.Services.WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> SearchName(string prefixText, int count)
        {
            //Connect to Database
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            //Send Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sc;
            cmd.CommandText = "SELECT ([FirstName] + isnull([MI],'')+[LastName]) as RewardName FROM [dbo].[Person] where FirstName like '%' + @SearchText + '%' or LastName like '%' + @SearchText + '%'";
            //cmd.CommandText = "SELECT [FirstName] FROM[RewardSystemLab4].[dbo].[Person]  where FirstName like '%' + @SearchText + '%'";
            cmd.Parameters.AddWithValue("@SearchText", prefixText);


            List<string> NameLists = new List<string>();
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                NameLists.Add(sdr["RewardName"].ToString());
            }

            sc.Close();
            return NameLists;
        }

        protected void btnCommit_Click(object sender, EventArgs e)
        {
            if (Session["ReceiverID"] == null || Session["ValueID"] == null || Session["CategoryID"] == null || Session["RewardAmount"] == null)
            {
                Response.Write("<script>alert('Please select Receiver Name,Value and Category')</script>");
                popReward.Show();
            }
            else if (Session["ValueID"].ToString() == "-1" || Session["CategoryID"].ToString() == "-1")
            {
                Response.Write("<script>alert('Please select Receiver Name,Value and Category')</script>");
                popReward.Show();
            }
            else
            {
                double pointsAmount = Convert.ToDouble(rblRewardPoints.SelectedValue);
                string EventDate = "2/17/2018"; // add textbox to enter
                string EventDescription = txtRDescription.Text;
                string LastUpdated = DateTime.Now.ToShortDateString();
                string LastUpdatedBy = Session["loggedIn"].ToString();
                int ReceiverID = Convert.ToInt32(Session["ReceiverID"]);
                int RewarderID = Convert.ToInt32(Session["ID"]);
                int CategoryID = Convert.ToInt32(ddlRCategory.SelectedValue);
                int ValueID = Convert.ToInt32(ddlRValue.SelectedValue);

                //try
                //{
                //Connect to Database
                SqlConnection sc = new SqlConnection();
                sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
                sc.Open();
                PeerTranscation emp = new PeerTranscation(pointsAmount, EventDate, EventDescription, LastUpdated, LastUpdatedBy, ReceiverID, RewarderID, CategoryID, ValueID);

                string sqlString = "INSERT INTO [dbo].[PeerTransaction]([PointsAmount],[Date],[EventDescription],[LastUpdated],[LastUpdatedBy],[ReceiverID],[RewarderID],[CategoryID],[ValueID]) VALUES (@PointsAmount,@Date,@EventDescription,@LastUpdated,@LastUpdatedBy,@ReceiverID,@RewarderID,@CategoryID,@ValueID)";

                SqlCommand insert = new SqlCommand(sqlString);
                insert.Connection = sc;

                insert.Parameters.AddWithValue("@PointsAmount", emp.getPoints());
                insert.Parameters.AddWithValue("@Date", emp.getDate());
                insert.Parameters.AddWithValue("@EventDescription", emp.getDescription());
                insert.Parameters.AddWithValue("@LastUpdated", emp.getLUD());
                insert.Parameters.AddWithValue("@LastUpdatedBy", emp.getLUDB());
                insert.Parameters.AddWithValue("@ReceiverID", emp.getReceiverID());
                insert.Parameters.AddWithValue("@RewarderID", emp.getRewarderID());
                insert.Parameters.AddWithValue("@CategoryID", emp.getCategoryID());
                insert.Parameters.AddWithValue("@ValueID", emp.getValueID());
                //set up paypal variables

                string getEmBoy = "SELECT 'e-mail' from person where personID = @receiverID";
                SqlCommand getReceiver = new SqlCommand(getEmBoy);
                getReceiver.Connection = sc;
                getReceiver.Parameters.AddWithValue("@ReceiverID", emp.getReceiverID());
                Session["whoToPaypal"] = getReceiver.ExecuteNonQuery().ToString();
                Session["amountToPaypal"] = emp.getPoints();
                
                //new payout based on the reward given baby
                //KYLEEEEEEEEEEEEEEEEEEE

                SampleItem newPayout = new SampleItem { Title = "Create a payout", ExecutePage = "PayoutCreate.aspx", HasSourcePage = true };
                
                //RunSample(); //this is the line that executes paypal

                //string whatIsThis = newPayout;
                //
                //
                //
                //var payout = new Payout
                //{
                //    // #### sender_batch_header
                //    // Describes how the payments defined in the `items` array are to be handled.
                //    sender_batch_header = new PayoutSenderBatchHeader
                //    {
                //        sender_batch_id = "batch_" + System.Guid.NewGuid().ToString().Substring(0, 8),
                //        email_subject = "You have a payment"
                //    },
                //    // #### items
                //    // The `items` array contains the list of payout items to be included in this payout.
                //    // If `syncMode` is set to `true` when calling `Payout.Create()`, then the `items` array must only
                //    // contain **one** item.  If `syncMode` is set to `false` when calling `Payout.Create()`, then the `items`
                //    // array can contain more than one item.
                //    items = new List<PayoutItem>
                //{
                //    new PayoutItem
                //    {
                //        recipient_type = PayoutRecipientType.EMAIL,
                //        amount = new Currency
                //        {

                //            value = "0.99",//PeerTransaction.getTransaction().getAmount().toString();,
                //            currency = "USD"
                //        },
                //        receiver = "484person@gmail.com",
                //        note = "Thank you.",
                //        sender_item_id = "item_1"
                //    }
                //}
                //};
                ////string token = payout.GetTokenFromApprovalUrl();
                ////payout.Create(Configuration.GetAPIContext(), true);


                //
                //
                //
                //

                insert.ExecuteNonQuery();
                sc.Close();
                //}


                //catch
                //{
                //    lblOutput.Text = "Fail add to database";
                //}
                LatestUpdates();
                reset();
                Response.Write("<script>alert('Peer reward Successful')</script>");
            }
        }



        protected override void RunSample()
        {
            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            // See [Configuration.cs](/Source/Configuration.html) to know more about APIContext.
            var apiContext = Configuration.GetAPIContext();

            // ### Initialize `Payout` Object
            // Initialize a new `Payout` object with details of the batch payout to be created.
            var payout = new Payout
            {
                // #### sender_batch_header
                // Describes how the payments defined in the `items` array are to be handled.
                sender_batch_header = new PayoutSenderBatchHeader
                {
                    sender_batch_id = "batch_" + System.Guid.NewGuid().ToString().Substring(0, 8),
                    email_subject = "You have a payment"
                },
                // #### items
                // The `items` array contains the list of payout items to be included in this payout.
                // If `syncMode` is set to `true` when calling `Payout.Create()`, then the `items` array must only
                // contain **one** item.  If `syncMode` is set to `false` when calling `Payout.Create()`, then the `items`
                // array can contain more than one item.
                items = new List<PayoutItem>
                {
                    new PayoutItem
                    {
                        recipient_type = PayoutRecipientType.EMAIL,
                        amount = new Currency
                        {

                            value = Session["amountToPaypal"].ToString(),
                            currency = "USD"
                        },
                        receiver = "484person@gmail.com",//Session["whoToPaypal"].ToString(),
                        note = "Thank you.",
                        sender_item_id = "item_1"
                    }//,
                    //new PayoutItem
                    //{
                    //    recipient_type = PayoutRecipientType.EMAIL,
                    //    amount = new Currency
                    //    {
                    //        value = "0.90",
                    //        currency = "USD"
                    //    },
                    //    receiver = "shirt-supplier-two@mail.com",
                    //    note = "Thank you.",
                    //    sender_item_id = "item_2"
                    //},
                    //new PayoutItem
                    //{
                    //    recipient_type = PayoutRecipientType.EMAIL,
                    //    amount = new Currency
                    //    {
                    //        value = "2.00",
                    //        currency = "USD"
                    //    },
                    //    receiver = "shirt-supplier-three@mail.com",
                    //    note = "Thank you.",
                    //    sender_item_id = "item_3"
                    //}
                }
            };

            // ^ Ignore workflow code segment
            //#region Track Workflow
            //this.flow.AddNewRequest("Create payout", payout);
            //#endregion

            // ### Payout.Create()
            // Creates the batch payout resource.
            // `syncMode = false` indicates that this call will be performed **asynchronously**,
            // and will return a `payout_batch_id` that can be used to check the status of the payouts in the batch.
            // `syncMode = true` indicates that this call will be performed **synchronously** and will return once the payout has been processed.
            // > **NOTE**: The `items` array can only have **one** item if `syncMode` is set to `true`.
            var createdPayout = payout.Create(apiContext, false);

            // ^ Ignore workflow code segment
            //#region Track Workflow
            //this.flow.RecordResponse(createdPayout);
            //#endregion

            // For more information, please visit [PayPal Developer REST API Reference](https://developer.paypal.com/docs/api/).
        }

        private void LatestUpdates()
        {
            SqlConnection insertA = new SqlConnection();
            insertA.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            insertA.Open();
            SqlCommand insertDB1 = new SqlCommand();
            insertDB1.Connection = insertA;
            insertDB1.CommandText = "UPDATE [dbo].[Person] SET [TempName] = 'Anonymous' where [Anonymous]='Yes' ";
            insertDB1.ExecuteNonQuery();
            insertA.Close();

            SqlConnection insertB = new SqlConnection();
            insertB.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            insertB.Open();
            SqlCommand insertDB2 = new SqlCommand();
            insertDB2.Connection = insertB;
            insertDB2.CommandText = "UPDATE [dbo].[Person] SET [TempName] = [FirstName] where ([Anonymous] is null) or ([Anonymous] = 'No')";
            insertDB2.ExecuteNonQuery();
            insertB.Close();


            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            DataTable dt = new DataTable();
            string getpost = "SELECT PeerTransaction.Date, PeerTransaction.EventDescription, PeerTransaction.LastUpdated, PeerTransaction.PointsAmount, " +
                "Person.TempName AS ReceiverName, Category.Title, Value.ValueName, Person_1.TempName AS RewarderName FROM PeerTransaction INNER JOIN  Person " +
                "ON PeerTransaction.ReceiverID = Person.PersonID INNER JOIN Value ON PeerTransaction.ValueID = Value.ValueID INNER JOIN Category " +
                "ON PeerTransaction.CategoryID = Category.CategoryID INNER JOIN Person AS Person_1 ON PeerTransaction.RewarderID = Person_1.PersonID " +
                "where(person.BusinessEntityID = " + Session["BusinessEntityID"] + ") and(Person_1.BusinessEntityID = " + Session["BusinessEntityID"] +
                ") and(person.Status = 'Active') and(Person_1.Status = 'Active') ORDER BY PeerTransaction.TransactionID DESC ";
            SqlCommand cmd = new SqlCommand(getpost, sc);
            SqlDataReader sdr = cmd.ExecuteReader();
            dlPosts.DataSource = sdr;
            dlPosts.DataBind();
            sdr.Close();
            sc.Close();
        }

        private void updatecombox()
        {
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();

            DataTable dt = new DataTable();
            string getName = "SELECT PersonID, ([FirstName] + isnull([MI], '') +[LastName]) as RewardName FROM[dbo].[Person] Where (Position != 'CEO')" +
                " and (Status='Active') and (BusinessEntityID=" + Session["BusinessEntityID"] + ") and (PersonID!=" + Session["ID"] + ")";
            SqlDataAdapter da = new SqlDataAdapter(getName, sc);
            da.Fill(dt);
            cbName.DataSource = dt;
            cbName.DataTextField = "RewardName";
            cbName.DataValueField = "PersonID";
            cbName.DataBind();
            sc.Close();
        }

        private void updatecombonickname()
        {
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();

            DataTable dtn = new DataTable();
            string getNickname = "SELECT PersonID, Nickname FROM [dbo].[Person] WHERE (Nickname IS NOT NULL) and (Status='Active') and " +
                "(BusinessEntityID=" + Session["BusinessEntityID"] + ") and (PersonID!=" + Session["ID"] + ") and (Position != 'CEO')";
            SqlDataAdapter da = new SqlDataAdapter(getNickname, sc);
            da.Fill(dtn);
            cbNickname.DataSource = dtn;
            cbNickname.DataTextField = "Nickname";
            cbNickname.DataValueField = "PersonID";
            cbNickname.DataBind();
            sc.Close();
        }

        private void rewardpool()
        {
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insert = new SqlCommand();
            insert.Connection = sc;
            insert.CommandText = "select pointsbalance from person where personID=@id";
            insert.Parameters.AddWithValue("@id", Session["ID"]);
            SqlDataReader reader = insert.ExecuteReader();
            reader.Read();
            Session["PointsBalance"] = Convert.ToInt32(reader["pointsbalance"]);
            lblPoints.Text = "Current Balance: " + Session["PointsBalance"].ToString();
        }

        protected void cbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ReceiverID"] = cbName.SelectedValue;
        }


        protected void cbNickName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ReceiverID"] = cbNickname.SelectedValue;
        }


        protected void ddlRValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ValueID"] = ddlRValue.SelectedValue;
        }

        protected void ddlRCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CategoryID"] = ddlRCategory.SelectedValue;
        }

        protected void rblRewardPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["RewardAmount"] = rblRewardPoints.SelectedValue;
        }

        protected void reset()
        {
            Session["ReceiverID"] = null;
            Session["Nickname"] = null;
            Session["ValueID"] = null;
            Session["CategoryID"] = null;
            Session["RewardAmount"] = null;
        }
    }
}