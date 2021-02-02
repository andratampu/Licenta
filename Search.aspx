<%@ Page Title="" Language="C#" MasterPageFile="~/Logged.Master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Licenta.Search" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
   
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css" />
    <style type="text/css">
        .Background  
        {  
            background-color: Black;  
            filter: alpha(opacity=90);  
            opacity: 0.8;  
        } 
        .card {
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
            transition: 0.3s;
            width: 90%;
        }
        .vertical{
            position:absolute;
            left:10%;
            right:0;
            display: flex;
            align-items: center;
            justify-content: center;
            flex-direction: row;
        }
        .search{
            align-items:center;
            height: 100%;
            width: 30%;
        }
        .results{
            height: 100%;
            width: 70%;
        }
        .card:hover {    
            box-shadow: 0 10px 20px 0 rgba(0,0,0,0.7);
        }
    </style>
    <div class="vertical">
        <div class="search">
            <h4>Search recipes</h4>
            <div runat="server"> 
                <asp:TextBox ID="searchTxt" runat="server" Width="80%" Required="Required!"/>  
            </div> 
            <h4>Search ingredients</h4>

            <div>
                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Width="80%"></asp:TextBox>
            </div>

            <br/>
            <asp:Button ID="searchBtn" runat="server" Width ="90%" Text="Search" OnClick="searchBtn_Click" />
            <br/>
         </div>
        <div class="results">
            <div class="items">
                <br />
                <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" OnItemCommand="DataList1_ItemCommand" RepeatColumns="3" CellPadding="0" CellSpacing="0" ItemStyle-Width="30%" SeparatorStyle-Width="2%" RepeatLayout="Flow" Width="100%" ItemStyle-Height="30%">
                     <ItemTemplate>
                        <div class="card">
                             <img src="<%# Eval("Image") %>" alt="Avatar" style="width:100%">
                             <div class="container">
                                 <asp:Label ID="Label1" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                 <asp:Label ID="Label3" runat="server" Text='<%# Eval("ID") %>'  Visible="false"></asp:Label>
                                 <br />
                                 <asp:Button ID="Button1" runat="server" Text="View Details" CommandName="Item" />
                                 <br />
                              </div>
                        </div>
                    </ItemTemplate>
               </asp:DataList>
             </div>
        </div>
   </div>

</asp:Content>
