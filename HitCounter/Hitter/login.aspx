<%@ Page Title="" Language="C#" MasterPageFile="~/hitter.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Hitter.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <%-- <link href="Content/bootstrap.css" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
      <div class="row">
        <div class="col-md-5 col-md-offset-4">
          <div class="panel panel-default">
            <div class="panel-body">
             <%-- <form action="/login" method="post" style="margin:0" autocomplete="off">--%>
                <div class="form-group">
                  <input type="text" name="username" id="username" runat="server" placeholder="Enter Username" class="form-control" required minlength="3" maxlength="15" />
                </div>
                <button type="submit" class="btn btn-primary btn-block" id="sendchat" runat="server" onserverclick="sendchat_ServerClick">Enter Chat</button>
             <%-- </form>--%>
            </div>
          </div>
        </div>
      </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptarea" runat="server">
    <script src="Scripts/jquery-3.0.0.js"></script>
    <script src="Scripts/bootstrap.js"></script>
</asp:Content>
