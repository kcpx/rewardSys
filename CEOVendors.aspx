<%@ Page Title="" Language="C#" MasterPageFile="~/CEOMasterPage.master" AutoEventWireup="true" CodeFile="CEOVendors.aspx.cs" Inherits="CEOVendors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="btn-aligncenter">
        <h1>MANAGE VENDORS</h1>

        <p>
            <br />
            <asp:Button ID="addBtn" runat="server" Text="Add New Vendor" CssClass="button" PostBackUrl="~/CreateVendor.aspx"/>
    </p>
    <p>
    </p>
        <h1>CURRENT VENDORS</h1>

        <p>
            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="RewardProviderID" HeaderText="RewardProviderID" SortExpression="RewardProviderID" />
                    <asp:BoundField DataField="RewardProviderName" HeaderText="RewardProviderName" SortExpression="RewardProviderName" />
                    <asp:BoundField DataField="PhoneNumber" HeaderText="PhoneNumber" SortExpression="PhoneNumber" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" SelectCommand="SELECT        RewardProviderPool.RewardProviderID, RewardProvider.RewardProviderName, RewardProvider.PhoneNumber, RewardProvider.Email  
FROM            RewardProvider INNER JOIN
                         RewardProviderPool ON RewardProvider.RewardProviderID = RewardProviderPool.RewardProviderID
WHERE RewardProviderPool.Status='active' AND ([dbo].[RewardProviderPool].BusinessEntityID = @BusinessEntityID)">
                <SelectParameters>
                    <asp:SessionParameter Name="BusinessEntityID" SessionField="BusinessEntityID" />
                </SelectParameters>
            </asp:SqlDataSource>
    </p>
        <h1>REMOVE VENDORS</h1>

    <p>
        <asp:CheckBoxList ID="vendorList" runat="server" DataSourceID="SqlDataSource1" DataTextField="RewardProviderID" DataValueField="RewardProviderID">
        </asp:CheckBoxList>
    </p>
        <p>
            <asp:Button ID="removeBtn" runat="server" Text="Remove" CssClass="button" OnClick="removeBtn_Click"/>
    </p>
    </div>
</asp:Content>

