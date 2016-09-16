<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Assign.aspx.cs" Inherits="SSEProject.Account.Assign" %>
<asp:Content ID="AssignList" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">  

               function AssignConfirm()
               {
                   var Ans = confirm("Do you want to Assign the Selected To-Do items?");
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
    <asp:GridView ID="selectedItemsGrid" runat="server" AutoGenerateColumns="False" OnRowDataBound="itemsGrid_RowDataBound" CellPadding="6" DataKeyNames="ID" Width="1300px">  
              <Columns>  
                 <asp:TemplateField HeaderText="ID">  
                    <ItemTemplate>  
                        <asp:Label ID="ID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>  
                    </ItemTemplate>
                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Description">  
                    <ItemTemplate>  
                        <asp:Label ID="Description" runat="server" Text='<%#Eval("Description") %>'></asp:Label>  
                    </ItemTemplate>
                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />  
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Time">  
                    <ItemTemplate>  
                        <asp:Label ID="Time" runat="server" Text='<%#Convert.ToDateTime(Eval("Time")).ToString("MM/dd/yyyy hh:mm:ss tt")%>'></asp:Label>  
                    </ItemTemplate> 
                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Status">  
                    <ItemTemplate>  
                        <asp:Label ID="Status" runat="server" Text='<%#Eval("Status") %>'></asp:Label>  
                    </ItemTemplate>
                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />    
                </asp:TemplateField>
                  </Columns>
        <HeaderStyle BackColor="#6495ED" ForeColor="#000000"/> 
       <EditRowStyle BackColor="#A8E4FF" ForeColor="#000000" />
        </asp:GridView>
    
        <p contenteditable="false" style="font-family: 'Times New Roman', Times, serif; font-size: 24px; font-weight: bold; font-style: normal; width: 213px;">Assign To </p>
      
     <asp:DropDownList ID="usersDropDownList" runat="server" DataTextField="Username" DataValueField="Username" Height="50px" Width="200px">
        </asp:DropDownList>
        
        <asp:ImageButton  width="172px" height="70px" ID="ButtonAssignGo" runat="server"   ImageUrl="\Resources\Images\GoButton.png"  ImageAlign="AbsMiddle"/>
      <asp:Label ID="MessageBox" runat="server" BackColor="#FF9999" BorderColor="#CC0000" BorderStyle="Double" BorderWidth="1px" Font-Bold="True" Font-Italic="False" Font-Names="Times New Roman" Font-Size="Large" ForeColor="Black"></asp:Label>
    </asp:Content>