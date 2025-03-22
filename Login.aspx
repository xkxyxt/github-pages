<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<%-- <meta charset="UTF-8" />  
    <title>登录页面</title>  
    <style>  
        /* 背景遮罩 */
        #overlay {  
            display: none; /* 初始隐藏 */
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5);
            z-index: 999; /* 确保遮罩在最上层 */
        }  

        /* 弹窗 */
        #popup {  
            display: none; /* 初始隐藏 */
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            background-color: #fff;
            border-radius: 10px;
            padding: 40px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            z-index: 1000; /* 确保弹窗在遮罩之上 */
            width: 600px;
            max-width: 90%;
            height: 350px; /* 增加高度 */
            text-align: center;
            transition: all 0.3s ease;
        }

        #popup h3 {
            margin-top: 0;
            color: #333;
            font-size: 28px;
            margin-bottom: 20px;
        }

        .form-control {
            width: 100%;
            padding: 10px;
            margin: 10px 0;
            border: 1px solid #ddd;
            border-radius: 5px;
            font-size: 16px;
        }

        .btn {
            background-color: #007bff;
            color: #fff;
            border: none;
            padding: 10px 20px;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
            transition: background-color 0.3s ease;
        }

        .btn:hover {
            background-color: #0056b3;
        }

        .close-btn {
            background-color: #dc3545;
            color: #fff;
            border: none;
            padding: 10px 20px;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
            transition: background-color 0.3s ease;
            margin-left: 10px;
        }

        .close-btn:hover {
            background-color: #c82333;
        }
    </style>  --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--<!-- 背景遮罩 -->  
    <div id="overlay"></div>  

    <!-- 弹窗内容 -->  
    <div id="popup">  
        <h3>登录信息</h3>  
        <asp:TextBox ID="txtUsername" runat="server" placeholder="请输入用户名" CssClass="form-control" />  
        <asp:TextBox ID="txtPassword" runat="server" placeholder="请输入密码" TextMode="Password" CssClass="form-control" />  
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" /><br />  
        <asp:Button ID="btnLogin" runat="server" Text="登录" OnClick="btnLogin_Click" CssClass="btn" />  
        <button type="button" class="close-btn" onclick="hidePopup()">关闭</button>  
    </div>  

    <script type="text/javascript">  
       function showPopup(message) {
            document.getElementById('overlay').style.display = 'block';
            document.getElementById('popup').style.display = 'block';
            if (message) {
                document.getElementById('<%= lblMessage.ClientID %>').innerText = message;
            }
        }

        function hidePopup() {
            document.getElementById('overlay').style.display = 'none';
            document.getElementById('popup').style.display = 'none';
        }
        
    </script>  --%>
</asp:Content>

