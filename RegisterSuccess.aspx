<%@ Page Title="" Language="C#" MasterPageFile="~/LoginMP.Master" AutoEventWireup="true" CodeBehind="RegisterSuccess.aspx.cs" Inherits="diseasePrediction.RegisterSuccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="Panel1" runat="server">
    
<div class="container">

<h2>Register (New Users)</h2>
<hr class="colorgraph">


			
		

		

          

            

              <div class="form-group">

            <asp:TextBox class="form-control input-lg" placeholder="Enter OTP" tabindex="4" ID="txtOTP" runat="server" Width=50%></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ErrorMessage="Enter OTP" ControlToValidate="txtOTP" 
                    CssClass="validation" ToolTip="Enter OTP" ValidationGroup="a">Enter OTP</asp:RequiredFieldValidator>							  				
			</div>
			
			
			<div class="row">
				<div class="col-xs-12 col-md-6">
                    <asp:Button ID="btnRegister" runat="server" Text="Sign Up" 
                        class="btn btn-primary btn-block btn-lg" Width=50% 
                        ValidationGroup="a" onclick="btnRegister_Click" /></div>
				
			</div>
		
	

</div>


</asp:Panel>

</asp:Content>
