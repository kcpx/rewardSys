<%@ Page Title="" Language="C#" MasterPageFile="~/CEOMasterPage.master" AutoEventWireup="true" CodeFile="AddGiftCard.aspx.cs" Inherits="AddGiftCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style2 {
            width: 55%;
            margin-left: 230px;
        }
        .auto-style4 {
            width: 831px;
        }
        .auto-style5 {
            width: 74%;
            text-align: center;
        }
        .auto-style6 {
            width: 868px;
            text-align: left;
        }
        .auto-style7 {
            width: 868px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>ADD GIFT CARD</h1>
    <table class="auto-style2">
        <tr>
            <td class="auto-style7">Select reward provider:</td>
            <td class="auto-style4">
                <asp:DropDownList ID="ProviderDrop" runat="server" DataSourceID="SqlDataSource1" DataTextField="RewardProviderName" DataValueField="RewardProviderName" Width="129px">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" SelectCommand="SELECT [RewardProviderName] FROM [RewardProvider]"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td class="auto-style6">Description (optional):</td>
            <td class="auto-style4">
                <asp:TextBox ID="DescriptionTxt" runat="server" Height="219px" Width="299px" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style6">Value:</td>
            <td class="auto-style4">
                <asp:TextBox ID="valueTxt" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
        </tr>
    </table>
    <p class="auto-style5">
        <asp:Button ID="SaveBtn" runat="server" Text="Save Gift Card" Width="25%" OnClick="SaveBtn_Click" CssClass="button" />
        
    </p>
    <p class="auto-style5">
        &nbsp;</p>
    <p class="auto-style5">
        <asp:Button ID="SaveBtn0" runat="server" Text="Back" Width="25%" PostBackUrl="~/CEOGiftCard.aspx" CssClass="button" />
    </p>
</asp:Content>

