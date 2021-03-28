<%@ Page Title="" Language="C#" MasterPageFile="~/hitter.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="Hitter.Chat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/app.css" rel="stylesheet" />
    <style>
        
        .chat__body,.chat__body .chat__typing{
            display:block;
        }
        .chat__body .chat__typing{
            position:relative;
        }

        .chat__body
        {
            height:500px;
            overflow:scroll;
            margin-bottom:0px;
            overflow-x:hidden;
        }

        .chat__type__body .chat__type #sendMessage {
    position: relative;
    display: inline-block;
    top: -15px;
    width: 14%;
}
        .chat__type__body .chat__type textarea {
    background-color: inherit;
    width: 85%;
    vertical-align: text-bottom;
    border: 0.5px dashed #4a1b4a;
    display: inline-block;
}
        .chatdescopy{
            background-color: #f2f2f2;
            margin-top: -20px;
        }
        .users__bar{
            height:500px;
        }

        .row{
            display:block;
        }

        .chat__body .chat__main .__chat__.receive__chat p{
            margin-top: 20px !important;
            margin-bottom: -8.5px !important;
        }
        .chat__body .chat__main .__chat__.receive__chat{
            padding: 0px 15px;
            padding-bottom: 0px;
        }

        .chat__body .chat__main .__chat__ p.delivery-status
        {
            display:inline;
        }

        .chat__body .chat__main .__chat__.receive__chat p.delivery-status
        {
            display:inline !important;
            top: -20px !important;
        }

        #ContentPlaceHolder1_sendMessage{
            margin-top: -31px;
    padding: 5px 37px;
        }

        .users__bar .panel-body.users__body .user__item .status-bar.on{
            background-color:green;
        }
    </style>

      <script src="Scripts/modernizr-2.8.3.js"></script>
    <script src="Scripts/jquery-3.3.1.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    

    <script src="Scripts/jquery.signalR-2.4.1.min.js"></script>
    <script type="text/javascript" src="/signalr/hubs"></script>
    <script type="text/javascript">
      
        //$(function () {

        //    chrome.runtime.onMessage.addListener(function (message, sender, sendResponse) {

        //        // handles the message is received from the sender
        //        //  ...

        //        // send a response message to the sender
        //        sendResponse(responseMessage);

        //        // Here is the line of code that we want to add
        //        return true;
        //    })

        //});

        

        $(function () {
           
            var notiauth = $.connection.authHub;
            //debugger;
            // Create a function that the hub can call to broadcast messages.
            notiauth.client.updateMessages = function () {
                GetAuthCreate();
            };
            // Start the connection.
            $.connection.hub.start().done(function () {
                console.log("connection started");
                //notifications.onconn();
                GetAuthCreate();

            })
                .fail(function (e) {
                    alert(e);
                });
        });

        $(function () {

           

            var noti = $.connection.loginStatusHub;
            //debugger;
            // Create a function that the hub can call to broadcast messages.
            noti.client.updateMessages = function () {
                GetLoginStatus()
            };
            // Start the connection.
            $.connection.hub.start().done(function () {
                console.log("connection started")
                //notifications.onconn();
                GetLoginStatus()

            })
                .fail(function (e) {
                    alert(e);
                });
        });

        //$(function () {
           
           
        //    var notifications = $.connection.myHub2;
        //    //debugger;
        //    // Create a function that the hub can call to broadcast messages.
           
        //    notifications.client.updateMessages = function () {
              
        //            getAllMessages();
                   
        //    };
            
        //        // Start the connection.
        //        $.connection.hub.start().done(function () {
        //            console.log("connection started");
                   


                   
        //        })
        //            .fail(function (e) {
        //                alert(e);
        //     });
            
        //});


        

        function GetAuthCreate() {
           
            var cts = $('#contacts');
            $.ajax({
                url: '/Home/GetAuthCreate',
                contentType: 'application/html ; charset:utf-8',
                type: 'GET',
                dataType: 'html',
                success: function (result) {
                    var a4 = JSON.parse(result);
                    console.log(a4);
                    cts.empty();
                    var i = 0;
                    //href = "Chat.aspx?ruser='+value.id+'"
                    $.each(a4, function (key, value) {
                        
                        cts.append('<a class="user__item" id="u_' + value.id + '" onclick="cli(this.id)"   ><li id="l_'+value.id+'">'
                                    +'<div class="avatar">'
                                        +'<img src="/Content/no_avatar.png">'
                                    +'</div>'
                                        +'<span>'+value.name+'</span>'
                                    +'<div class="status-bar" id="sbar_' + value.id + '">'
                            +'</div ></li ></a >'

                        );
                        i++;
                    });


                }
            });
        }



        function GetLoginStatus() {
            var stBar = $('.status-bar');
            $.ajax({
                url: '/Home/GetLoginStatus',
                contentType: 'application/html ; charset:utf-8',
                type: 'GET',
                dataType: 'html',
                success: function (result) {
                    var a3 = JSON.parse(result);
                    console.log(a3);
                    var i = 0;

                    $.each(a3, function (key, value) {
                        var lgid = "sbar_" + value.loginid;
                        if ($('#' + lgid).length && value.loginstatus == 1) {
                            stBar = $('#' + lgid + '.status-bar');
                            /*stBar.empty();*/
                            if (!stBar.hasClass('on')) {
                                stBar.addClass('on');
                            }
                        }
                        else {
                            stBar = $('#' + lgid + '.status-bar');
                            /*stBar.empty();*/
                            if (stBar.hasClass('on')) {
                                stBar.removeClass('on');
                            }
                            
                        }
                        
                        i++;
                    });
                }
            });
        }

        function getAllMessages() {
            
            var tbl = $('.chat__main');
            var id = parseInt(localStorage.chatterid);
            ////var senderid = parseInt( '<%= Session["senderid"] %>');
            var senderid = parseInt('<%= Session["myid"] %>');
            $.ajax({
                url: '/Home/GetReceiveMessage/'+id,
                contentType: 'application/html ; charset:utf-8',
                type: 'GET',
                dataType: 'html',
                success: function (result) {

                    var a2 = JSON.parse(result);
                    console.log(a2);
                    tbl.empty();
                    var i = 0;
                    $.each(a2, function (key, value) {
                        if (value.sender_id != senderid) {
                            tbl.append('<div id="' + value.id + '" class="row __chat__par__">' +
                                '<div class="__chat__ receive__chat">' +
                                '<p>' + value.message + '</p>' +
                                '<p class="delivery-status">' +
                                new Date(parseInt((value.created_at).substr(6))).getDate() +
                                '-' +
                                (new Date(parseInt((value.created_at).substr(6))).getMonth() + 1) +
                                '-' +
                                new Date(parseInt((value.created_at).substr(6))).getFullYear() +
                                '&nbsp;<label class="delTime">' +
                                new Date(parseInt((value.created_at).substr(6))).getHours() +
                                ':' + (((new Date(parseInt((value.created_at).substr(6))).getMinutes()) < 10 ? '0' : '') +
                                    (new Date(parseInt((value.created_at).substr(6))).getMinutes()))
                                + '</label>'
                                + '</p></div ></div >'

                            );
                        }
                        else {
                            tbl.append('<div id="' + value.id + '" class="row __chat__par__">' +
                                '<div class="__chat__ from__chat">' +
                                '<p>' + value.message + '</p>' +
                                '<p class="delivery-status">' +
                                new Date(parseInt((value.created_at).substr(6))).getDate() +
                                '-' +
                                (new Date(parseInt((value.created_at).substr(6))).getMonth() + 1) +
                                '-' +
                                new Date(parseInt((value.created_at).substr(6))).getFullYear() +
                                '&nbsp;<label class="delTime">' +
                                new Date(parseInt((value.created_at).substr(6))).getHours() +
                                ':' + (((new Date(parseInt((value.created_at).substr(6))).getMinutes()) < 10 ? '0' : '') +
                                    (new Date(parseInt((value.created_at).substr(6))).getMinutes()))
                                + '</label>'
                                + '</p></div ></div >'

                            );
                        }
                        i++;
                    });

                    var el = document.getElementById("chat__body");
                    el.scrollTop = el.scrollHeight;
                        

                }

            }
            );
        }
    </script>

  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:HiddenField id="hidval" runat="server"></asp:HiddenField>--%>
    
    <div class="container">
      <div class="row">
        <div class="col-xs-12 col-md-3">
          <aside class="main">
            <div class="row">
              <div class="col-xs-12">
                <div class="panel panel-default users__bar">
                  <div class="panel-heading users__heading">
                    <%--Contacts (@ViewBag.allUsers.Count)--%>
                      Contacts <%= CustomerCount() %> 
                  </div>
                  <div class="__no__chat__">
                      <p>Select a contact to chat with</p>
                  </div>
                  <div class="panel-body users__body">
                    <ul id="contacts" class="list-group">

                   <%-- <%foreach (var user in GetCustomers())
                        {%>
                        <a class="user__item" href="Chat.aspx?ruser=<%= user.id %>" >
                            <li>
                              <div class="avatar">
                                 <img src="/Content/no_avatar.png">
                              </div>
                              <span><%= user.name %></span>
                              <div class="status-bar" id="sbar_<%=user.id %>"></div>
                            </li>
                        </a>
                    <% }%>--%>
                    </ul>
                  </div>
                </div>
              </div>
            </div>
          </aside>


        </div>
        <div class="col-xs-12 col-md-9 chat__body" id="chat__body">
          <div class="row">
            <div class="col-xs-12">
              <ul class="list-group chat__main">
                  <%-- <%foreach (var convo in GetConversationsList(Convert.ToInt32(Request.QueryString["ruser"]),Convert.ToInt32(Session["myid"].ToString() )))
                       { %>
                  <%if (convo.sender_id != Convert.ToInt32(Session["myid"]))
                                           { %>
                <div id="<%= convo.id %>" class="row __chat__par__">
                                <div class="__chat__ receive__chat">
                                    <p><%= convo.message %></p>
                                    <p class="delivery-status">Delivered</p>
                                </div>
                            </div>

                  <%} %>
                  <%else
                                           {%>
                   <div id="<%= convo.id %>" class="row __chat__par__">
                                <div class="__chat__ from__chat">
                                    <p><%= convo.message %></p>
                                    <p class="delivery-status">Delivered</p>
                                </div>
                            </div>
                            
                  <%} %>
                  <%} %>--%>
              </ul>
            </div>
            
          </div>
             
           
        </div> 
          <div class="col-xs-12 col-md-9 col-md-offset-3 chatdescopy">
                          <div class="chat__type__body">
              <div class="chat__type">
                  <input type="text" id="hv" runat="server" style="display:none;" />
                <textarea id="msg_box" placeholder="Type your message" runat="server"></textarea>
                <button class="btn btn-success" id="sendMessage" runat="server" onserverclick="sendMessage_ServerClick">Send</button>
              </div>
            </div>
            <div class="chat__typing">
              <span id="typerDisplay"></span>
            </div>
              </div>
      </div>
    </div>
    <%--<label id="hidval" style="display:none;" runat="server"></label>--%>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptarea" runat="server">
     
     <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>--%>
    <script type="text/javascript">
        //$(document).ready(function () {

        //    $('#l_2').click(function () {
        //        alert("fuck");
        //    });
        //});

        function cli(id) {
            localStorage.setItem("chatterid", (id).substr(2));
            //alert(localStorage.chatterid);
            $('#ContentPlaceHolder1_hv').text(localStorage.chatterid);
            getAllMessages();
        }

        //document.getElementById('l_2').onclick = function changeContent() {

        //    document.getElementById('l_2').textContent = "Help me";
        //    document.getElementById('l_2').style = "Color: red";

        //}
    </script>
</asp:Content>
