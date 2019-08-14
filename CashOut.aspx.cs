using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Windows.Forms;
using PayPal.Sample.Utilities;
using PayPal.Api;
using System.Net.Mail;
//using static System.Net.WebRequestMethods;

namespace PayPal.Sample
{
    public partial class CashOut : BaseSamplePage
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
            if (!IsPostBack)
            {
                Session["CashoutAmount"] = null;
                rewardpool();
            }
        }

        protected void cashout_Click(object sender, EventArgs e)
        {
            if (Session["CashoutAmount"] == null)
            {
                Response.Write("<script>alert('Please select Reward Amount you want to get')</script>");

            }
            else
            {
                SqlConnection sc = new SqlConnection();
                sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
                sc.Open();
                SqlCommand insert = new SqlCommand();
                insert.Connection = sc;

                insert.CommandText = "SELECT [TotalAmount] FROM [MoneyTransaction] where MoneyTransactionID=(select max(MoneyTransactionID) from MoneyTransaction)";
                SqlDataReader reader = insert.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    int totalPoints = Convert.ToInt32(reader["TotalAmount"]);
                    reader.Close();
                    int sessionID = (int)(Session["ID"]);
                    SqlCommand trans = new SqlCommand("select value from giftcard where giftcardID = @giftcardID");
                    trans.Parameters.AddWithValue("@giftcardID", rblcashout.SelectedValue);
                    trans.Connection = sc;
                    SqlDataReader transReader = trans.ExecuteReader();
                    transReader.Read();
                    Session["transAmount"] = Convert.ToInt32(transReader["value"]); 
                    int transactionAmount = (int)(Session["transAmount"]);
                    transReader.Close();
                    if (totalPoints >= transactionAmount && (Convert.ToInt32(Session["PointsBalance"]) > transactionAmount))
                    {
                        MoneyTransaction newTransaction = new MoneyTransaction(totalPoints, DateTime.Today.ToShortDateString(), transactionAmount, DateTime.Today.ToShortDateString(), Session["loggedIn"].ToString(), Convert.ToInt32(Session["ID"]));
                        insert.CommandText = "INSERT INTO [dbo].[MoneyTransaction] ([Date],[TotalAmount],[TransactionAmount],[LastUpdated],[LastUpdatedBy],[PersonID])" +
                        "VALUES (@Date,@TotalAmount,@TransactionAmount,@LastUpdated,@LastUpdatedBy,@PersonID)";
                        insert.Parameters.AddWithValue("@TotalAmount", totalPoints - transactionAmount);
                        insert.Parameters.AddWithValue("@Date", newTransaction.getDate());
                        insert.Parameters.AddWithValue("@TransactionAmount", newTransaction.getTransactionAmount());
                        insert.Parameters.AddWithValue("@LastUpdated", newTransaction.getLUD());
                        insert.Parameters.AddWithValue("@LastUpdatedBy", newTransaction.getLUDB());
                        insert.Parameters.AddWithValue("@PersonID", newTransaction.getPersonID());
                        insert.ExecuteNonQuery();
                        //amount to pay billyjacks son, this is 
                        Session["payProvider"] = newTransaction.getTransactionAmount();
                        Session["providerToPaypal"] = "billyJacks@gmail.com";//sql statement to grab email of the provider of giftcard based on provider name of card - from/in table provider
                        //
                        string payp = Session["PointsBalance"].ToString();
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

                            value = payp,//paypalAmount.ToString(),
                            currency = "USD"
                        },
                        receiver = "484billyjacks@gmail.com",//Session["providerToPaypal"].ToString(),
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
                        //RunSample();

                        //employee receipt
                        SqlCommand insertgcr = new SqlCommand();
                        insertgcr.Connection = sc;
                        insertgcr.CommandText = "INSERT INTO [dbo].[GiftCardReceipt]([GiftCardID],[PersonID],[PurchaseDate],[LastUpdated],[LastUpdatedBy],[ConfirmationNumber]) VALUES ( @GiftCardID, @PersonID, @PurchaseDate, @LastUpdated, @LastUpdatedBy, @ConfirmationNumber)";


                        string giftcardID = rblcashout.SelectedValue;
                        Random rnd = new Random();
                        int confirmation = rnd.Next(5, 100);


                        insertgcr.Parameters.AddWithValue("@GiftCardReceiptID", 1);
                        insertgcr.Parameters.AddWithValue("@GiftCardID", giftcardID);
                        insertgcr.Parameters.AddWithValue("@PersonID", Session["ID"]);
                        insertgcr.Parameters.AddWithValue("@PurchaseDate", DateTime.Now);
                        insertgcr.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
                        insertgcr.Parameters.AddWithValue("@LastUpdatedby", Session["loggedIn"].ToString());
                        insertgcr.Parameters.AddWithValue("@ConfirmationNumber", confirmation);

                        insertgcr.ExecuteNonQuery();
                        


                        Response.Redirect("EmployeeReciept.aspx");
                        SqlCommand getItBoy = new SqlCommand();

                        //employee receipt end
                        sc.Close();
                        Response.Write("<script>alert('Transaction Submited')</script>");

                        rewardpool();
                    }
                    else
                    {
                        Response.Write("<script>alert('personal points not enough or Bank balance low')</script>");
                    }
                    SampleItem newPayout = new SampleItem { Title = "Create a payout", ExecutePage = "PayoutCreate.aspx", HasSourcePage = true };
                }


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
                            
                            value = "25",//paypalAmount.ToString(),
                            currency = "USD"
                        },
                        receiver = "484billyjacks@gmail.com",//Session["providerToPaypal"].ToString(),
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
        protected void rblcashout_SelectedIndexChanged(object sender, EventArgs e)
        {
            //changed shit to grab the value of selected card 
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand cardAmountCheck = new SqlCommand();
            cardAmountCheck.Connection = sc;
            cardAmountCheck.Parameters.AddWithValue("@giftcardid", rblcashout.SelectedValue);
            cardAmountCheck.CommandText = "select value from giftcard where giftcardid = @giftcardid";
            Session["CashoutAmount"] = cardAmountCheck.ExecuteScalar().ToString();
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
            Session["PointsBalance"] = Convert.ToInt32(reader["PointsBalance"]);
            lblPoints.Text = "Your Current Points Balance: " + Session["PointsBalance"].ToString();
        }

        public void Send_Mail(string password)
        {
            string email = "darrenwoodwarda@gmail.com";
            string Name = "Company Name";
            
            String message = "Dear Employee: \n";
            message += "Your CEO has Created an Account for you!!\n";
            message += "Please login with UserName and Password provides below:\n";
            message += "UserName:  " + Name + "\n PassWord: " + password + "\n";
            MailMessage mail = new MailMessage("elkmessage@gmail.com", email, "Your Account Has been Created", message);
            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("elkmessage@gmail.com", "javapass");
            client.Host = "smtp.gmail.com";
            client.Send(mail);
        }

        protected void getPayPal_Click(object sender, EventArgs e)
        {
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();
            SqlCommand insert = new SqlCommand();
            insert.Connection = sc;

            insert.CommandText = "SELECT [TotalAmount] FROM [MoneyTransaction] where MoneyTransactionID=(select max(MoneyTransactionID) from MoneyTransaction)";
            SqlDataReader reader = insert.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                int totalPoints = Convert.ToInt32(reader["TotalAmount"]);
                reader.Close();
                int sessionID = (int)(Session["ID"]);
                
              
               
                int transactionAmount = 25;
                
                if (totalPoints >= transactionAmount && (Convert.ToInt32(Session["PointsBalance"]) > transactionAmount))
                {
                    MoneyTransaction newTransaction = new MoneyTransaction(totalPoints, DateTime.Today.ToShortDateString(), transactionAmount, DateTime.Today.ToShortDateString(), Session["loggedIn"].ToString(), Convert.ToInt32(Session["ID"]));
                    insert.CommandText = "INSERT INTO [dbo].[MoneyTransaction] ([Date],[TotalAmount],[TransactionAmount],[LastUpdated],[LastUpdatedBy],[PersonID])" +
                    "VALUES (@Date,@TotalAmount,@TransactionAmount,@LastUpdated,@LastUpdatedBy,@PersonID)";
                    insert.Parameters.AddWithValue("@TotalAmount", totalPoints - transactionAmount);
                    insert.Parameters.AddWithValue("@Date", newTransaction.getDate());
                    insert.Parameters.AddWithValue("@TransactionAmount", newTransaction.getTransactionAmount());
                    insert.Parameters.AddWithValue("@LastUpdated", newTransaction.getLUD());
                    insert.Parameters.AddWithValue("@LastUpdatedBy", newTransaction.getLUDB());
                    insert.Parameters.AddWithValue("@PersonID", newTransaction.getPersonID());
                    insert.ExecuteNonQuery();
                    //amount to pay billyjacks son, this is 
                    Session["payProvider"] = newTransaction.getTransactionAmount();
                    Session["providerToPaypal"] = "484person@gmail.com";//sql statement to grab email of the provider of giftcard based on provider name of card - from/in table provider
                }
            }                                                     
            SqlCommand insertgcr = new SqlCommand();
            insertgcr.Connection = sc;
            insertgcr.CommandText = "INSERT INTO [dbo].[GiftCardReceipt]([GiftCardID],[PersonID],[PurchaseDate],[LastUpdated],[LastUpdatedBy],[ConfirmationNumber]) VALUES ( @GiftCardID, @PersonID, @PurchaseDate, @LastUpdated, @LastUpdatedBy, @ConfirmationNumber)";


            string giftcardID = "9";
            Random rnd = new Random();
            int confirmation = rnd.Next(5, 100);
            string password = "58210";

            insertgcr.Parameters.AddWithValue("@GiftCardReceiptID", 1);
            insertgcr.Parameters.AddWithValue("@GiftCardID", Int32.Parse(giftcardID));
            insertgcr.Parameters.AddWithValue("@PersonID", Int32.Parse(Session["ID"].ToString()));
            insertgcr.Parameters.AddWithValue("@PurchaseDate", DateTime.Now);
            insertgcr.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
            insertgcr.Parameters.AddWithValue("@LastUpdatedby", Session["loggedIn"].ToString());
            insertgcr.Parameters.AddWithValue("@ConfirmationNumber", Int32.Parse(password));

            insertgcr.ExecuteNonQuery();

            SqlCommand pointsToPayPal = new SqlCommand("select PointsBalance from person where personID = @personID");
            pointsToPayPal.Parameters.AddWithValue("@personID", Session["ID"]);
            pointsToPayPal.Connection = sc;
            Session["payProvider"] = pointsToPayPal.ExecuteScalar().ToString();
            SqlCommand emailToPayPal = new SqlCommand("select 'e-mail' from person where personID = @personID");
            emailToPayPal.Parameters.AddWithValue("@personID", Session["ID"]);
            emailToPayPal.Connection = sc;
            Session["providerToPaypal"] = "484person@gmail.com";//emailToPayPal.ExecuteScalar().ToString();
            //RunSample();
            Send_Mail(password);
            Response.Redirect("EmployeeReciept.aspx");

            SqlCommand resetPoints = new SqlCommand("update person set [PointsBalance] = '0' where personid = @personID");
            resetPoints.Parameters.AddWithValue("@personID", Session["personID"]);
            resetPoints.Connection = sc;
            resetPoints.ExecuteNonQuery();

            //do we need the amount recorded in money transaction?

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

                            value = "25",//paypalAmount.ToString(),
                            currency = "USD"
                        },
                        receiver = "484person@gmail.com",//Session["providerToPaypal"].ToString(),
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
        }
    }
}