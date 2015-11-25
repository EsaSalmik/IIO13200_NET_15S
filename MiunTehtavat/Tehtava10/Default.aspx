<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tehtävä 10</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:XmlDataSource ID="srcLevyt"
            DataFile="~/App_Data/LevykauppaX.xml"
            Xpath="Records/genre[@name='Pop']/record"
            runat="server"></asp:XmlDataSource>



        <h1 style="font-family: 'MV Boli'">LEVYKAUPPA X</h1>



        <asp:Repeater ID="Repeater" DataSourceID="srcLevyt" runat="server">
            <HeaderTemplate>
                <table border="0">
            </HeaderTemplate>
            <ItemTemplate>
                             <ul>
                                <img src="Images/<%# Eval("ISBN") %>.jpg"/>
                                <li><b><%# Eval("Artist") %> : <%# Eval("Title") %></b></li>
                                <li><b>ISBN:</b> <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# Eval("ISBN","~/Default2.aspx?isbn={0}") %>' runat="server"> <%# Eval("ISBN") %> </asp:HyperLink></li>
                                <li><b>Hinta:</b> <%# Eval("Price") %></li>
                            </ul>
                        </td>
               
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
