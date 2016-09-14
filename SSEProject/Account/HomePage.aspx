<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="HomePage.aspx.cs" Inherits="SSEProject.Account.HomePage" %>
       <asp:Content ID="ToDoListTable" ContentPlaceHolderID="MainContent" runat="server">
           <asp:ImageButton  width="40" height="40" ID="ButtonAssign" runat="server" CommandName="Assign"  ImageUrl="\Resources\Images\assign.ico" ToolTip="Assign Task" ImageAlign="AbsMiddle" Text="Assign"/>
    <asp:ImageButton   width="30" height="30" ID="ButtonDelete" runat="server" CommandName="Delete"   ImageUrl="\Resources\Images\delete_icon.png" ToolTip="Delete Task" ImageAlign="AbsMiddle" Text="Delete" />
    <asp:GridView runat="server" ID="itemsGrid" AutoGenerateColumns="False" Width="1430px" Height="350px" HorizontalAlign="Center" AllowSorting="True"
        onrowdeleting="itemsGrid_RowDeleting"
        onrowediting="itemsGrid_RowEditing"
         onrowcancelingedit="itemsGrid_RowCancelingEdit"
        OnRowDataBound="itemsGrid_RowDataBound"
        onrowupdating="itemsGrid_RowUpdating"
         CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" DataKeyNames="ID" DataSourceID="TestDB">
        <Columns>
             <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="selectRow" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" />
            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
            <asp:TemplateField>
                  <ItemTemplate>
                      <asp:ImageButton  width="20" height="20" ID="ButtonAccept" runat="server" CommandName="Accept"  ImageUrl="\Resources\Images\accept.png"  />
                      </ItemTemplate>
                 </asp:TemplateField>
                       <asp:TemplateField>
                 <ItemTemplate>
                     <asp:ImageButton  width="20" height="20" ID="ButtonReject" runat="server" CommandName="Reject"  ImageUrl="\Resources\Images\reject.png"  />
                      </ItemTemplate>
                            </asp:TemplateField>
            <asp:TemplateField>
                 <EditItemTemplate>
                     <asp:ImageButton  width="50" height="25" ID="ButtonUpdate" runat="server" CommandName="Update"  ImageUrl="\Resources\Images\update.png"  />
                     <asp:ImageButton  width="50" height="25" ID="ButtonCancel" runat="server" CommandName="Cancel"  ImageUrl="\Resources\Images\cancel.png"  />
                 </EditItemTemplate>
                 <ItemTemplate>
                   <asp:ImageButton  width="50" height="25" ID="ButtonEdit" runat="server" CommandName="Edit"  ImageUrl="\Resources\Images\edit-button.png"  />
                </ItemTemplate>
              <FooterTemplate>
                <asp:Button ID="ButtonAdd" runat="server" CommandName="AddNew"  Text="Add New Row" />
                </FooterTemplate>
                </asp:TemplateField>     
     
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
          
           <asp:SqlDataSource ID="TestDB" runat="server" ConnectionString="Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Samreen\SSEProject\Resources\ToDoList.accdb;Persist Security Info=True;Jet OLEDB:Database Password=123456" ProviderName="System.Data.OleDb" SelectCommand="SELECT * FROM [Items]"></asp:SqlDataSource>
          
</asp:Content>
        