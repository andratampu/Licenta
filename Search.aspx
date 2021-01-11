<%@ Page Title="" Language="C#" MasterPageFile="~/Logged.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Licenta.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent1" runat="server">

    
    <div class="jumbotron">
       <%-- <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>--%>
        <h3>Căutare</h3>
        <form id="form2" runat="server">
        <div>
        </div>
        <asp:Repeater ID="Repeater2" runat="server">
            <HeaderTemplate>
                <table cellspacing="0" rules="all" border="1">
                    <tr>
                        <th scope="col" style="width: 80px">
                            ID
                        </th>
                        <th scope="col" style="width: 120px">
                            Title
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("ID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Title") %>' />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </form>
</asp:Content>
