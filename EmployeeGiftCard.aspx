<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeGiftCard.aspx.cs" Inherits="PayPal.Sample.EmployeeGiftCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label1" runat="server" style="z-index: 1; left: 508px; top: 163px; position: absolute; width: 181px" Text="Select Gift Card"></asp:Label>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <h1>
        GIFT CARDS</h1>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" style="height: 429px; width: 777px; z-index: 1; left: 512px; top: 194px; position: absolute">
        <Columns>
            <asp:BoundField DataField="RewardProviderName" HeaderText="RewardProviderName" SortExpression="RewardProviderName" />
            <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" DataFormatString="${0:#.00}" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:BoundField DataField="DatePosted" HeaderText="DatePosted" SortExpression="DatePosted" DataFormatString="{0:d}" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" SelectCommand="SELECT GiftCard.ImageGC, RewardProvider.RewardProviderName, GiftCard.Value, GiftCard.Description, GiftCard.DatePosted FROM GiftCard INNER JOIN RewardProvider ON GiftCard.RewardProviderID = RewardProvider.RewardProviderID
WHERE GiftCard.Status = 'active'"></asp:SqlDataSource>
    <asp:RadioButtonList ID="RadioButtonList1" runat="server" DataSourceID="SqlDataSource2" DataTextField="GiftCardID" DataValueField="GiftCardID" style="z-index: 1; left: 318px; top: 199px; position: absolute; height: 425px; width: 184px">
    </asp:RadioButtonList>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" SelectCommand="SELECT GiftCard.GiftCardID FROM GiftCard INNER JOIN RewardProvider ON GiftCard.RewardProviderID = RewardProvider.RewardProviderID
WHERE GiftCard.Status = 'active'"></asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="z-index: 1; left: 790px; top: 701px; position: absolute; width: 174px" Text="Retrieve Gift Card!" />
    </asp:Content>

