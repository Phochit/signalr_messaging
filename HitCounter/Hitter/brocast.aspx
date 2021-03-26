<%@ Page Title="" Language="C#" MasterPageFile="~/hitter.Master" AutoEventWireup="true" CodeBehind="brocast.aspx.cs" Inherits="Hitter.brocast" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/bootstrap.css" rel="stylesheet" />

         <script src="Scripts/jquery-3.3.1.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <!--<script src="../Scripts/jquery-3.0.0.js"></script>-->
    <%--<script src="Scripts/jquery.signalR-2.1.2.js"></script>--%>
    <%--<script src="Scripts/jquery.signalR-2.4.1.js"></script>--%>
    <script src="Scripts/jquery.signalR-2.4.1.min.js"></script>
    <script type="text/javascript" src="/signalr/hubs"></script>
    <script type="text/javascript">

        $(function () {
            var notifications = $.connection.chatListMessage;
            //debugger;
            // Create a function that the hub can call to broadcast messages.
            notifications.client.updateMessages = function () {
                getAllMessages()
            };
            // Start the connection.
            $.connection.hub.start().done(function () {
                console.log("connection started")
                //notifications.onconn();
                getAllMessages();
            })
                .fail(function (e) {
                alert(e);
            });
            //document.getElementById("ContentPlaceHolder1_sendMessage").addEventListener("click", function (event) {
            //    //var user = document.getElementById("userInput").value;
            //    //var message = document.getElementById("messageInput").value;
            //    //connection.invoke("SendMessage", user, message).catch(function (err) {
            //    //    return console.error(err.toString());
            //    //});
            //    getAllMessages();
            //    //event.preventDefault();
            //});
        });

        function getAllMessages() {
            var tbl = $('#messagesTable');
            ////var senderid = parseInt( '<%= Session["senderid"] %>');
            $.ajax({
                url: 'Push/GetMessages',
                contentType: 'application/html ; charset:utf-8',
                type: 'GET',
                dataType:'html',
                success: function (result) {
                    
                    var a2 = JSON.parse(result);
                    console.log(a2);
                    tbl.empty();
                    var i = 1;
                    $.each(a2, function (key, value) {
                        tbl.append('<tr>' + '<td>' + i + '</td>' + '<td>' + value.sender_id + '</td>' + '<td>' + value.receiver_id + '</td>'
                            + '<td>' + value.message + '</td>' + '<td>' + value.status + '</td>'  + '<td>' + value.created_at + '</td>' + '</tr>');
                        i = i + 1;
                    });
                }

            }
            );
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading">Employee Information</div>
            <div class="panel-body">

                <div>
                    <table id="tab"></table>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <table class="table table-striped">
                                <thead>
                                    <tr>
                                        <th>Id</th>
                                        <th>Name</th>
                                        <th>Salary</th>
                                        <th>Department</th>
                                        <th>Designation</th>
                                    </tr>
                                </thead>
                                <tbody id="messagesTable"></tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptarea" runat="server">

</asp:Content>
