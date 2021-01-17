<%@ Page Title="" Language="C#" MasterPageFile="~/Logged.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Licenta.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="newbody">
       <%-- <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>--%>
        <h3>Căutare</h3>
        <%--<form id="form2" runat="server">--%>
        <div class="items">
            <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" CellPadding="0" CellSpacing="0" ItemStyle-Width="30%" SeparatorStyle-Width="2%" RepeatLayout="Flow" Width="100%" ItemStyle-Height="30%">
                 <ItemTemplate>
                    <style>
                        .card {
                            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
                            transition: 0.3s;
                            width: 90%;
                        }

                        .card:hover {
                        
                            box-shadow: 0 10px 20px 0 rgba(0,0,0,0.7);
                        }

                        .container {
                            /* padding: 2px 16px;*/
                        }
                    </style>
                    <div class="card">
                         <img src="<%# Eval("Image") %>" alt="Avatar" style="width:100%">
                         <div class="container">
                            <p><b><%# Eval("Title") %></b></p>
                            <p>To be done</p>
                          </div>
                    </div>
                    <br />
            </ItemTemplate>
            </asp:DataList>
        </div>
    <%--</form>--%>
        </div>
</asp:Content>
