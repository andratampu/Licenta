<%@ Page Title="" Language="C#" MasterPageFile="~/Logged.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Licenta.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .items
        { 
            position:absolute;
            left:0;
            right:0;
            text-align:center;
            padding-top:10%;
        }
    </style>
    <div class="items">
        <h1>Welcome!</h1>
        <br />
        <h4>By going to the <i>Search</i> page you can search what recipe you would like to cook or if you have no clue just press the <i>Recommend me something</i> button.</h4>
    </div>

</asp:Content>
