<%@ Page Title="" Language="C#" MasterPageFile="~/LoginMP.Master" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="diseasePrediction.AdminLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:Panel ID="Panel1" runat="server">
    
<div class="container">

<br />

<h2>ADMIN LOGIN FORM</h2>
<hr class="colorgraph">



		
			
			

			<div class="form-group">

            <asp:TextBox class="form-control input-lg" placeholder="Enter User Id" tabindex="4" ID="txtUserId" runat="server" Width=50%></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="Enter AdminId" ControlToValidate="txtUserId" 
                    CssClass="validation" ToolTip="Enter AdminId" ValidationGroup="a">Enter AdminId</asp:RequiredFieldValidator>
				
			</div>
			<div class="form-group">
				<asp:TextBox class="form-control input-lg" placeholder="Enter Password" 
                    tabindex="4" ID="txtPassword" runat="server" TextMode="Password" Width=50%></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="Enter Password" ControlToValidate="txtPassword" 
                    CssClass="validation" ToolTip="Enter Password" ValidationGroup="a">Enter Password</asp:RequiredFieldValidator>

			</div>

			
			
			
			<div class="row">
				<div class="col-xs-12 col-md-6">
                    <asp:Button ID="btnLogin" runat="server" Text="Sign In" 
                        class="btn btn-primary btn-block btn-lg" Width=50% 
                        onclick="btnLogin_Click" ValidationGroup="a" /></div>
				
			</div>
		
	

</div>
<br />




</asp:Panel>

</asp:Content>
