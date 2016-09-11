<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="HomePage.aspx.cs" Inherits="SSEProject.Account.HomePage" %>
       <asp:Content ID="ToDoListTable" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView runat="server" ID="itemsGrid" AutoGenerateColumns="False" Width="1041px" Height="350px" HorizontalAlign="Center" AllowSorting="True" OnSelectedIndexChanged="itemsGrid_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" DataKeyNames="ID" DataSourceID="SqlDataSource">
        <Columns>
             <asp:CheckBoxField AccessibleHeaderText="Select">
            </asp:CheckBoxField>
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" />
            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
     
             <asp:TemplateField HeaderText="Actions">
              <ItemTemplate>
                <asp:Image Width="20px" Height="20px" src="../Resources/Images/accept.png" runat="server" />
                <asp:Image Width="20px" Height="20px" src="../Resources/Images/reject.png" runat="server" />
                <asp:Image Width="20px" Height="20px"  src="../Resources/Images/forward_2.png" runat="server" />
              </ItemTemplate>
            </asp:TemplateField>        
            <asp:ButtonField  ButtonType="Image" ImageUrl="~/Resources/Images/delete_icon.png" Text="Button" >
            <ControlStyle Height="30px" Width="30px" />
            </asp:ButtonField>
            <asp:ButtonField ButtonType="Image" ImageUrl="~/Resources/Images/edit-button.png">
            <ControlStyle Height="45px" Width="65px" />
            </asp:ButtonField>
        </Columns>
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <rowstyle backcolor="#FFFBD6"  
           forecolor="#333333"/>

        <alternatingrowstyle backcolor="White"/>
        
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:GridView>
           <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [Items]"></asp:SqlDataSource>
</asp:Content>
        