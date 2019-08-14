<%@ Page Title="" Language="C#" MasterPageFile="~/CEOMasterPage.master" AutoEventWireup="true" CodeFile="CreateVendor.aspx.cs" Inherits="CreateVendor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="btn-aligncenter">
        <h1>ADD A NEW REWARD PROVIDER</h1>

        <asp:ListBox ID="ListBox1" runat="server" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" SelectionMode="Multiple" DataSourceID="SqlDataSource1" DataTextField="Company Name" DataValueField="Company ID"></asp:ListBox>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" SelectCommand="Select [dbo].[BusinessEntity].[BusinessEntityID] as &quot;Company ID&quot;, [BusinessEntityName] as &quot;Company Name&quot; from [dbo].[BusinessEntity] inner join [dbo].[Person] on [dbo].[BusinessEntity].BusinessEntityID = [dbo].[Person].BusinessEntityID
Where ([PersonID] = @PersonID);">
            <selectparameters>
		<asp:sessionparameter name="PersonID" sessionfield="PersonID" type="Int32" />
	</selectparameters>
        </asp:SqlDataSource>
        

        <asp:Table ID="Table1" runat="server" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Label1" runat="server" class="auto-style1" Text="Company Name:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="CompanyNameText" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="CompanyNameText" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="CompanyNameText" ValidationExpression="^[a-zA-Z\s']{0,20}$" Text="First name can not be more than 20 alphabetic characters in length" CssClass="auto-style7" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Label3" runat="server" class="auto-style1" Text="Phone Number:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="PhoneNumberText" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><%--<asp:TableCell>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtLastName" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z\s']{0,30}$" Text="Last name can not be more than 30 alphabetic characters in length" CssClass="auto-style7" />
                </asp:TableCell>--%></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Label4" runat="server" class="auto-style1" Text="Email Address:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="VendorEmailText" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="VendorEmailText" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                        ControlToValidate="VendorEmailText" ErrorMessage="Please enter corect email. For example john@example.com"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </asp:TableCell></asp:TableRow></asp:Table><div class="table">
            <div class="btn-aligncenter">
                <asp:Button ID="Button1" runat="server" Text="Add Reward Provider " OnClick="BtnCommitRewardProvider_Click" CssClass="button" Width="25%" OnClientClick="if (!confirm('Please double check your entered data')) return false"></asp:Button>

            </div>
        </div>
    </div>
</asp:Content>

