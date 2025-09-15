<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DetalleVentaUnica.aspx.cs" Inherits="Vistas.DetalleVentaUnica" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <<h2>Detalle de la Venta</h2>
<asp:Label ID="lblInfoVenta" runat="server"></asp:Label>
<br /><br />

<asp:GridView ID="gvDetalles" runat="server" AutoGenerateColumns="false" CssClass="table">
    <Columns>
        <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
        <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
        <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
    </Columns>
</asp:GridView>

<asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" CssClass="btn btn-secondary" />
</asp:Content>
