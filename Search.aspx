<%@ Page Title="" Language="C#" MasterPageFile="~/Logged.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Licenta.Search" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .container{
            width: 100%;
        } 
        .Background  
        {  
            background-color: Black;  
            filter: alpha(opacity=90);  
            opacity: 0.8;  
        }  
        .Popup  
        {  
            background-color: #FFFFFF;  
            border-width: 3px;  
            border-style: solid;  
            border-color: black;  
            padding-top: 10px;  
            padding-left: 10px;  
            width: 400px;  
            height: 350px;  
        }  
        .lbl  
        {  
            font-size:16px;  
            font-style:italic;  
            font-weight:bold;  
        }  
        
        .card {
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
            transition: 0.3s;
            width: 90%;
        }

        .card:hover {
                        
            box-shadow: 0 10px 20px 0 rgba(0,0,0,0.7);
        }
    </style>
    <div class="newbody">
        <h3>Căutare</h3>
        <div id="form1" runat="server"> 
            <asp:TextBox ID="searchTxt" runat="server" Width="70%" />   
            <asp:Button ID="searchBtn" runat="server" Width ="20%" Text="Search" />
        </div>  
        <br />
        <div class="items">
            
            <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" CellPadding="0" CellSpacing="0" ItemStyle-Width="30%" SeparatorStyle-Width="2%" RepeatLayout="Flow" Width="100%" ItemStyle-Height="30%">
                 <ItemTemplate>
                    <div class="card">
                         <img src="<%# Eval("Image") %>" alt="Avatar" style="width:100%">
                         <div class="container">
                            <p><b><%# Eval("Title") %></b></p>
                             <p id="RecipeId"><%# Eval("ID")%></p>
                             <%--<asp:Label ID="Label1" runat="server" Visible="false"><%# Eval("ID")%></asp:Label>--%>
                             <asp:TextBox ID="TextBox1" runat="server"><%#Eval("ID")%></asp:TextBox>  <%--To be solved--%>
                            <asp:Button ID="Button1" runat="server" Text="View Details" OnClientClick="Button1_Click" />
                             <div id="form2" runat="server">  
                                <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="Button1"  
                                    CancelControlID="Button2" BackgroundCssClass="Background">  
                                </cc1:ModalPopupExtender>  
                                <asp:Panel ID="Panel1" runat="server" CssClass="Popup" align="center" style = "display:none">  
                                    <iframe style=" width: 350px; height: 300px;" id="irm1" src="DetailsPopup.aspx" runat="server"></iframe>  
                                   <br/>  
                                    <asp:Button ID="Button2" runat="server" Text="Close" />  
                                </asp:Panel>  
                            </div>
                          </div>
                    </div>
                    <br />
                </ItemTemplate>
           </asp:DataList>
        </div>
     </div>
</asp:Content>
