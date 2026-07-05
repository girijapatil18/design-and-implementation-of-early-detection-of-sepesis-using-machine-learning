<%@ Page Title="" Language="C#" MasterPageFile="~/Member/MemberMP.Master" AutoEventWireup="true" CodeBehind="Topics.aspx.cs" Inherits="diseasePrediction.Member.Topics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="Panel1" runat="server">

 <div class="container">

        <div class="section-title">
           <h2><span>DISCUSSION</span> FORUM !</h2>
        </div>
        <hr class="colorgraph">
        <div class="row">

   

     
     
      <br />

        <table align="center" style="width: 95%;">
            <tr style="font-size: small">
                <td align="left">
                    <asp:Table ID="Table1" runat="server">
                    </asp:Table>
                </td>
            </tr>
        </table>
      
        <table align="left" style="width: 45%;">
            <tr style="font-size: small">
                <td class="style1">
                    <b>Add New Topic</b></td>
                <td style="width: 398px">
                    &nbsp;</td>
            </tr>
            <tr style="font-size: small">
                <td class="style1">
                    <asp:TextBox ID="txt_topic" runat="server" Height="80px" TextMode="MultiLine" 
                        Width="400px"></asp:TextBox>
                </td>
                <td style="width: 398px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txt_topic" CssClass="validation" 
                        ErrorMessage="Enter Topic" ToolTip="Enter Topic" ValidationGroup="topic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="font-size: small">
                <td align="right" class="style1">
                    <asp:Button ID="btn_topic" runat="server" onclick="btn_topic_Click" 
                        Text="Submit" ValidationGroup="topic" />
                </td>
                <td align="right" style="width: 398px">
                    &nbsp;</td>
            </tr>
        </table>

        </div>
        </div>

        <br />
        <br />
    </asp:Panel>
</asp:Content>
