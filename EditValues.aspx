<%@ Page Title="" Language="C#" MasterPageFile="~/CEOMasterPage.master" AutoEventWireup="true" CodeFile="EditValues.aspx.cs" Inherits="EditValues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="btn-aligncenter">
        <h1>MANAGE COMPANY VALUES</h1>
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ValueID" DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None" AllowSorting="True">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="ValueName" HeaderText="ValueName" SortExpression="ValueName" />
                <asp:BoundField DataField="ValueDescription" HeaderText="ValueDescription" SortExpression="ValueDescription" />
                <asp:BoundField DataField="ValueID" HeaderText="ValueID" InsertVisible="False" ReadOnly="True" SortExpression="ValueID" />
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" DeleteCommand="DELETE FROM [Value] WHERE [ValueID] = @ValueID" InsertCommand="INSERT INTO [Value] ([ValueName], [ValueDescription], [LastUpdated], [LastUpdatedBy], [BusinessEntityID]) VALUES (@ValueName, @ValueDescription, @LastUpdated, @LastUpdatedBy, @BusinessEntityID)" SelectCommand="SELECT * FROM [Value] WHERE ([BusinessEntityID] = @BusinessEntityID) ORDER BY [ValueID]" UpdateCommand="UPDATE [Value] SET [ValueName] = @ValueName, [ValueDescription] = @ValueDescription, [LastUpdated] = @LastUpdated, [LastUpdatedBy] = @LastUpdatedBy, [BusinessEntityID] = @BusinessEntityID WHERE [ValueID] = @ValueID">
            <DeleteParameters>
                <asp:Parameter Name="ValueID" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="ValueName" Type="String" />
                <asp:Parameter Name="ValueDescription" Type="String" />
                <asp:Parameter DbType="Date" Name="LastUpdated" />
                <asp:Parameter Name="LastUpdatedBy" Type="String" />
                <asp:Parameter Name="BusinessEntityID" Type="Int32" />
            </InsertParameters>
            <SelectParameters>
                <asp:SessionParameter Name="BusinessEntityID" SessionField="BusinessEntityID" Type="Int32" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="ValueName" Type="String" />
                <asp:Parameter Name="ValueDescription" Type="String" />
                <asp:Parameter DbType="Date" Name="LastUpdated" />
                <asp:Parameter Name="LastUpdatedBy" Type="String" />
                <asp:Parameter Name="BusinessEntityID" Type="Int32" />
                <asp:Parameter Name="ValueID" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:Button ID="btnAddValue" runat="server" PostBackUrl="~/AddValue.aspx" Text="Add New Company Value" CssClass="button" />
    </div>

</asp:Content>

