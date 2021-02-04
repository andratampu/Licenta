<%@ Page Title="" Language="C#" MasterPageFile="~/Logged.Master" AutoEventWireup="true" CodeBehind="RecipeDetails.aspx.cs" Inherits="Licenta.RecipeDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .vertical
        {
            position:absolute;
            left:0;
            right: 5%;
            background-color:white;
            display:flex;
            flex-direction:row;
        } 
        .vertical2
        {
            display:flex;
        }
        .push-right
        {
            margin-left: auto;
        }
        .crop
        {
             max-width: 100%; 
             overflow: hidden;
             text-indent:-20%;
        }
        .texts
        {
            margin-left:5%;
        }
      </style>
    <br />
    <div class="vertical">
        <div class="crop">
            <asp:Image ID="Image1" runat="server" Height="100%" Width="100%" />
        </div>
        <div class="texts">
            <div class="vertical2">
                <asp:Label ID="Label1" runat="server" Text="Ingredients" Font-Size="Large"></asp:Label>
                <div class="vertical2 push-right">
                    <asp:Label ID="Label5" runat="server" Text="Rating:"></asp:Label>

                    <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Selected="True">1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                    </asp:DropDownList>

                </div>
            </div>
        
            <asp:Label ID="Label2" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
            <br />
            <asp:Label ID="Label3" runat="server" Text="Instructions" Font-Size="Large"></asp:Label>
            <br />
            <asp:Label ID="Label4" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
        </div>
   </div>


</asp:Content>
