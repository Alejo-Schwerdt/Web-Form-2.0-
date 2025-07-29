<%@ Page Title="Agregar/Editar Producto" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AgregarEditarProducto.aspx.cs" Inherits="Vistas.AgregarEditarProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
     <h2><asp:Label ID="lblTitulo" runat="server" Text="Agregar Producto" /></h2>

    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />

    <asp:Panel runat="server" DefaultButton="btnGuardar">
        <table>
            <tr><td>Nombre:</td><td><asp:TextBox ID="txtNombre" runat="server" /></td></tr>
            <tr><td>Descripción:</td><td><asp:TextBox ID="txtDescripcion" runat="server" /></td></tr>
            <tr><td>Tipo:</td><td><asp:TextBox ID="txtTipo" runat="server" /></td></tr>
            <tr><td>Valor:</td><td><asp:TextBox ID="txtValor" runat="server" /></td></tr>
            <tr><td>Stock:</td><td><asp:TextBox ID="txtStock" runat="server" /></td></tr>
            <tr><td>Marca:</td><td><asp:TextBox ID="txtMarca" runat="server" /></td></tr>
        </table>

        <asp:Button ID="btnGuardar" runat="server" Text="💾 Guardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CssClass="btn btn-secondary" />
    </asp:Panel>
</asp:Content>
