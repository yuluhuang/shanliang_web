using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UN.BLL;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

/// <summary>
///AbstractPage 的摘要说明
/// </summary>
public class AbstractPage : System.Web.UI.Page
{
    protected CoreManager bllService = CoreManager.getInstance();
    public bool checklogin(HttpContext hc)
    {
        //if (!bllService.getLoginService(hc.Session).islogon())
        //{
        //    hc.Response.Write("XXXXXX");
        //    hc.Response.End();
        //    return false;
        //}
        return true;
    }

    public object getNotLoginMessage()
    {
        return "XXXXXX";
    }
    protected void Page_Load(object sender, EventArgs e) { 
          
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="page">每页显示信息数</param>
    /// <param name="lnkUp">上一页</param>
    /// <param name="lnkDown">下一页</param>
    /// <param name="sy">首页</param>
    /// <param name="wy">尾页</param>
    /// <param name="lbl_info">文本</param>
    /// <param name="dt">数据源</param>
    /// <returns></returns>
    public PagedDataSource getPagedDataSource(int page,HyperLink lnkUp, HyperLink lnkDown, HyperLink sy, HyperLink wy, Label lbl_info,DataTable dt)
    {
        PagedDataSource pds = new PagedDataSource();
        pds.DataSource = dt.DefaultView;
        pds.AllowPaging = true;
        pds.PageSize = page;
        int currentPage = Convert.ToInt32(Request.QueryString["page"]);
        pds.CurrentPageIndex = currentPage;
        if (!pds.IsFirstPage)
        {
            lnkUp.NavigateUrl = Request.CurrentExecutionFilePath + "?page=" + (currentPage - 1);
        }
        if (!pds.IsLastPage)
        {
            lnkDown.NavigateUrl = Request.CurrentExecutionFilePath + "?page=" + (currentPage + 1);
        }
        sy.NavigateUrl = Request.CurrentExecutionFilePath + "?page=0";
        int aaa = Convert.ToInt32(pds.PageCount) - 1;
        wy.NavigateUrl = Request.CurrentExecutionFilePath + "?page=" + aaa;
        lbl_info.Text = "第" + (currentPage + 1) + "页,共" +pds.PageCount + "页";
        return pds;
    }
    public void bitmap(string path,string type, Bitmap OriginalImage, int Width, int Height, int X, int Y)
    {
        double bili_w = OriginalImage.Width / 500.0;
        double bili_h = OriginalImage.Height / 400.0;

        int X1 = (int)(X * bili_w);
        int Y1 = (int)(Y * bili_h);
        int Width1 = (int)(Width * bili_w);
        int Height1 = (int)(Height * bili_h);
        using (var bmp = new Bitmap(480, 240, OriginalImage.PixelFormat))
        {
            bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);//分辨率
            using (Graphics Graphic = Graphics.FromImage(bmp))
            {
                Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                /*大图*/
                Graphic.DrawImage(OriginalImage, new Rectangle(0, 0, 480, 240), X1, Y1, Width1, Height1,
                                GraphicsUnit.Pixel);
                //var ms = new MemoryStream();
                bmp.Save(path + "max_110252331" + type, OriginalImage.RawFormat);

            }
        }
    }
}