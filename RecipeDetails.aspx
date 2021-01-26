<%@ Page Title="" Language="C#" MasterPageFile="~/Logged.Master" AutoEventWireup="true" CodeBehind="RecipeDetails.aspx.cs" Inherits="Licenta.RecipeDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .container{
            width: 100%;
        } 
      </style>
    <asp:Image ID="Image1" runat="server" Height="200px" Width="100%" />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Favorite" BackColor="Transparent" OnClick="Button1_Click" />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Ingredients" Font-Size="Large"></asp:Label>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Instructions" Font-Size="Large"></asp:Label>
    <br />
    <asp:Label ID="Label4" runat="server" Text="Label" Font-Size="Medium"></asp:Label>


</asp:Content>
