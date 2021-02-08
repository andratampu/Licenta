<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Licenta._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
        <h4>On this website you will be able to search for recipes and if you have no clue on what to search, we'll recommend you something.</h4>
        <h4>Please go to <i>Log In</i> if you are already registered or to <i>Sign Up</i> if you don't have an account.</h4>
    </div>
</asp:Content>
