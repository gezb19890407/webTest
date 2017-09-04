<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebPushDemo.aspx.cs" Inherits="testWebApplication.plugInUnit.webPush.WebPushDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="https://cdn.goeasy.io/goeasy.js"></script>
    <script type="text/javascript">
        var commonKey = 'BC-d1188086f6b944d1a76c8ce4088ad5f1';
        if (typeof GoEasy !== 'undefined') {
            var goEasy = new GoEasy({
                appkey: commonKey,
                userId: "222",
                username: "22",
                onConnected: function () {
                    writePanel("Connect to GoEasy success.");
                },
                onDisconnected: function () {
                    writePanel("Disconnect to GoEasy server.");
                },
                onConnectFailed: function (error) {
                    writePanel("Connect to GoEasy failed, error code: " + error.code + " Error message: " + error.content);
                }
            });
        }

        subscribe();
        function subscribe() {
            goEasy.subscribe({
                channel: 'notification',
                onMessage: function (message) {
                    writePanel('Meessage received:' + message.content);
                },
                onSuccess: function () {

                    writePanel("Subscribe the Channel successfully.");

                },

                onFailed: function (error) {

                    writePanel("Subscribe the Channel failed, error code: " + error.code + " error message: " + error.content);

                }

            });

        }

        function publishMessage() {
            goEasy.publish({
                channel: 'notification',
                message: 'You received a new notification',
                onSuccess: function () {

                    writePanel("Publish message success.");

                },
                onFailed: function (error) {

                    writePanel("Publish message failed, error code: " + error.code + " Error message: " + error.content);

                }
            });

        }

        function unsubscribe() {
            goEasy.unsubscribe({
                channel: "notification",
                onSuccess: function () {

                    writePanel("Cancel Subscription successfully.");

                },
                onFailed: function (error) {

                    writePanel("Cancel the subscrition failed, error code: " + error.code + "error message: " + error.content);
                }

            });
        }

        function writePanel(message, title) {
            var newDiv = document.createElement("div");
            var date = new Date().Format("yyyy-MM-dd HH:mm:ss");
            newDiv.innerHTML = date + "：" + (title != null ? "标题：" + title : "") + message + "<br />";
            document.getElementById("divPanel").appendChild(newDiv);
        };

        // 对Date的扩展，将 Date 转化为指定格式的String
        // 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
        // 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
        // 例子： 
        // (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
        // (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18 
        Date.prototype.Format = function (fmt) { //author: meizz 
            var o = {
                "M+": this.getMonth() + 1, //月份 
                "d+": this.getDate(), //日 
                "H+": this.getHours(), //小时 
                "m+": this.getMinutes(), //分 
                "s+": this.getSeconds(), //秒 
                "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
                "S": this.getMilliseconds() //毫秒 
            };
            if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
            for (var k in o)
                if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
            return fmt;
        }
    </script>
</head>
<body runat="server">
    <form runat="server">
        <input type="button" value="发送" onclick="publishMessage()" />
        <input type="button" value="退订" onclick="unsubscribe()" />
        <input type="button" value="订阅" onclick="subscribe()" />
        <asp:Button ID="Button1" runat="server" Text="后端发送" OnClick="Button1_Click" />
    </form>
    <div id="divPanel"></div>
</body>
</html>
