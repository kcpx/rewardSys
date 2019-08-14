<%@ Page Title="" Language="C#" MasterPageFile="~/CEOMasterPage.master" AutoEventWireup="true" CodeFile="AddValue.aspx.cs" Inherits="AddValue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="btn-aligncenter">
        <h1>ADD A NEW COMPANY VALUE</h1>
        <br />
        <br />
        <h2>Enter the Following Information:</h2>
    </div>

    <asp:Table ID="tblAddValue" runat="server" HorizontalAlign="Center" CssClass="table" Width="400">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="lblValueName" runat="server" class="auto-style1" Text="Value Name:"></asp:Label>

            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="txtValueName" runat="server" CssClass="textbox" ></asp:TextBox>
                
            </asp:TableCell>

            <asp:TableCell>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtValueName" ErrorMessage="*"></asp:RequiredFieldValidator>
                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtValueName" ValidationExpression="^[a-zA-Z\s']{0,50}$" Text="Value Name cannot be more than 50 characters!" CssClass="auto-style7" />--%>
            </asp:TableCell>

        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="lblValueDescription" runat="server" class="auto-style1" Text="ValueDescription:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="txtValueDescription" runat="server" CssClass="textbox"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtValueDescription" ErrorMessage="*"></asp:RequiredFieldValidator>
                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtValueDescription" ValidationExpression="^[a-zA-Z\s']{0,250}$" Text="Value Name cannot be more than 250 characters!" CssClass="auto-style7" />--%>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <div class="btn-aligncenter">
            <asp:Button ID="btnCommit" runat="server" Text="Add Value!" OnClick="BtnCommit_Click" CssClass="button" Width="25%" OnClientClick="if (!confirm('Please double check your entered data')) return false"></asp:Button>
    </div>
    
</asp:Content>

