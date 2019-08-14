<%@ Page Title="" Language="C#" MasterPageFile="~/Rewardprovider.master" AutoEventWireup="true" CodeFile="RPManageGiftCard.aspx.cs" Inherits="RPManageGiftCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div class="btn-aligncenter">
        <h1>MANAGE GIFT CARDS</h1>
    <p>&nbsp;</p>
    <p>
        <asp:Button ID="requestBtn" runat="server" Text="Request New Gift Card" CssClass="button" Width="50%" PostBackUrl="~/VendorAddGC.aspx"/>
    </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Button ID="logBtn" runat="server" Text="Gift Card Log" CssClass="button" Width="50%" PostBackUrl="~/GiftCardLog.aspx"/>
    </p>
        <h1>ACTIVE GIFT CARDS</h1>
    <p>
        &nbsp;</p>
    <p>
        <asp:GridView ID="GridView1" runat="server" CssClass="table" AutoGenerateColumns="False" DataKeyNames="GiftCardID" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="GiftCardID" HeaderText="GiftCardID" InsertVisible="False" ReadOnly="True" SortExpression="GiftCardID" />
                <asp:BoundField DataField="BusinessEntityName" HeaderText="BusinessEntityName" SortExpression="BusinessEntityName" />
                <asp:BoundField DataField="Value" DataFormatString="${0:#.00}" HeaderText="Value" SortExpression="Value" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="DatePosted" HeaderText="DatePosted" SortExpression="DatePosted" DataFormatString="{0:d}" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" SelectCommand="SELECT        GiftCard.GiftCardID, BusinessEntity.BusinessEntityName, GiftCard.Value, GiftCard.Description, GiftCard.DatePosted, GiftCard.ImageGC
FROM            BusinessEntity INNER JOIN
                         GiftCard ON BusinessEntity.BusinessEntityID = GiftCard.BusinessEntityID INNER JOIN
                         RewardProvider ON GiftCard.RewardProviderID = RewardProvider.RewardProviderID INNER JOIN
                         RewardProviderPool ON BusinessEntity.BusinessEntityID = RewardProviderPool.BusinessEntityID AND RewardProvider.RewardProviderID = RewardProviderPool.RewardProviderID
WHERE GiftCard.Status='active' AND ([dbo].[RewardProviderPool].RewardProviderID = @VendorID)">
            <SelectParameters>
                <asp:SessionParameter Name="VendorID" SessionField="VendorID" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
    <p>
        &nbsp;</p>
        <h1>PENDING GIFT CARDS</h1>
        <p>
            <asp:GridView ID="GridView2" runat="server" CssClass="table" AutoGenerateColumns="False" DataKeyNames="GiftCardID" DataSourceID="SqlDataSource2">
                <Columns>
                    <asp:BoundField DataField="GiftCardID" HeaderText="GiftCardID" InsertVisible="False" ReadOnly="True" SortExpression="GiftCardID" />
                    <asp:BoundField DataField="BusinessEntityName" HeaderText="BusinessEntityName" SortExpression="BusinessEntityName" />
                    <asp:BoundField DataField="Value" DataFormatString="${0:#.00}" HeaderText="Value" SortExpression="Value" />
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                    <asp:BoundField DataField="DatePosted" HeaderText="DatePosted" SortExpression="DatePosted" DataFormatString="{0:d}" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" SelectCommand="SELECT        GiftCard.GiftCardID, BusinessEntity.BusinessEntityName, GiftCard.Value, GiftCard.Description, GiftCard.DatePosted, GiftCard.ImageGC
FROM            BusinessEntity INNER JOIN
                         GiftCard ON BusinessEntity.BusinessEntityID = GiftCard.BusinessEntityID INNER JOIN
                         RewardProvider ON GiftCard.RewardProviderID = RewardProvider.RewardProviderID INNER JOIN
                         RewardProviderPool ON BusinessEntity.BusinessEntityID = RewardProviderPool.BusinessEntityID AND RewardProvider.RewardProviderID = RewardProviderPool.RewardProviderID
WHERE GiftCard.Status='pending'"></asp:SqlDataSource>
    </p>
    <p>
        &nbsp;</p>
        <h1>REMOVE GIFT CARDS</h1>
    <p>
        <asp:CheckBoxList ID="removeList" runat="server" DataSourceID="SqlDataSource3" DataTextField="GiftCardID" DataValueField="BusinessEntityName">
        </asp:CheckBoxList>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" SelectCommand="SELECT        GiftCard.GiftCardID, BusinessEntity.BusinessEntityName, GiftCard.Value, GiftCard.Description, GiftCard.DatePosted, GiftCard.ImageGC
FROM            BusinessEntity INNER JOIN
                         GiftCard ON BusinessEntity.BusinessEntityID = GiftCard.BusinessEntityID INNER JOIN
                         RewardProvider ON GiftCard.RewardProviderID = RewardProvider.RewardProviderID INNER JOIN
                         RewardProviderPool ON BusinessEntity.BusinessEntityID = RewardProviderPool.BusinessEntityID AND RewardProvider.RewardProviderID = RewardProviderPool.RewardProviderID
WHERE GiftCard.Status='active' OR GiftCard.Status='pending'"></asp:SqlDataSource>
        </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Button ID="removeBtn" runat="server" Text="Remove" CssClass="button" OnClick="removeBtn_Click"/>
        </p>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

