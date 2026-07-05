<%@ Page Title="" Language="C#" MasterPageFile="~/Member/MemberMP.Master" AutoEventWireup="true" CodeBehind="_RFAlgorithm.aspx.cs" Inherits="diseasePrediction.Member._RFAlgorithm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="Panel1" runat="server">
    
<div class="container">
<br />

<h2>SEPSIS PREDICTION (RF)</h2>
<hr class="colorgraph">


			
             <div style="height:500px; width:auto; overflow:auto">
                 <h2>Testing Dataset</h2>
                 <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                     BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                     <FooterStyle BackColor="White" ForeColor="#000066" />
                     <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                     <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                     <RowStyle ForeColor="#000066" />
                     <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                     <SortedAscendingCellStyle BackColor="#F1F1F1" />
                     <SortedAscendingHeaderStyle BackColor="#007DBB" />
                     <SortedDescendingCellStyle BackColor="#CAC9C9" />
                     <SortedDescendingHeaderStyle BackColor="#00547E" />
                 </asp:GridView>
</div>
      
            <br />

             <h2><span>SEPSIS </span>PREDICTION USING RANDOM FOREST ALGORITHM</h2>
          <hr />

          <br />
          <div class="col-xs-12 col-md-6">
          <asp:Button ID="btnPrediction" runat="server" 
                      Text="Click Here to Predict" 
              onclick="btnPrediction_Click" class="btn btn-primary btn-block btn-lg"  /></div> &nbsp;&nbsp;&nbsp;

              <div class="col-xs-12 col-md-6">
          <asp:Button ID="btnResults" runat="server" class="btn btn-primary btn-block btn-lg" 
              onclick="btnResults_Click" Text="Results Analysis" /></div>
          <br /><br /><div>
           <h2> 1 YES ,0 NO</h2>
      <asp:Table ID="tablePrediction" runat="server">
      </asp:Table>

            <br />
		
	

</div>
</div>

<br />
<br />

</asp:Panel>
</asp:Content>

