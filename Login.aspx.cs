using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Students.BLL;

public partial class Login : System.Web.UI.Page
{
    //Class2 class2 = new Class2();
    //protected void Page_Load(object sender, EventArgs e)
    //{

    //}
    //protected void btnLogin_Click(object sender, EventArgs e)

    //{

    //    // 调用 CheckLogin 方法验证用户  
    //    int userId = class2.CheckLogin(txtUsername.Text, txtPassword.Text);

    //    // 根据返回的 userId 判断登录是否成功  
    //    if (userId > 0)
    //    {
    //        // 登录成功，等下通过 JavaScript 弹窗提示并重定向  
    //        showPopup("登录成功！", "Default.aspx");
    //    }
    //    else
    //    {
    //        // 登录失败，显示失败弹窗  
    //        showPopup("登录失败，请检查用户名和密码。");
    //    }
    //}

    //private void showPopup(string message, string redirectUrl = null)
    //{
    //    string script = $"showPopup('{message}');";
    //    if (!string.IsNullOrEmpty(redirectUrl))
    //    {
    //        script += $"setTimeout(function() {{ window.location='{redirectUrl}'; }}, 1000);"; // 延时2秒重定向  
    //    }
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "showPopup", script, true);
    //}
    [WebMethod]
    public static int CheckUserLogin(string name, string password)
    {
        Class2 userClass = new Class2();
        return userClass.CheckLogin(name, password);
    }
    [WebMethod]
    public static int CheckAdminLogin(string name, string password)
    {
        // 调用Class2中的CheckAdminLogin方法
        Class2 adminBLL = new Class2();
        return adminBLL.CheckAdminLogin(name, password);
    }
}
