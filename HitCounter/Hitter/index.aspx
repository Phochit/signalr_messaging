<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Hitter.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navigation Bar -->
     <nav class="navbar navbar-inverse">
      <div class="container-fluid">
        <div class="navbar-header">
          <a class="navbar-brand" href="#">pChat -  </a>
        </div>
        <ul class="nav navbar-nav navbar-right">
          <li><a href="#" id="logout" runat="server" onserverclick="logout_ServerClick">Log Out</a></li>
        </ul>
      </div>
    </nav>
    <!-- / Navigation Bar -->

    <div class="container">
      <div class="row">
        <div class="col-md-5 col-md-offset-4">
          <div class="panel panel-default">
            <div class="panel-body">
              <form action="/login" method="post" style="margin:0" autocomplete="off">
                <div class="form-group">
                  <input type="text" name="username" id="username" runat="server" placeholder="Enter Username" class="form-control" required minlength="3" maxlength="15" />
                </div>
                <button type="submit" class="btn btn-primary btn-block" id="sendchat" runat="server" onserverclick="sendchat_ServerClick" >Enter Chat</button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
    </form>
    <script src="Scripts/jquery-3.0.0.js"></script>
    <script src="Scripts/bootstrap.js"></script>
</body>
</html>
