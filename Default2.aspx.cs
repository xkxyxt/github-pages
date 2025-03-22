using Students.BLL;
using Students.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Default2 : System.Web.UI.Page
{
    [WebMethod]
    public static List<NEWS22> GetLatestNews()
    {
        Class2 bll = new Class2();
        return bll.GetLatestNews();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}