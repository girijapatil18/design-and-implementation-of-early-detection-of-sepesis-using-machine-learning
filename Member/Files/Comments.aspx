<%@ Page Title="" Language="C#" MasterPageFile="~/Member/MemberMP.Master" AutoEventWireup="true" CodeBehind="Comments.aspx.cs" Inherits="diseasePrediction.Member.Comments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="Panel1" runat="server">

 <div class="container">

        <div class="section-title">
           <h2><span>VIEW TOPIC</span> COMMENTS !</h2>
        </div>
        <hr class="colorgraph">
        <div class="row">

   

    
     

      <br />
       

        <table align="center" style="width: 96%;">
            <tr style="font-size: small">
                <td>
                    <asp:Table ID="Table1" runat="server">
                    </asp:Table>
                </td>
            </tr>
        </table>
         <br />
        <table align="center" style="width: 95%;">
            <tr style="font-size: small">
                <td style="width: 398px">
                    <b>Add New Comment</b></td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr style="font-size: small">
                <td style="width: 398px">
                    <asp:TextBox ID="txt_comment" runat="server" Height="80px" TextMode="MultiLine" 
                        Width="400px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txt_comment" ErrorMessage="Enter Comment" ToolTip="field required" 
                        ValidationGroup="comment" CssClass="validation"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="font-size: small">
                <td align="right" style="width: 398px">
                    <asp:Button ID="btn_comment" runat="server" Text="Submit" 
                        ValidationGroup="comment" onclick="btn_comment_Click" />
                </td>
                <td>
                    <br />
                </td>
            </tr>
        </table>
       </div>

       </div>

       <br />

       <br />
    </asp:Panel>
</asp:Content>
