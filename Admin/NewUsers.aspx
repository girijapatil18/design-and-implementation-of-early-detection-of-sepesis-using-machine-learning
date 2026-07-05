<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMP.Master" AutoEventWireup="true" CodeBehind="NewUsers.aspx.cs" Inherits="diseasePrediction.Admin.NewUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="Panel1" runat="server">       
<div class="container"> 
<br />           
<h2>NEW USERS</h2>
<hr class="colorgraph">

 <asp:Table ID="tableUsers" runat="server">
    </asp:Table>

</div>

<br />  
<br />  
   
</asp:Panel>


</asp:Content>
