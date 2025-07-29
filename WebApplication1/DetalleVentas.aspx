<%@ Page Title="Detalle de Ventas" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DetalleVentas.aspx.cs" Inherits="Vistas.DetalleVentas.DetalleVentas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <h2>Historial de Ventas</h2>

    <asp:Repeater ID="rptVentas" runat="server">
        <ItemTemplate>
            <div style="border:1px solid #ccc; padding:10px; margin-bottom:15px;">
                <strong>Venta #<%# Eval("IdVenta") %></strong><br />
                Fecha: <%# Eval("FechaVenta", "{0:dd/MM/yyyy HH:mm}") %><br />
                Total: <%# Eval("TotalVenta", "{0:C}") %><br />

                <asp:Repeater ID="rptDetalles" runat="server" DataSource='<%# Eval("Detalles") %>'>
                    <HeaderTemplate>
                        <table class="table table-striped">
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
