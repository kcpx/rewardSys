<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeReciept.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    <asp:Label ID="Label2" runat="server" style="z-index: 1; left: 367px; top: 329px; position: absolute; height: 8px; width: 140px" Text="Employee Name:"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <asp:Label ID="Label3" runat="server" style="z-index: 1; left: 368px; top: 426px; position: absolute" Text="Date Activated: "></asp:Label>
    <asp:Label ID="labelgdate" runat="server" style="z-index: 1; left: 596px; top: 423px; position: absolute; width: 129px" Text="Label"></asp:Label>
    <asp:Button ID="printButton" runat="server" Text="Print Reciept" OnClientClick="javascript:window.print();" style="z-index: 1; left: 444px; top: 540px; position: absolute; width: 143px" />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" style="z-index: 1; left: 687px; top: 537px; position: absolute; width: 172px" Text="Return to HomePage" />
    <asp:Label ID="Label6" runat="server" style="z-index: 1; left: 366px; top: 373px; position: absolute; height: 27px; width: 215px" Text="GiftCard Confirmation Number "></asp:Label>
    <asp:Label ID="Label8" runat="server" style="z-index: 1; left: 371px; top: 290px; position: absolute; width: 129px" Text="Employee ID"></asp:Label>
    <asp:Label ID="personid" runat="server" style="z-index: 1; left: 597px; top: 284px; position: absolute" Text="Label"></asp:Label>
    <asp:Label ID="Label7" runat="server" style="z-index: 1; left: 412px; top: 143px; position: absolute; width: 386px" Text="Congradulations! Thank you for choosing a Gift Card from our partnered Providers. Details to retrieve your Gift Card has been sent to your email "></asp:Label>
    <asp:Label ID="confirmation" runat="server" style="z-index: 1; left: 596px; top: 375px; position: absolute; height: 18px; width: 39px" Text="Label"></asp:Label>
    <asp:Label ID="Label5" runat="server" style="z-index: 1; left: 517px; top: 92px; position: absolute; height: 22px; width: 255px; font-weight: 700" Text="Your Reciept"></asp:Label>
    <asp:Label ID="labelgName" runat="server" style="z-index: 1; left: 597px; top: 330px; position: absolute; width: 120px" Text="Label"></asp:Label>
    </asp:Content>
     
