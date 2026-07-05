<%@ Page Title="" Language="C#" MasterPageFile="~/Member/MemberMP.Master" AutoEventWireup="true" CodeBehind="SinglePatient.aspx.cs" Inherits="diseasePrediction.Member.SinglePatient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="Panel1" runat="server">
    
<div class="container">

<h2>Single Patient Prediction (NB Algorithm)</h2>
<hr class="colorgraph">
						
			

             <table style="width: 70%; height: 124px;">
                <tr>
                    <td class="style2">
                        <strong>Select Patient</strong></td>
                    <td class="style4">
                        <asp:DropDownList ID="DropDownListPatients" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                 <tr>
                     <td class="style2">
                         &nbsp;</td>
                     <td class="style4">
                         &nbsp;</td>
                     <td class="style5">
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td class="style2">
                         &nbsp;</td>
                     <td class="style4">
                         <asp:Button ID="btnSearch" runat="server" 
                             class="btn btn-primary btn-block btn-lg" Height="50px" 
                             onclick="btnSearch_Click" Text="Search" Width="200px" />
                     </td>
                     <td class="style5">
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
                <tr>
                    <td class="style2">
                        &nbsp;</td>
                    <td class="style4">
                        &nbsp;</td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
              
                <tr>
                    <td class="style2">
                        <strong>HR</strong></td>
                    <td class="style4">
                      <div class="form-group">
                        <asp:TextBox ID="txtHR" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong>O2Sat</strong></td>
                    <td class="style4">
                      <div class="form-group">
                        <asp:TextBox ID="txtO2Sat" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong>Temp</strong></td>
                    <td class="style4">
                      <div class="form-group">
                        <asp:TextBox ID="txtTemp" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong>MAP</strong></td>
                    <td class="style4">
                      <div class="form-group">
                        <asp:TextBox ID="txtMAP" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong>Resp</strong></td>
                    <td class="style4">
                      <div class="form-group">
                        <asp:TextBox ID="txtResp" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style1">
                        <strong>pH</strong></td>
                    <td class="style1">
                      <div class="form-group">
                        <asp:TextBox ID="txtpH" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style1">
                        &nbsp;</td>
                    <td class="style1">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong>AST</strong></td>
                    <td class="style4">
                      <div class="form-group">
                        <asp:TextBox ID="txtAST" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                       <strong> BUN</strong></td>
                    <td class="style4">
                      <div class="form-group">
                        <asp:TextBox ID="txtBUN" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong>Glucose</strong></td>
                    <td class="style4">
                      <div class="form-group">
                        <asp:TextBox ID="txtGlucose" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong>Lactate</strong></td>
                    <td class="style4">
                      <div class="form-group">
                        <asp:TextBox ID="txtLactate" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                 <tr>
                    <td class="style2">
                        <strong>Hgb</strong></td>
                    <td class="style4">
                      <div class="form-group">
                        <asp:TextBox ID="txtHgb" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong>PTT</strong></td>
                    <td class="style4">
                      <div class="form-group">
                        <asp:TextBox ID="txtPTT" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong> WBC</strong></td>
                    <td class="style4">
                      <div class="form-group">
                        <asp:TextBox ID="txtWBC" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                  <tr>
                    <td class="style2">
                        <strong> Fibrinogen</strong></td>
                    <td class="style4">
                      <div class="form-group">
                        <asp:TextBox ID="txtFibrinogen" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong> Platelets</strong></td>
                    <td class="style4">
                      <div class="form-group">
                        <asp:TextBox ID="txtPlatelets" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                 <tr>
                    <td class="style2">
                        <strong>Age</strong></td>
                    <td class="style4">
                      <div class="form-group">
                        <asp:TextBox ID="txtAge" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                 <tr>
                    <td class="style2">
                        <strong> Gender</strong></td>
                    <td class="style4">
                      <div class="form-group">
                        <asp:TextBox ID="txtGender" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                 <tr>
                    <td class="style2">
                        <strong> HospAdmTime</strong></td>
                    <td class="style4">
                      <div class="form-group">
                        <asp:TextBox ID="txtHospAdmTime" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                 <tr>
                    <td class="style2">
                        <strong>ICULOS</strong></td>
                    <td class="style4">
                      <div class="form-group">
                        <asp:TextBox ID="txtICULOS" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                 <tr>
                    <td class="style2">
                        <strong>time</strong></td>
                    <td class="style4">
                      <div class="form-group">
                        <asp:TextBox ID="txttime" runat="server" class="form-control input-lg" 
                              Width="200px"></asp:TextBox>
                        </div>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
              
             
                <tr>
                    <td class="style2">
                        &nbsp;</td>
                    <td class="style4">
                        &nbsp;</td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                 <tr>
                     <td class="style2">
                         &nbsp;</td>
                     <td class="style4">
                         <div class="row">
                             <div class="col-xs-12 col-md-6">
                                 <asp:Button ID="btnSubmit" runat="server" 
                                     class="btn btn-primary btn-block btn-lg" Height="50px" 
                                     onclick="btnSubmit_Click" Text="Disease Prediction" ValidationGroup="user" 
                                     Width="200px" />
                             </div>
                         </div>
                     </td>
                     <td class="style5">
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
            </table>
    <br />
           
   
           
                        <h2 class="title"><span>
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label></span></h2>
         
		
	

</div>



    </span>



</asp:Panel>
</asp:Content>
