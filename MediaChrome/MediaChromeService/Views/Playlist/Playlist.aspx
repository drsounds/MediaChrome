<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Playlist
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><asp:Localize runat="server" ID="HeaderLocale"></asp:Localize></h2>
    <asp:Label Text="Text" runat="server" ID="Title" />
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
</asp:Content>
