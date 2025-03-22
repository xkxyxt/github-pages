<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>广州市中小学派位</title>
<style type="text/css">
    body, html {
        body,html {
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
        }
        .navbar {
            background-color: #4CAF50; /* 绿色 */
            color: white;
            padding: 15px 0;
            text-align: right;
            width: 100%;
            box-sizing: border-box;
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding-left: 20px;
            padding-right: 20px;
        }
        .nav-brand {
            display: flex;
            align-items: center;
        }
        .nav-brand img {
            height: 40px;
            margin-right: 10px;
        }
        .nav-brand span {
            font-size: 24px;
            font-weight: bold;
        }
        .navbar a {
            color: white;
            padding: 10px 20px;
            text-decoration: none;
            display: inline-block;
            font-size: 18px;
        }
        .navbar a:hover {
            background-color: #45a049; /* 深一点的绿色 */
        }
    .dashboard-container {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(400px, 1fr));
    gap: 2rem;
    padding: 2rem;
    max-width: 1400px;
    margin: 0 auto;
}

.dashboard-card {
    background: #fff;
    border-radius: 12px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    min-height: 400px;
    display: flex;
    flex-direction: column;
    transition: transform 0.3s ease;
}

.dashboard-card:hover {
    transform: translateY(-5px);
}

.card-header {
    padding: 1.5rem;
    border-bottom: 2px solid #f0f0f0;
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.card-header h3 {
    font-size: 1.4rem;
    color: #333;
    margin: 0;
}

.card-header .more-link {
    color: #007bff;
    text-decoration: none;
    font-size: 0.95rem;
}

.card-content {
    padding: 1.5rem;
    flex-grow: 1;
    overflow-y: auto;
}

/* 新闻动态增强样式 */
.news-list {
    list-style: none;
    padding: 0;
    margin: 0;
}

.news-list li {
    padding: 1rem 0;
    border-bottom: 1px solid #eee;
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.news-list li:last-child {
    border-bottom: none;
}

.news-date {
    font-size: 0.9rem;
    color: #666;
}

/* 公开公告增强样式 */
.notice-list {
    list-style: none;
    padding: 0;
    margin: 0;
}

.notice-list li {
    padding: 1rem 0;
    border-bottom: 1px solid #eee;
    display: flex;
    justify-content: space-between;
}

.notice-type {
    background: #e3f2fd;
    color: #1976d2;
    padding: 2px 8px;
    border-radius: 4px;
    font-size: 0.85rem;
}

/* 政务服务增强样式 */
.service-buttons {
    display: grid;
    grid-template-columns: repeat(2, 1fr);
    gap: 1rem;
    margin-bottom: 1.5rem;
}

.btn-service {
    padding: 1.2rem;
    background: #f8f9fa;
    border: 1px solid #dee2e6;
    border-radius: 8px;
    cursor: pointer;
    transition: all 0.3s ease;
    display: flex;
    flex-direction: column;
    align-items: center;
}

.btn-service:hover {
    background: #007bff;
    color: white;
    transform: scale(1.05);
}

.service-desc {
    font-size: 0.9rem;
    color: #666;
    margin-top: 1rem;
    line-height: 1.5;
}
        .hidden { display: none; }
        .fixed-width-input { width: 95%; }
        .cursor-move { cursor: move; }
        #loginModal {
            display: none;
            position: fixed;
            z-index: 1000;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgba(0,0,0,0.4);
        }
        .modal-content {
            background: white;
            margin: 15% auto;
            padding: 40px;
            border-radius: 10px;
            box-shadow: 0 10px 20px rgba(0,0,0,0.1), 0 4px 8px rgba(0,0,0,0.1);
            width: 30%;
            color: #333;
            text-align: center;
            transition: all 0.3s ease-in-out;
        }
        .close {
            color: #aaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }
        .close:hover,
        .close:focus {
            color: black;
            cursor: pointer;
        }
        .form-group {
            margin-bottom: 20px;
            text-align: left;
        }
        .form-group label {
            font-size: 18px;
            font-weight: 500;
            display: block;
            margin-bottom: 10px;
        }
        .input-container {
            display: flex;
            align-items: center;
        }
        .required-asterisk {
            color: red;
            font-size: 16px;
            margin-left: 5px;
        }
        input[type="text"], input[type="password"] {
            background: #fff;
            border: 1px solid #ddd;
            color: #333;
            padding: 12px 20px;
            margin: 0;
            outline: none;
            border-radius: 5px;
            font-size: 16px;
            transition: border-color 0.3s ease-in-out;
            width: calc(100% - 20px);
        }
        input[type="text"]:focus, input[type="password"]:focus {
            border-color: #007bff;
        }
        button {
            background: #007bff;
            color: white;
            border: none;
            padding: 12px 20px;
            margin: 20px 5px;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
            transition: all 0.3s ease-in-out;
        }
        button:hover {
            background: #0056b3;
        }
        @media (max-width: 600px) {
            .modal-content {
                width: 90%;
            }
        }
        .custom-select {
            height: 40px;
            padding: 10px;
            font-size: 16px;
        }
        #newsModalContent {
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.2);
            max-width: 600px;
            margin: auto;
            width: 90%;
            position: relative;
            top: -700px;
        }
        #newsModalContent button {
            background: none;
            border: none;
            font-size: 1.5em;
            cursor: pointer;
            color: #aaa;
            position: absolute;
            top: 10px;
            right: 10px;
            padding: 0;
        }
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }
        body, html {
            height: 100%;
            overflow-x: hidden;
        }
        .content {
            display: flex;
            justify-content: space-between;
            align-items: flex-start;
            width: 100%;
            padding: 10px;
        }
        .main-info, .news-feed {
            width: 48%;
            margin-bottom: 10px;
        }
        .news-feed h2 {
            margin-bottom: 10px;
            margin-top: 0;
        }
        #latestNews article {
            border-bottom: 1px solid #ccc;
            padding-bottom: 10px;
            margin-bottom: 10px;
            margin-top: 0;
        }
        #latestNews .news-item {
            margin-bottom: 20px;
            margin-top: 0;
        }
        #latestNews .news-title {
            margin: 0 0 5px;
        }
        #latestNews .news-date small {
            display: block;
            font-size: 0.875em;
            color: #666;
        }
        #latestNews .news-content {
            margin-top: 10px;
        }
        #latestNews hr {
            margin-top: 10px;
            border: none;
            border-top: 1px solid #ccc;
        }
        .eye-icon {  
    position: absolute;  
    right: 25px;  
    top: 50%;  
    transform: translateY(-50%);  
    cursor: pointer;  
    user-select: none; /* 防止选中文字 */  
}  
.input-container {  
    position: relative; /* 确保子元素绝对定位基于这个元素 */  
}  
</style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar">
            <div class="nav-brand">
                <img src="images/logo.png" alt="LOGO">
                <span>广州市中小学电脑系统</span>
            </div>
            <div class="nav-links">
                <a href="#home" onclick="event.preventDefault();">首页</a>
                <a href="#login" onclick="openLoginModal()">派位系统</a>
                <%--<a href="EnrollAndRegister.aspx" onclick="trackNavigation('EnrollAndRegister')">报名报考</a>
                <a href="#news" onclick="openNewsModal()">新闻公告</a>--%>
            </div>
        </div>

        <!-- 三大板块 -->
        <div class="dashboard-container">
            <!-- 新闻动态 -->
            <section class="dashboard-card">
                <div class="card-header">
                    <h3><i class="icon-news"></i> 新闻动态</h3>
                    <a href="#more-news" class="more-link">更多 ></a>
                </div>
                <div class="card-content">
                    <ul class="news-list">
                        <li>2024年新生入学政策解读</li>
                        <li>校园信息化建设成果展示</li>
                        <li>秋季学期开学通知</li>
                    </ul>
                </div>
            </section>

            <!-- 公开公告 -->
            <section class="dashboard-card">
                <div class="card-header">
                    <h3><i class="icon-notice"></i> 公开公告</h3>
                    <a href="#more-notices" class="more-link">更多 ></a>
                </div>
                <div class="card-content">
                    <ul class="notice-list">
                        <li>电脑派位系统操作指南</li>
                        <li>报名系统维护通知</li>
                        <li>家长操作手册下载</li>
                    </ul>
                </div>
            </section>

            <!-- 政务服务 -->
            <section class="dashboard-card">
                <div class="card-header">
                    <h3><i class="icon-service"></i> 政务服务</h3>
                    <a href="#more-services" class="more-link">更多 ></a>
                </div>
                <div class="card-content">
                    <div class="service-buttons">
                        <button class="btn-service">在线报名</button>
                        <button class="btn-service">结果查询</button>
                        <button class="btn-service">资料下载</button>
                    </div>
                </div>
            </section>
        </div>
    <!-- Login Modal -->
    <div id="loginModal" class="modal">
        <div class="modal-content">
            <span class="close" onclick="closeLoginModal()">&times;</span>
            <h2>登录</h2>
            <div id="message" class="mb-4"></div>
            <div class="form-group">
                <label for="userType" class="block mb-2">用户类型:</label>
                <select id="userType" class="border border-gray-400 p-2 w-full fixed-width-input custom-select">
                    <option value="user">普通用户</option>
                    <option value="admin">管理员</option>
                </select>
            </div>
            <div class="form-group">
                <label for="username" class="block mb-2">用户名:</label>
                <div class="input-container">
                    <input type="text" id="username" class="border border-gray-400 p-2 w-full fixed-width-input" />
                    <span id="username-asterisk" class="required-asterisk">*</span>
                </div>
            </div>
            <div class="form-group">  
    <label for="password" class="block mb-2">密码:</label>  
    <div class="input-container relative">  
        <input type="password" id="password" class="border border-gray-400 p-2 w-full fixed-width-input" />  
        <span id="password-asterisk" class="required-asterisk">*</span>  
        <span class="eye-icon" onclick="togglePasswordVisibility('password')">👁️</span>  
    </div>  
</div>  
<div class="form-group">  
    <label for="confirmPassword" class="block mb-2">确认密码:</label>  
    <div class="input-container relative">  
        <input type="password" id="confirmPassword" class="border border-gray-400 p-2 w-full fixed-width-input" />  
        <span id="confirmPassword-asterisk" class="required-asterisk">*</span>  
        <span class="eye-icon" onclick="togglePasswordVisibility('confirmPassword')">👁️</span>  
        <span id="confirmPassword-error" class="error-message"></span>  
    </div>  
</div>  
            <div class="flex justify-center">
                <button type="button" class="bg-green-600 text-white py-2 px-4 rounded" onclick="submitLogin()">登录</button>
                <button type="button" class="bg-gray-300 py-2 px-4 rounded ml-2" onclick="closeLoginModal()">取消</button>
            </div>
        </div>
    </div>

    <!-- News Modal -->
    <div id="newsModal" class="modal hidden fixed inset-0 z-50 flex items-center justify-center">
        <div id="newsModalContent" class="bg-white p-6 rounded-lg cursor-move max-h-full overflow-y-auto">
            <h2 class="text-lg font-semibold">新闻公告</h2>
            <button onclick="closeNewsModal()" class="absolute top-2 right-2">&times;</button>
            <div id="newsContent"></div>
        </div>
    </div>

    <!-- Error Modal -->
    <div id="errorModal" class="hidden fixed inset-0 z-50 flex items-center justify-center">
        <div class="bg-white p-6 rounded-lg cursor-move h-1/5 max-h-4xl" id="errorModalHeader">
            <h2 class="text-lg font-semibold">错误信息</h2>
            <div id="errorMessage" class="mt-4"></div>
            <button onclick="closeErrorModal()" class="mt-4 bg-blue-500 text-white px-4 py-2 rounded">关闭</button>
        </div>
    </div>

    <script src="scripts.js"></script>
        <div>
        </div>
    </form>
    <script>  
        function togglePasswordVisibility(inputId) {
            const inputField = document.getElementById(inputId);
            const type = inputField.getAttribute("type") === "password" ? "text" : "password";
            inputField.setAttribute("type", type);
        }  
        // Open Login Modal  
        function openLoginModal() {
            document.getElementById('loginModal').style.display = "block";
        }

        // Close Login Modal  
        function closeLoginModal() {
            document.getElementById('loginModal').style.display = "none";
            document.getElementById('message').textContent = ''; // Clear message  
            document.getElementById('username').value = '';
            document.getElementById('password').value = '';
            document.getElementById('confirmPassword').value = '';
        }

        // Login Submission  
        async function submitLogin() {
            var username = document.getElementById('username').value;
            var password = document.getElementById('password').value;
            var confirmPassword = document.getElementById('confirmPassword').value;
            var userType = document.getElementById('userType').value; // 获取用户类型
            var messageDiv = document.getElementById('message');
            var errors = [];

            // 清除之前的提示信息
            messageDiv.textContent = '';

            // 验证
            if (!username) {
                errors.push("用户名不能为空。");
                document.getElementById('username-asterisk').textContent = '*';
            }
            if (!password) {
                errors.push("密码不能为空。");
                document.getElementById('password-asterisk').textContent = '*';
            }
            if (password !== confirmPassword) {
                errors.push("两次密码不一致。");
                document.getElementById('confirmPassword-error').textContent = '两次密码不一致';
            }

            // 显示错误信息
            if (errors.length > 0) {
                showErrorModal(errors.join("<br>"));
                return;
            }

            // 模拟登录过程（替换为实际的异步登录请求）
            try {
                const url = userType === 'admin' ? "Login.aspx/CheckAdminLogin" : "Login.aspx/CheckUserLogin";
                const response = await fetch(url, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json; charset=utf-8"
                    },
                    body: JSON.stringify({ name: username, password: password })
                });

                const result = await response.json();

                if (response.ok && result.d > 0) {
                    messageDiv.textContent = '登录成功！';
                    messageDiv.className = 'mb-4 success-message';
                    setTimeout(() => {
                        window.location.href = userType === 'admin' ? "AdminPanel.aspx" : "Default.aspx"; // 根据用户类型重定向
                    }, 1000);
                } else {
                    messageDiv.textContent = '用户名或密码错误！';
                    messageDiv.className = 'mb-4 error-message';
                }
            } catch (error) {
                messageDiv.textContent = '登录请求失败，请稍后再试。';
                messageDiv.className = 'mb-4 error-message';
                console.error('Error during login:', error);
            }
        }

        // Show Error Modal  
        function showErrorModal(errorMessage) {
            const errorMessageElement = document.getElementById('errorMessage');
            errorMessageElement.innerHTML = errorMessage.replace(/\n/g, "<br/>"); // Display line breaks  
            document.getElementById('errorModal').classList.remove('hidden');
        }

        // Close Error Modal  
        function closeErrorModal() {
            document.getElementById('errorModal').classList.add('hidden');
        }

        // When the user clicks anywhere outside of the modal, close it  
        window.onclick = function (event) {
            var modal = document.getElementById('loginModal');
            if (event.target == modal) {
                closeLoginModal();
            }
        }


        // Open News Modal and load news
        function openNewsModal() {
            document.getElementById('newsModal').style.display = "flex"; // 使用 flex 确保居中
            loadNews();
        }

        function closeNewsModal() {
            document.getElementById('newsModal').style.display = "none";
        }

        // 当页面加载时调用此函数以填充新闻公告
        window.onload = function () {
            loadNews();
        };

        async function loadNews() {
            try {
                const response = await fetch("/Default2.aspx/GetLatestNews", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json; charset=utf-8"
                    },
                    body: JSON.stringify({})
                });
                const result = await response.json();

                if (response.ok) {
                    displayNews(result.d); // 注意这里的result.d，因为ASP.NET会包装返回的数据
                } else {
                    console.error('Failed to load news:', result);
                }
            } catch (error) {
                console.error('Error loading news:', error);
            }
        }

        function displayNews(newsArray) {
            var newsContentDiv = document.getElementById('latestNews');
            newsContentDiv.innerHTML = ''; // 清除之前的新闻内容

            newsArray.forEach(newsItem => {
                var article = document.createElement('article');
                article.innerHTML = `
            <div class="news-item">
                <h3 class="news-title">${newsItem.w_Title}</h3>
                <p class="news-date"><small>${new Date(newsItem.w_Date).toLocaleString()}</small></p>
            </div>
            <hr />
        `;
                newsContentDiv.appendChild(article);
            });

            // 如果没有新闻，则显示一条消息
            if (newsArray.length === 0) {
                newsContentDiv.innerHTML = '<p>暂无最新新闻公告。</p>';
            }
        }
    </script>  
</body>
</html>
