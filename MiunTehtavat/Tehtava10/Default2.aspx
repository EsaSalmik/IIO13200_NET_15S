<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:XmlDataSource ID="srcLevyt"
            DataFile="~/App_Data/LevykauppaX.xml"
            runat="server"></asp:XmlDataSource>

        <asp:XmlDataSource ID="srcBiisit"
            DataFile="~/App_Data/LevykauppaX.xml"
            runat="server"></asp:XmlDataSource>

        <asp:Repeater ID="Repeater" DataSourceID="srcLevyt" runat="server">
            <HeaderTemplate>
                <table border="0">
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td>
                            <ul>
                                <img src="Images/<%# Eval("ISBN") %>.jpg"/></br>
                                <b><%# Eval("Artist") %> : <%# Eval("Title") %></b></br>
                                <b>ISBN: </b><%# Eval("ISBN") %></br>
                                <b>Levyn Biisit</b></br>
                                                      
                                        <asp:Repeater ID="Repeater" DataSourceID="srcBiisit" runat="server">
                                        <HeaderTemplate>
                                            <table border="0">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Eval("name") %></br>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                        </asp:Repeater>
                        </td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
