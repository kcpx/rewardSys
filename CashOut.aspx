<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true" CodeFile="CashOut.aspx.cs" Inherits="PayPal.Sample.CashOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

        <asp:Label runat="server" ID="lblPoints"></asp:Label>
        <div>    <asp:RadioButtonList ID="rblcashout" runat="server" RepeatDirection="Horizontal" Width="150px" OnSelectedIndexChanged="rblcashout_SelectedIndexChanged" DataSourceID="rbl" DataTextField="GiftCardID" DataValueField="GiftCardID" style="z-index: 1; left: 215px; top: 80px; position: absolute; height: 297px; width: 150px">
                                <asp:ListItem Value="10">Cash Out $10</asp:ListItem>

                                <asp:ListItem Value="15">Cash Out $15</asp:ListItem>

                                <asp:ListItem Value="25">Cash Out $25</asp:ListItem>

                            </asp:RadioButtonList>
          
  
            <asp:SqlDataSource ID="rbl" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" SelectCommand="SELECT GiftCard.GiftCardID FROM GiftCard INNER JOIN RewardProvider ON GiftCard.RewardProviderID = RewardProvider.RewardProviderID
WHERE GiftCard.Status = 'active'"></asp:SqlDataSource>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="GiftCardID" DataSourceID="gridview" ForeColor="#333333" GridLines="None" style="z-index: 1; left: 377px; top: 49px; position: absolute; height: 128px; width: 217px">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="GiftCardID" HeaderText="GiftCardID" InsertVisible="False" ReadOnly="True" SortExpression="GiftCardID" />
                    <asp:BoundField DataField="RewardProviderName" HeaderText="RewardProviderName" SortExpression="RewardProviderName" />
                    <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" DataFormatString="${0:#.00}" />
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                    <asp:BoundField DataField="DatePosted" HeaderText="DatePosted" SortExpression="DatePosted" DataFormatString="{0:d}" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:SqlDataSource ID="gridview" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" SelectCommand="SELECT GiftCard.GiftCardID, GiftCard.ImageGC, RewardProvider.RewardProviderName, GiftCard.Value, GiftCard.Description, GiftCard.DatePosted FROM GiftCard INNER JOIN RewardProvider ON GiftCard.RewardProviderID = RewardProvider.RewardProviderID
WHERE GiftCard.Status = 'active'"></asp:SqlDataSource>
          
  
    <asp:Button runat="server" ID="cashout" onclick="cashout_Click" style="z-index: 1; left: 373px; top: 404px; position: absolute" Text="Purchase Selected Card" />
            <asp:Button ID="getPayPal" runat="server" OnClick="getPayPal_Click" style="z-index: 1; left: 630px; top: 404px; position: absolute" Text="Turn Points into PayPal Cash" />
   </div>

</asp:Content>





<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

