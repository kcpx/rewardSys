<%@ Page Title="" Language="C#" MasterPageFile="~/CEOMasterPage.master" AutoEventWireup="true" CodeFile="CEOExportExcel.aspx.cs" Inherits="CEOExportExcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:GridView ID="GridView1" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
    RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
    runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="OnPageIndexChanging">
<AlternatingRowStyle BackColor="White" ForeColor="#000000"></AlternatingRowStyle>
    <Columns>
        <asp:BoundField DataField="GiftCardID" HeaderText="GiftCardID" ItemStyle-Width="150px" >
<ItemStyle Width="150px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="RewardProviderID" HeaderText="RewardProviderID" ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="Value" HeaderText="Value" ItemStyle-Width="100px" DataFormatString="${0:#.00}" >
<ItemStyle Width="100px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="DatePosted" HeaderText="Date Posted" ItemStyle-Width="100px" DataFormatString="{0:d}" >
<ItemStyle Width="100px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="DateRemoved" HeaderText="Date Removed" ItemStyle-Width="100px" DataFormatString="{0:d}" >
<ItemStyle Width="100px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="LastUpdated" HeaderText="Last Updated" ItemStyle-Width="100px" DataFormatString="{0:d}" >
<ItemStyle Width="100px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="LastUpdatedBy" HeaderText="Last Updated By" ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="VoucherNumber" HeaderText="Voucher Number" ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
        </asp:BoundField>
    </Columns>

<HeaderStyle BackColor="#3AC0F2" ForeColor="White"></HeaderStyle>

<RowStyle BackColor="#A1DCF2"></RowStyle>
</asp:GridView>
<br />
<asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick = "ExportToExcel" />

<asp:GridView ID="GridView2" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
    RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
    runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="OnPageIndexChanging">
<AlternatingRowStyle BackColor="White" ForeColor="#000000"></AlternatingRowStyle>
    <Columns>
        <asp:BoundField DataField="PersonID" HeaderText="PersonID" ItemStyle-Width="150px" >
<ItemStyle Width="150px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="FirstName" HeaderText="First Name" ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="LastName" HeaderText="Last Name" ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="MI" HeaderText="MI" ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="NickName" HeaderText="Nick Name" ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="E-mail" HeaderText="E-mail" ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="Position" HeaderText="Position" ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="UserName" HeaderText="User Name" ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="PointsBalance" HeaderText="Points Balance" ItemStyle-Width="100px" DataFormatString="{0:#}" >
<ItemStyle Width="100px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField DataField="LoginCount" HeaderText="Login Count" ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
        </asp:BoundField>
    </Columns>

<HeaderStyle BackColor="#3AC0F2" ForeColor="White"></HeaderStyle>

<RowStyle BackColor="#A1DCF2"></RowStyle>
</asp:GridView>
<br />
<asp:Button ID="Button1" runat="server" Text="Export To Excel" OnClick = "ExportToExcel2" />



    <asp:Button ID="btnImport" runat="server" OnClick="btnImport_Click" style="z-index: 1; left: 216px; top: 747px; position: absolute" Text="Import Employees" />
    <asp:Label ID="Label1" runat="server" style="z-index: 1; left: 424px; top: 750px; position: absolute" Text="Label"></asp:Label>



</asp:Content>

