<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminCreateCompany.aspx.cs" Inherits="AdminCreateCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="btn-aligncenter">
        <h1>New Company Information</h1>
    </div>
    
    <asp:Table ID="CreateCompany" runat="server" HorizontalAlign="Center" Height="200px">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblCompanyName" runat="server" class="auto-style1" Text="Company Name:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="textbox" Height="35px"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ID="CompanyNameReq" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtCompanyName" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="FirstNameMessage" runat="server" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z\s']{0,40}$" Text="Company name can not be more than 40 alphabetic characters in length" CssClass="auto-style7" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblPhoneNumber" runat="server" class="auto-style1" Text="Company Phone Number:"></asp:Label>
                   </asp:TableCell><asp:TableCell>  
                    <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="textbox" Height="35px"></asp:TextBox>
                </asp:TableCell>
   
                  <asp:TableCell>
                    <asp:RequiredFieldValidator ID="PhoneNumberReq" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtPhoneNumber" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="LastNameMessage" runat="server" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z\s']{0,10}$" Text="Phone Number can not be more that 10 Digits (No Brackets)" CssClass="auto-style7" />
                </asp:TableCell></asp:TableRow>
        <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblEmail" runat="server" class="auto-style1" Text="Company Email:"></asp:Label>
                   </asp:TableCell><asp:TableCell>  
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Height="35px"></asp:TextBox>
                </asp:TableCell>
   
                </asp:TableRow>

        </asp:Table><div class="table">
        <div class="btn-aligncenter">
            <h1>CEO Account For Company</h1>

        <asp:Table ID="CreateEmployeefrom" runat="server" HorizontalAlign="Center" Height="200px">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblFirstName" runat="server" class="auto-style1" Text="First Name:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="textbox" Height="35px"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ID="FirstNameRequire" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtFirstName" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z\s']{0,20}$" Text="First name can not be more than 20 alphabetic characters in length" CssClass="auto-style7" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblMI" runat="server" Text="Middle Initial:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtMI" runat="server" CssClass="textbox" Height="35px"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RegularExpressionValidator ID="MIMessage" runat="server"  ControlToValidate="txtMI" ValidationExpression="^[a-zA-Z\s]{0,1}$" Text="Middle name can not be more than 1 alphabetic character in length" CssClass="auto-style7" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblLastName" runat="server" class="auto-style1" Text="Last Name:"></asp:Label>
                   </asp:TableCell><asp:TableCell>  
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="textbox" Height="35px"></asp:TextBox>
                </asp:TableCell>
   
                  <asp:TableCell>
                    <asp:RequiredFieldValidator ID="LastNameRequire" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtLastName" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z\s']{0,30}$" Text="Last name can not be more than 30 alphabetic characters in length" CssClass="auto-style7" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblCeoEmail" runat="server" class="auto-style1" Text="CEO Email Address:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtCeoEmail" runat="server" CssClass="textbox" Height="35px"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ID="Required" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtCeoEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                        ControlToValidate="txtEmail" ErrorMessage="Please enter corect email. For example john@example.com"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </asp:TableCell></asp:TableRow>
        </asp:Table><div class="table">
            <div class="btn-aligncenter">
                <asp:Button ID="btnCommit" runat="server" Text="Add Company" OnClick="BtnCommit_Click" CssClass="button" Width="25%" OnClientClick="if (!confirm('Please double check your entered data')) return false"></asp:Button>

            </div>
            
        </div>
 

</asp:Content>

