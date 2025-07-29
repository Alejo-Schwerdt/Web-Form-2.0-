<%@ Page Title="Gestión de Productos" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="WebApplication1.Views.Producto.Productos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <h2>Gestión de Productos</h2>

    <asp:Button ID="btnAgregarProducto" runat="server" Text="➕ Agregar Producto" OnClick="btnAgregarProducto_Click" CssClass="btn btn-success" />
    <asp:Button ID="btnVolver" runat="server" Text="🔙 Volver al Inicio" OnClick="btnVolver_Click" CssClass="btn btn-secondary" />

    <br /><br />

    <asp:GridView ID="GridViewProductos" runat="server" AutoGenerateColumns="false" OnRowCommand="GridViewProductos_RowCommand" CssClass="table table-striped" DataKeyNames="IdProducto">
        <Columns>
            <asp:BoundField DataField="IdProducto" HeaderText="ID" />
            <asp:BoundField DataField="NombreProducto" HeaderText="Nombre" />
            <asp:BoundField DataField="DescripcionProducto" HeaderText="Descripción" />
            <asp:BoundField DataField="TipoProducto" HeaderText="Tipo" />
            <asp:BoundField DataField="ValorProducto" HeaderText="Valor" DataFormatString="{0:C}" />
            <asp:BoundField DataField="StockProducto" HeaderText="Stock" />
            <asp:BoundField DataField="MarcaProducto" HeaderText="Marca" />
            <asp:ButtonField Text="📝 Editar" CommandName="Editar" ButtonType="Button" />
            <asp:ButtonField Text="🗑️ Eliminar" CommandName="Eliminar" ButtonType="Button" />
        </Columns>
    </asp:GridView>
</asp:Content>
