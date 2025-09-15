<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="DetalleVentaUnica.aspx.cs" Inherits="Vistas.DetalleVentas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <h2>Historial de Ventas</h2>

    <!-- Ordenar -->
    <div style="margin-bottom:10px;">
        Ordenar por:
        <asp:DropDownList ID="ddlOrdenar" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlOrdenar_SelectedIndexChanged">
            <asp:ListItem Value="FechaDesc">Fecha (Desc)</asp:ListItem>
            <asp:ListItem Value="FechaAsc">Fecha (Asc)</asp:ListItem>
            <asp:ListItem Value="TotalDesc">Total (Desc)</asp:ListItem>
            <asp:ListItem Value="TotalAsc">Total (Asc)</asp:ListItem>
        </asp:DropDownList>
    </div>

    <!-- Repeater padre -->
    <asp:Repeater ID="rptVentas" runat="server" OnItemDataBound="rptVentas_ItemDataBound" OnItemCommand="rptVentas_ItemCommand">
        <ItemTemplate>
            <div style="border:1px solid #ccc; padding:10px; margin-bottom:15px;">
                <strong>Venta #<%# Eval("IdVenta") %></strong><br />
                Fecha: <%# Eval("FechaVenta", "{0:dd/MM/yyyy HH:mm}") %><br />
                Total: <%# Eval("TotalVenta", "{0:C}") %><br />

                <asp:Button ID="btnVerDetalle" runat="server"
                    Text="Ver Detalle"
                    CommandName="VerDetalle"
                    CommandArgument='<%# Eval("IdVenta") %>'
                    CssClass="btn btn-info btn-sm" />

                <!-- Repeater hijo — se liga en ItemDataBound -->
                <asp:Repeater ID="rptDetalles" runat="server">
                    <HeaderTemplate>
                        <table class="table table-striped mt-2">
                            <tr>
                                <th>Producto</th>
                                <th>Cantidad</th>
                                <th>Precio Unitario</th>
                                <th>Subtotal</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Producto.NombreProducto") %></td>
                            <td><%# Eval("Cantidad") %></td>
                            <td><%# Eval("PrecioUnitario", "{0:C}") %></td>
                            <td><%# Eval("Subtotal", "{0:C}") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <asp:Button ID="btnVolver" runat="server" Text="🔙 Volver al inicio" OnClick="btnVolver_Click" CssClass="btn btn-secondary" />
</asp:Content>