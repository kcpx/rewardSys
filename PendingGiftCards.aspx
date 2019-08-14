<%@ Page Title="" Language="C#" MasterPageFile="~/CEOMasterPage.master" AutoEventWireup="true" CodeFile="PendingGiftCards.aspx.cs" Inherits="PendingGiftCards" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>PENDING GIFT CARDS</h1>
    <p>&nbsp;</p>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="GiftCardID" DataSourceID="SqlDataSource1" CssClass="table">
            <Columns>
                <asp:BoundField DataField="GiftCardID" HeaderText="GiftCardID" InsertVisible="False" ReadOnly="True" SortExpression="GiftCardID" />
                <asp:BoundField DataField="RewardProviderName" HeaderText="RewardProviderName" SortExpression="RewardProviderName" />
                <asp:BoundField DataField="Value" DataFormatString="${0:#.00}" HeaderText="Value" SortExpression="Value" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
            </Columns>
        </asp:GridView>
        
    </p>
    <p>
        &nbsp;</p>
    <h1>SELECT GIFT CARDS</h1>
    <p>
        &nbsp;</p>
    <h1>
        <asp:CheckBoxList ID="pendingList" runat="server" DataSourceID="SqlDataSource1" DataTextField="GiftCardID" DataValueField="GiftCardID" CssClass="btn-forgot">
        </asp:CheckBoxList>
        
    </h1>
    <h1>
&nbsp;&nbsp; </h1>
    <h1>
        <asp:Button ID="acceptBtn" runat="server" OnClick="acceptBtn_Click" Text="Accept" Width="20%" CssClass="button" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="rejectBtn" runat="server" Text="Reject" Width="20%" CssClass="button" OnClick="rejectBtn_Click" />
    </h1>
    <h1>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" SelectCommand="SELECT        GiftCard.GiftCardID, RewardProvider.RewardProviderName, GiftCard.Value, GiftCard.Description, GiftCard.Status
FROM            GiftCard INNER JOIN
                         RewardProvider ON GiftCard.RewardProviderID = RewardProvider.RewardProviderID
WHERE GiftCard.Status = 'pending';"></asp:SqlDataSource>
    </h1>
    <p>
        <asp:Button ID="backBtn" runat="server" PostBackUrl="~/CEOGiftCard.aspx" Text="Back" Width="20%" CssClass="button" />
    </p>
</asp:Content>

