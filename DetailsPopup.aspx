<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetailsPopup.aspx.cs" Inherits="Licenta.DetailsPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <form id="form1" runat="server">
        <h3>Ingredients</h3>
        <asp:DataList ID="DataList1" runat="server">
            <ItemTemplate>
                <p>
                    <%--<p><b><%# Eval("Title") %></b></p>--%>
                    To be done
                </p>
            </ItemTemplate>
        </asp:DataList>
        <h3>Preparation</h3>
        <asp:DataList ID="DataList2" runat="server">
            <ItemTemplate>
                <p>
                    To be done
                </p>
            </ItemTemplate>
        </asp:DataList>
    </form>
    
</body>
</html>
