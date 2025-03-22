
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuControl.ascx.cs" Inherits="MenuControl" %>


<nav style="width: 240px; background-color: white; padding: 10px; box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); position: fixed; height: 900px;top: 119px;">  
    <h2 class="text-lg font-semibold"style="margin-bottom: 40px;"> </h2>  
    <ul style="list-style: none; padding: 0;">  
        <li style="margin-bottom: 100px;">  
          <a href="javascript:void(0);" onclick="openModal()" class="block py-2 text-gray-700 hover:bg-gray-200" style="font-size: 25px;">派位管理</a>
        </li>  

        <li style="margin-bottom: 100px;">  
            <a href="javascript:void(0);" onclick="toggleSchoolList()" class="block py-2 text-gray-700 hover:bg-gray-200" style="font-size: 25px;">学校信息</a>  
        </li>  

    </ul>  

</nav>  
<aside id="schoolListContainer" style="width: 240px; background-color: #f9f9f9; padding: 10px; box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); margin-left: 10px; position: fixed; top: 200px; left: 260px; display: none; z-index: 10;">  
    <h3>学校列表</h3>  
    <ul id="schoolList" style="list-style: none; padding: 0;">  
        <%= GetSchoolNamesList() %>  
    </ul>  
</aside> 
<div id="myModal" class="modal">
    <div class="modal-content">
        <span class="close" onclick="closeModal()">&times; 关闭</span>
        <h2 class="modal-title">选择派位方式</h2>
        <ul class="sort-options">
            <li>
                <label>
                    <input type="radio" name="hfTwinBinding" value="twinBinding"> 双胎/多胎捆绑上学
                </label>
            </li>
            <li>
                <label>
                    <input type="radio" name="hfTwinBinding" value="noTwinBinding"> 双胎/多胎不捆绑上学
                </label>
            </li>
        </ul>
       
        <button id="btnConfirmSort" onclick="confirmSort()">确定</button>
        <input type="hidden" id="hfTwinBinding" name="hfTwinBinding" value="" runat="server"  />
    </div>
</div>

<style>  
   
    /* 模态框样式 */  
   .modal {  
    display: none; /* 默认隐藏 */  
    position: fixed; /* 固定定位 */  
    z-index: 1; /* 在最上层 */  
    left: 0;  
    top: 0;  
    width: 100%; /* 全屏 */  
    height: 100%; /* 全屏 */  
    overflow: hidden; /* 隐藏滚动条 */  
    background-color: rgba(0, 0, 0, 0.4); /* 背景透明度 */  
}  

.modal-content {  
    background-color: #fefefe; /* 内容背景色 */  
    margin: 10% auto; /* 上下距离10%，左右自动居中 */  
    padding: 20px; /* 内边距 */  
    border: 1px solid #888; /* 边框 */  
    width: 40%; /* 宽度 */  
    max-height: 80%; /* 最大高度，防止内容超出视口 */  
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2); /* 阴影效果 */  
    border-radius: 8px; /* 圆角 */  
    overflow-y: auto; /* 内容超出时显示滚动条 */  
}  

.modal-title {  
    font-size: 16px; /* 调整字体大小 */  
    margin: 0; /* 设置为0，靠近顶端 */  
    padding-bottom: 10px; /* 底部留出间距 */  
    border-bottom: 1px solid #ccc; /* 增加底部边框以分隔内容 */  
}  

.close {  
    color: #aaa; /* 关闭按钮颜色 */  
    float: right; /* 右对齐 */  
    font-size: 20px; /* 字体大小 */  
    font-weight: bold; /* 加粗 */  
}  

.close:hover,  
.close:focus {  
    color: black; /* 悬停和聚焦时颜色 */  
    text-decoration: none; /* 去掉下划线 */  
    cursor: pointer; /* 鼠标指针 */  
}  

.sort-options {  
    list-style-type: none; /* 去掉列表样式 */  
    padding: 0; /* 去掉内边距 */  
    margin: 0; /* 去掉外边距 */  
}  

.sort-options li {  
    margin-bottom: 5px; /* 调整选项之间的间隔 */  
}  

#btnConfirmSort {  
    background-color: #4CAF50; /* 按钮背景色 */  
    color: white; /* 按钮文字颜色 */  
    padding: 10px 20px; /* 按钮内边距 */  
    border: none; /* 去掉边框 */  
    border-radius: 5px; /* 圆角 */  
    cursor: pointer; /* 鼠标指针 */  
    font-size: 16px; /* 字体大小 */  
    transition: background-color 0.3s; /* 背景颜色过渡效果 */  
}  

#btnConfirmSort:hover {  
    background-color: #45a049; /* 悬停时背景颜色 */  
}  


    #schoolListContainer {
        max-height: 70vh; /* 设置最大高度为视口高度的80% */
        overflow-y: auto; /* 当内容超出时显示垂直滚动条 */
        border-radius: 15px; /* 添加圆角 */
        border: 1px solid #ccc; /* 添加边框 */
        transition: box-shadow 0.3s; /* 箭影效果平滑过渡 */
    }

    #schoolListContainer:hover {
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2); /* 悬停时的阴影效果 */
    }

    #schoolListContainer h3 {
        margin-top: 0; /* 上边距为0 */
        margin-bottom: 20px; /* 下边距增加一些空间 */
        font-size: 20px; /* 增加字体大小 */
    }

    #schoolListContainer ul {
        list-style: none; /* 去掉列表样式 */
        padding: 0; /* 去掉内边距 */
    }

    #schoolListContainer ul li {
        margin-bottom: 20px; /* 增加列表项之间的间隔 */
        font-size: 18px; /* 增大字体大小 */
    }

    @media (max-width: 768px) {
        #schoolListContainer {
            width: 100%; /* 在小屏幕下全宽 */
            left: 50px; /* 左对齐 */
            top: 50px; /* 上边距 */
        }
    }
</style>  


<script type="text/javascript">  
    function openModal() {
        document.getElementById('myModal').style.display = 'block';
    }

    function closeModal() {
        document.getElementById('myModal').style.display = 'none';
    }

    // 点击窗口外部关闭模态窗口  
    window.onclick = function (event) {
        const modal = document.getElementById('myModal');
        if (event.target === modal) {
            modal.style.display = "none";
        }
    }
    function confirmSort() {
        var selectedOption = document.querySelector('input[name="hfTwinBinding"]:checked');
        if (selectedOption) {
            document.getElementById('hfTwinBinding').value = selectedOption.value;

            console.log("Client Selected Twin Binding: " + document.getElementById('hfTwinBinding').value);

            var formClientId = '<%= ((MasterPage)this.Page.Master).FindControl("form1").ClientID %>';
             var form = document.getElementById(formClientId);
             console.log("Form element: ", form);
             console.log("Hidden field value before submit: " + document.getElementById('hfTwinBinding').value); // 新增的调试信息
             form.submit();
         } else {
             alert('请选择双胎是否捆绑上学的派位方式');
         }
     }
    function toggleSchoolList() {
        var schoolListContainer = document.getElementById('schoolListContainer');
        if (schoolListContainer.style.display === 'none') {
            schoolListContainer.style.display = 'block';
        } else {
            schoolListContainer.style.display = 'none';
        }
    }

    <%--function selectSchool(element) {
        var schoolName = element.innerText; // 获取被点击的学校名称
        // 更新页面上显示学校名称的元素
        document.getElementById('selectedSchoolName').innerText = "所选学校: " + schoolName;
        // 使用Ajax调用服务器端方法更新 dataContent 的内容
        __doPostBack('<%= this.UniqueID %>', schoolName);
}--%>
    function selectSchool(element) {
        var schoolName = element.innerText; // 获取被点击的学校名称
        // 更新页面上显示学校名称的元素
        document.getElementById('selectedSchoolName').innerText = schoolName;

        // 使用 AJAX 调用服务器端方法
        var xhr = new XMLHttpRequest();
        xhr.open("POST", "UpdateSchoolName.aspx/UpdateSelectedSchoolName", true);
        xhr.setRequestHeader("Content-Type", "application/json; charset=utf-8");
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
                var response = JSON.parse(xhr.responseText);
                if (response.d) {
                    console.log("学校名称已更新: " + response.d);
                } else {
                    console.log("更新学校名称失败");
                }
            }
        };
        xhr.send(JSON.stringify({ schoolName: schoolName }));
    }

</script>  

