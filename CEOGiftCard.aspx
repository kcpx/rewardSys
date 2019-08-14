<%@ Page Title="" Language="C#" MasterPageFile="~/CEOMasterPage.master" AutoEventWireup="true" CodeFile="CEOGiftCard.aspx.cs" Inherits="CEOGiftCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="btn-aligncenter">
        <h1>MANAGE GIFT CARD STATUS</h1>
       <%-- <h1>

                <asp:Button ID="AddBtn" runat="server" Text="Pending Gift Cards" Width="45%" OnClick="AddGiftCard_Click" CssClass="button" PostBackUrl="~/PendingGiftCards.aspx" />
            </h1>--%>
        <h3>&nbsp;</h3>
        <h3>(Removed, Pending or Active)</h3>
        <p>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" DataKeyNames="GiftCardID" CssClass="table" EnablePersistedSelection="True" AutoGenerateEditButton="True" CellPadding="4" ForeColor="#333333" GridLines="None" AllowSorting="True">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="GiftCardID" HeaderText="GiftCardID" SortExpression="GiftCardID" InsertVisible="False" ReadOnly="True" />
                    <asp:BoundField DataField="RewardProviderName" HeaderText="RewardProviderName" SortExpression="RewardProviderName" ReadOnly="True" />
                    <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" ReadOnly="True" />
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" ReadOnly="True" />
                    <asp:BoundField DataField="DatePosted" HeaderText="DatePosted" SortExpression="DatePosted" ReadOnly="True" />
                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
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
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" SelectCommand="SELECT GiftCard.GiftCardID, RewardProvider.RewardProviderName, GiftCard.Value, GiftCard.Description, GiftCard.DatePosted, GiftCard.Status FROM GiftCard INNER JOIN RewardProvider ON GiftCard.RewardProviderID = RewardProvider.RewardProviderID" UpdateCommand="UPDATE [GiftCard] SET [Status] = @Status WHERE [GiftCardID] = @GiftCardID">
                <UpdateParameters>
                    <%--<asp:Parameter Name="GiftCardID" Type="String" />
                    <asp:Parameter Name="RewardProviderName" Type="String" />
                    <asp:Parameter Name="Value" Type="String" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="DatePosted" Type="String" />--%>
                    <asp:Parameter Name="Status" Type="String" />
                    <asp:Parameter Name="GiftCardID" Type="Int32" />
                    <%--<asp:Parameter Name="RewardProviderID" Type="Int32" />--%>
                </UpdateParameters>
            </asp:SqlDataSource>

        </p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <%--<h1>EDIT GIFT CARDS</h1>
        <p>
            &nbsp;</p>
        <p>
            <asp:CheckBoxList ID="activeList" runat="server" DataSourceID="SqlDataSource1" DataTextField="GiftCardID" DataValueField="RewardProviderName" CssClass="btn-forgot">
            </asp:CheckBoxList>
        </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Button ID="removeBtn" runat="server" Text="Remove Gift Cards" Width="40%" OnClick="removeBtn_Click" CssClass="button" />

        </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Button ID="pendingBtn" runat="server" Text="Move to Pending" Width="40%" OnClick="pendingBtn_Click" CssClass="button" />

        </p>--%>
    </div>
</asp:Content>

