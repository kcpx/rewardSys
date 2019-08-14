<%@ Page Title="" Language="C#" MasterPageFile="~/CEOMasterPage.master" AutoEventWireup="true" CodeFile="CEOEmployees.aspx.cs" Inherits="CEOEmployees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="btn-aligncenter">
        <h1>MANAGE EMPLOYEES</h1>
        <br />
        <div class="btn-aligncenter">
        <%--<h3>Current Employees..</h3>--%>
        </div>
        
        <asp:GridView ID="currentEmployeesGrid" runat="server" CssClass="table" AutoGenerateColumns="False" DataKeyNames="PersonID" DataSourceID="SqlDataSource1" AutoGenerateEditButton="True" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="PersonID" HeaderText="PersonID" InsertVisible="False" ReadOnly="True" SortExpression="PersonID" />
                <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                <asp:BoundField DataField="MI" HeaderText="MI" SortExpression="MI" />
                <asp:BoundField DataField="NickName" HeaderText="NickName" SortExpression="NickName" />
                <asp:BoundField DataField="column1" HeaderText="Email" SortExpression="column1" />
                <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" SelectCommand="SELECT PersonID, FirstName, LastName, MI, NickName, Position, [E-mail] AS column1, Status FROM Person WHERE (BusinessEntityID = @BusinessEntityID)" DeleteCommand="DELETE FROM [Person] WHERE [PersonID] = @PersonID" InsertCommand="INSERT INTO [Person] ([FirstName], [LastName], [MI], [NickName], [Position], [E-mail], [Status]) VALUES (@FirstName, @LastName, @MI, @NickName, @Position, @column1, @Status)" UpdateCommand="UPDATE [Person] SET [FirstName] = @FirstName, [LastName] = @LastName, [MI] = @MI, [NickName] = @NickName, [Position] = @Position, [E-mail] = @column1, [Status] = @Status WHERE [PersonID] = @PersonID">
            <DeleteParameters>
                <asp:Parameter Name="PersonID" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="FirstName" Type="String" />
                <asp:Parameter Name="LastName" Type="String" />
                <asp:Parameter Name="MI" Type="String" />
                <asp:Parameter Name="NickName" Type="String" />
                <asp:Parameter Name="Position" Type="String" />
                <asp:Parameter Name="column1" Type="String" />
                <asp:Parameter Name="Status" Type="String" />
            </InsertParameters>
            <SelectParameters>
                <asp:SessionParameter Name="BusinessEntityID" SessionField="BusinessEntityID" Type="Int32" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="FirstName" Type="String" />
                <asp:Parameter Name="LastName" Type="String" />
                <asp:Parameter Name="MI" Type="String" />
                <asp:Parameter Name="NickName" Type="String" />
                <asp:Parameter Name="Position" Type="String" />
                <asp:Parameter Name="column1" Type="String" />
                <asp:Parameter Name="PersonID" Type="Int32" />
                <asp:Parameter Name="Status" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />
<%--        <h1>REMOVE EMPLOYEES</h1>
        <p>
            <asp:CheckBoxList ID="removeEmployeesCB" runat="server" DataSourceID="SqlDataSource1" DataTextField="PersonID" DataValueField="PersonID">
            </asp:CheckBoxList>
        </p>
        <p>
            <asp:Button ID="removeBtn" runat="server" Text="Remove" CssClass="button" OnClick="removeBtn_Click"/>
        </p>--%>
        <p>
            <asp:Button ID="newEmployeeBtn" runat="server" PostBackUrl="~/CreateEmployee.aspx" Text="Create New Employee!" CssClass="button" />
            <br />
    </p>
    </div>
</asp:Content>

