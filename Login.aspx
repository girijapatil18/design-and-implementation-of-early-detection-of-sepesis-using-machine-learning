<%@ Page Title="" Language="C#" MasterPageFile="~/LoginMP.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="diseasePrediction.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
    


<div class="container">

<br />

				
               



<h2>USER LOGIN FORM</h2>
<hr class="colorgraph">


<div class="row">
    <div class="col-xs-12 col-sm-8 col-md-6 col-sm-offset-2 col-md-offset-3">
		<form role="form" class="register-form">
			<h2>Sign in <small>manage your account</small></h2>
			<hr class="colorgraph">

			<div class="form-group">

            <asp:TextBox class="form-control input-lg" placeholder="Enter User Id" tabindex="4" ID="txtUserId" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="Enter MemberId" ControlToValidate="txtUserId" 
                    CssClass="validation" ToolTip="Enter MemberId" ValidationGroup="a">Enter MemberId</asp:RequiredFieldValidator>
				
			</div>
			<div class="form-group">
				<asp:TextBox class="form-control input-lg" placeholder="Enter Password" 
                    tabindex="4" ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="Enter Password" ControlToValidate="txtPassword" 
                    CssClass="validation" ToolTip="Enter Password" ValidationGroup="a">Enter Password</asp:RequiredFieldValidator>

			</div>

			
			
			<hr class="colorgraph">
			<div class="row">
				<div class="col-xs-12 col-md-6">
                    <asp:Button ID="btnLogin" runat="server" Text="Sign In" 
                        class="btn btn-primary btn-block btn-lg" onclick="btnLogin_Click" ValidationGroup="a" /></div>
				<div class="col-xs-12 col-md-6">Don't have an account? <a href="Register.aspx">Register</a></div>
			</div>

          

		</form>
	</div>
</div>


</div>

<br />


</asp:Panel>


</asp:Content>
