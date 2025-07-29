<%@ Page Title="Registrar Venta" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RegistrarVenta.aspx.cs" Inherits="WebApplication1.Views.Venta.RegistrarVenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <h2>Registrar Venta</h2>

    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />

    <table>
    <tr>
        <td>Producto:</td>
        <td style="display:flex; align-items:center; gap:10px;">
            <asp:DropDownList ID="ddlProductos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProductos_SelectedIndexChanged" />
            <asp:Label ID="lblStockDisponible" runat="server" ForeColor="Green" Text="" />
        </td>
    </tr>
    <tr>
        <td>Cantidad:</td>
        <td><asp:TextBox ID="txtCantidad" runat="server" /></td>
    </tr>
</table>

    <asp:Button ID="btnAgregar" runat="server" Text="➕ Agregar al carrito" OnClick="btnAgregar_Click" CssClass="btn btn-primary" />
    <br /><br />

    <asp:GridView ID="gvCarrito" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
            <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
        </Columns>
    </asp:GridView>

    <br />
    <asp:Button ID="btnRegistrarVenta" runat="server" Text="💾 Confirmar Venta" OnClick="btnRegistrarVenta_Click" CssClass="btn btn-success" />
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CssClass="btn btn-secondary" />
</asp:Content>
