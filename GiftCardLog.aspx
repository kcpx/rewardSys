<%@ Page Title="" Language="C#" MasterPageFile="~/Rewardprovider.master" AutoEventWireup="true" CodeFile="GiftCardLog.aspx.cs" Inherits="GiftCardLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <h1>Gift Card Log</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <div class="btn-aligncenter">
    <p>
        <br />
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CssClass="table">
            <Columns>
                <asp:BoundField DataField="PurchaseDate" HeaderText="PurchaseDate" SortExpression="PurchaseDate" />
                <asp:BoundField DataField="GiftCardID" HeaderText="GiftCardID" SortExpression="GiftCardID" />
                <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" />
                <asp:BoundField DataField="RewardProviderName" HeaderText="RewardProviderName" SortExpression="RewardProviderName" />
            </Columns>
        </asp:GridView>
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="backBtn" runat="server" PostBackUrl="~/RPManageGiftCard.aspx" Text="Back" CssClass="button" />
    </p>
    <p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" SelectCommand="SELECT        GiftCardReceipt.PurchaseDate, GiftCardReceipt.GiftCardID, GiftCard.Value, RewardProvider.RewardProviderName
FROM            BusinessEntity INNER JOIN
                         GiftCard ON BusinessEntity.BusinessEntityID = GiftCard.BusinessEntityID INNER JOIN
                         GiftCardReceipt ON GiftCard.GiftCardID = GiftCardReceipt.GiftCardID INNER JOIN
                         RewardProvider ON GiftCard.RewardProviderID = RewardProvider.RewardProviderID INNER JOIN
                         RewardProviderPool ON BusinessEntity.BusinessEntityID = RewardProviderPool.BusinessEntityID AND RewardProvider.RewardProviderID = RewardProviderPool.RewardProviderID
WHERE ([dbo].[RewardProvider].RewardProviderID = @VendorID)">
            <SelectParameters>
                <asp:SessionParameter Name="VendorID" SessionField="@VendorID" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
        </div>
</asp:Content>

