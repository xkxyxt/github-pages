using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Students.BLL;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

public partial class MenuControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    public string GetSchoolNamesList()
    {
        Class2 dbClass = new Class2(); // 创建Class2实例
        var schools = dbClass.GetAllSchoolNames(); // 调用新添加的方法获取学校名称列表
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        foreach (var school in schools)
        {
            //sb.AppendFormat("<li>{0}</li>", HttpUtility.HtmlEncode(school));
            sb.AppendFormat("<li onclick='selectSchool(this);'>{0}</li>", HttpUtility.HtmlEncode(school));

        }

        return sb.ToString();
    }
}
