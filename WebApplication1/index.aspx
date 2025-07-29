<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Vistas.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Inicio
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="titulo">
    <h2  style="text-align:center">Bienvenido al sistema TiendaHardware</h2>
    </div>
    <p>Seleccione una opción:</p>

    <div style="display:flex; flex-direction:column; gap:15px; max-width:300px;">
        <asp:Button ID="btnProductos" runat="server" Text="➕ Gestionar Productos" OnClick="btnProductos_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnRegistrarVenta" runat="server" Text="🛒 Registrar Venta" OnClick="btnRegistrarVenta_Click" CssClass="btn btn-success" />
        <asp:Button ID="btnDetalleVentas" runat="server" Text="📊 Ver Ventas" OnClick="btnDetalleVentas_Click" CssClass="btn btn-info" />
    </div>
</asp:Content>
