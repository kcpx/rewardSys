<%@ Page Language="C#"  MasterPageFile="~/Rewardprovider.master" AutoEventWireup="true" CodeFile="VendorAddGC.aspx.cs" Inherits="VendorAddGC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
     <div class="btn-aligncenter">
        <h1>SUBMIT A NEW GIFTCARD</h1>

         <asp:ListBox ID="ListBox1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Company" SelectionMode="Multiple" DataValueField="Company ID" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged"></asp:ListBox>
         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" SelectCommand="SELECT [dbo].[BusinessEntity].[BusinessEntityID] as &quot;Company ID&quot;, [BusinessEntityName] as &quot;Company&quot; from [dbo].[BusinessEntity] inner join [dbo].[RewardProviderPool] on [dbo].[BusinessEntity].BusinessEntityID = [dbo].[RewardProviderPool].BusinessEntityID 
where ([dbo].[RewardProviderPool].RewardProviderID = @VendorID) ;">
             <SelectParameters>
                 <asp:SessionParameter Name="VendorID" SessionField="VendorID" />
             </SelectParameters>
         </asp:SqlDataSource>

        <asp:Table ID="CreateEmployeefrom" runat="server" HorizontalAlign="Center" Height="200px">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblValue" runat="server" class="auto-style1" Text="GiftCard Value: "></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtValue" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblDescription" runat="server" Text="GiftCard Description: "></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtGCDescription" runat="server" MaxLength="250" TextMode="MultiLine" CssClass="textbox" style="overflow:hidden" Height="150px"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
               
                </asp:TableCell></asp:TableRow>
            <asp:TableRow>
               
                <asp:TableCell>
                    <asp:FileUpload ID="PictureUpload" runat="server" />
                    
                    <asp:Image ID="ProfilePicture" runat="server" Width="480" />
                </asp:TableCell>
                
            </asp:TableRow>

        </asp:Table><div class="table">
            <div class="btn-aligncenter">
                <asp:Button ID="btnCommit" runat="server" Text="Submit For Approval " OnClick="BtnCommit_Click" CssClass="button" Width="25%" OnClientClick="if (!confirm('Please double check your entered data')) return false"></asp:Button>
            </div>
        </div>
    </div>

    
</asp:Content>
