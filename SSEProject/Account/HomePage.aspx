<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Site.Master" CodeBehind="HomePage.aspx.cs" Inherits="SSEProject.Account.HomePage" %>
       <asp:Content ID="ToDoListTable" ContentPlaceHolderID="MainContent" runat="server">
           <script type="text/javascript">  

               function DeleteConfirm()
               {
                   var Ans = confirm("Do you want to Delete Selected To-Do item?");
                   if (Ans)
                   {
                       return true;
                   }
                   else
                   {
                       return false;
                   }
               }

           </script>
           <asp:ScriptManager ID="scriptManager" runat="server"   />

<asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">                 
   <ContentTemplate>
   <asp:Timer runat="server" ID="Countdown_Timer" Interval="5000" ontick="Countdown_Timer_Tick"></asp:Timer>
         <asp:ImageButton  width="60" height="60" ID="ButtonAssign" OnClick="buttonAssign_Click" runat="server" ImageUrl="\Resources\Images\assign.ico" ToolTip="Assign Task" ImageAlign="AbsMiddle"/>
         <asp:ImageButton   width="70" height="70" ID="ButtonDelete" OnClick="buttonDelete_Click" runat="server" ImageUrl="\Resources\Images\delete_icon.png" ToolTip="Delete Task" ImageAlign="AbsMiddle"/>
    
           <asp:GridView ID="itemsGrid" runat="server" ShowFooter="True" AutoGenerateColumns="False" CellPadding="6" OnRowDataBound="itemsGrid_RowDataBound" OnRowCancelingEdit="itemsGrid_RowCancelingEdit" OnRowEditing="itemsGrid_RowEditing" OnRowUpdating="itemsGrid_RowUpdating" >  
                <Columns>  
                 <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox ID="selectRow" runat="server"/>
                    </ItemTemplate>
                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                     <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="ID">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>  
                    </ItemTemplate>  
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Description">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Description" runat="server" Text='<%#Eval("Description") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="Description" runat="server" Text='<%#Eval("Description") %>'></asp:TextBox>  
                    </EditItemTemplate>  
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Time">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Time" runat="server" Text='<%#Convert.ToDateTime(Eval("Time")).ToString("MM/dd/yyyy hh:mm:ss tt")%>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="Time" runat="server" Text='<%#Convert.ToDateTime(Eval("Time")).ToString("MM/dd/yyyy hh:mm:ss tt")%>'></asp:TextBox>  
                    </EditItemTemplate>  
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Status">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Status" runat="server" Text='<%#Eval("Status") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="Status" runat="server" Text='<%#Eval("Status") %>'></asp:TextBox>  
                    </EditItemTemplate>  
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="Timer">  
                    <ItemTemplate> 
                        <asp:Label ID="lbl_Timer" runat="server" Text=""></asp:Label>  
                    </ItemTemplate>  
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField>  
                    <ItemTemplate>  
                        <asp:ImageButton  width="70" height="35" ID="ButtonEdit" runat="server" CommandName="Edit"  ImageUrl="\Resources\Images\edit-button.png"  />
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:ImageButton  width="60" height="35" ID="ButtonUpdate" runat="server" CommandName="Update"  ImageUrl="\Resources\Images\update.png"  />  
                        <asp:ImageButton  width="60" height="35" ID="ButtonCancel" runat="server" CommandName="Cancel"  ImageUrl="\Resources\Images\cancel.png"  /> 
                    </EditItemTemplate> 
                    <FooterTemplate>  <%--SHOWS BLANK FILEDS AT THE BOTTOM OF THE GRIDVIEW.--%>
                         <tr>
                            <th>ID</th>
                            <th>Description</th>
                            <th>Time</th>
                            <th>Status</th>
                            <th></th>
                        </tr>
                        <tr>
                           <td><asp:TextBox ID="tbID"   runat="server" /></td>
                           <td><asp:TextBox ID="tbDescription"   runat="server" /></td>
                            <td><asp:TextBox ID="tbTime"  runat="server" />
                                <asp:CustomValidator runat="server" ControlToValidate="tbTime" ErrorMessage="Date was in incorrect format" OnServerValidate="CustomValidator1_ServerValidate" />
                            </td>
                            <td><asp:TextBox ID="tbStatus"   runat="server" /></td>
                            <td>
                                <asp:ImageButton  width="70" height="35" ID="ButtonAdd" runat="server" OnClick="buttonAdd_Click" 
                                    CommandName="Footer"  ImageUrl="\Resources\Images\add.png" />
                            </td>
                        </tr>
                        </div>   
                    </FooterTemplate>
                      </asp:TemplateField>
            </Columns>  
               <EmptyDataTemplate>  <%--SHOWS BLANK FILEDS WHEN THERE ARE NO TASKS TO SHOW.--%>
                        <tr>
                            <th>ID</th>
                            <th>Description</th>
                            <th>Time</th>
                            <th>Status</th>
                            <th></th>
                        </tr>
                        <tr>
                           <td><asp:TextBox ID="tbID"   runat="server" /></td>
                           <td><asp:TextBox ID="tbDescription"   runat="server" /></td>
                            <td><asp:TextBox ID="tbTime"  runat="server" /><asp:CustomValidator runat="server" ControlToValidate="tbTime" ErrorMessage="Date was in incorrect format" OnServerValidate="CustomValidator1_ServerValidate" />
                            </td>
                            <td><asp:TextBox ID="tbStatus"   runat="server" /></td>
                            <td>
                                <asp:ImageButton  width="70" height="35" ID="ButtonAdd" runat="server" OnClick="buttonAdd_Click" 
                                    CommandName="EmptyDataTemplate"  ImageUrl="\Resources\Images\add.png" />
                            </td>
                        </tr>
                   </EmptyDataTemplate>
            <HeaderStyle BackColor="#669999" ForeColor="#000000"/>  
        </asp:GridView>  
      
          
           <asp:SqlDataSource ID="TestDB" runat="server" ConnectionString="Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\noama\Documents\Projects\SSEProject\Resources\ToDoList.accdb;Persist Security Info=True;Jet OLEDB:Database Password=123456" ProviderName="System.Data.OleDb" SelectCommand="SELECT * FROM [Items]">
           </asp:SqlDataSource>
         <asp:Label ID="MessageBox" runat="server" BackColor="#FF9999" BorderColor="#CC0000" BorderStyle="Double" BorderWidth="1px" Font-Bold="True" Font-Italic="False" Font-Names="Times New Roman" Font-Size="Large" ForeColor="Black"></asp:Label>
         </ContentTemplate>  
       </asp:UpdatePanel>
     
</asp:Content>
        