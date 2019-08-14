using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayPal.Api;
/// <summary>
/// Summary description for PayoutCreate
/// </summary>
public class PayoutCreate
{
    public PayoutCreate()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void makePayout()
    {
        var apiContext = new APIContext
        {
            AccessToken = "access_token$sandbox$bbkjzq3wtwfym2jw$a45aed0297136131fdb782bb84a59b7c",
            Config = new Dictionary<string, string> { { "mode", "sandbox" } }
        };

        var payout = new Payout
        {
            sender_batch_header = new PayoutSenderBatchHeader
            {
                sender_batch_id = "batch_" + System.Guid.NewGuid().ToString().Substring(0, 8),
                email_subject = "You have a payment"
            },

            items = new List<PayoutItem>
                {
                    new PayoutItem
                    {
                        recipient_type = PayoutRecipientType.EMAIL,
                        amount = new Currency
                        {
                            value = "1.00",
                            currency = "USD"
                        },
                        receiver = "484person@gmail.com",
                        note = "Thank you.",
                        sender_item_id = "item_1"
                    },
                }
        };
        
    }
}
