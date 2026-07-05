<%@ Page Title="" Language="C#" MasterPageFile="~/LoginMP.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="diseasePrediction.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:Panel ID="Panel1" runat="server">       
<div class="container">          
<h2>Contact Details</h2>
<hr class="colorgraph">


<p>Name: Anil Raj</p>

<p>Address: #98, 5th Cross, New Kanthraj Urs Road, Bengaluru.</p>

<p>Mobile: 9985744141</p>

<p>Email Id: AnilRaj@gmail.com</p>

<p>Website Address: www.Disease.com</p>

<br />

 <table style="width: 75%;">
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="Image1" runat="server" Height="200px" 
                        ImageUrl="~/img/medical3.jpg" Width="350px" />
                </td>
                <td>
                    
                </td>
                <td>
                   
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>

</div>
</asp:Panel>

</asp:Content>
