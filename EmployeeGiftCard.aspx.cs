using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Windows.Forms;
using System.Configuration;
using PayPal.Api;

namespace PayPal.Sample
{
    public partial class EmployeeGiftCard : BaseSamplePage
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



        protected void RadioButtonList1_SelectedIndexChanged1(object sender, EventArgs e)
        {



        }

        protected void Button1_Click(object sender, EventArgs e)
        {


            //string selectedValue = RadioButtonList1.SelectedValue;



            //Insert Receipt Into DataBase

            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = ConfigurationManager.ConnectionStrings["GroupProjectConnectionString"].ConnectionString;
            sc.Open();


            SqlCommand insertgcr = new SqlCommand();
            insertgcr.Connection = sc;
            insertgcr.CommandText = "INSERT INTO [dbo].[GiftCardReceipt]([GiftCardID],[PersonID],[PurchaseDate],[LastUpdated],[LastUpdatedBy],[ConfirmationNumber]) VALUES ( @GiftCardID, @PersonID, @PurchaseDate, @LastUpdated, @LastUpdatedBy, @ConfirmationNumber)";


            //string giftcardID = RadioButtonList1.SelectedValue;
            Random rnd = new Random();
            int confirmation = rnd.Next(5, 100);


            insertgcr.Parameters.AddWithValue("@GiftCardReceiptID", 1);
            //insertgcr.Parameters.AddWithValue("@GiftCardID", giftcardID);
            insertgcr.Parameters.AddWithValue("@PersonID", Session["ID"]);
            insertgcr.Parameters.AddWithValue("@PurchaseDate", DateTime.Now);
            insertgcr.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
            insertgcr.Parameters.AddWithValue("@LastUpdatedby", Session["loggedIn"].ToString());
            insertgcr.Parameters.AddWithValue("@ConfirmationNumber", confirmation);

            insertgcr.ExecuteNonQuery();
            sc.Close();


            Response.Redirect("EmployeeReciept.aspx");
            SqlCommand getItBoy = new SqlCommand();
            //getItBoy.CommandText = "select "
            //Session["amountForGiftCard"] = 
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


    }
}