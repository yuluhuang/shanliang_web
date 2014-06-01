using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using UN.BLL;
using System.Web.UI.WebControls;


public class AbstractHandler:System.Web.UI.Page
{
    protected CoreManager coreManager = CoreManager.getInstance(); 
    public string getandpost(HttpContext context, string aaa)
    {
        string a;
        try
        {
            if (context.Request.QueryString[aaa] != null)
            {
                a = context.Request.QueryString[aaa].ToString();
            }
            else if (context.Request.Form[aaa] != null)
            {
                a = context.Request.Form[aaa].ToString();
            }
            else
            {
                a = null;
            }
        }catch(Exception ex){
            a = null;
        }
        return a;
    }

    /// <summary>
    /// 剪裁图像
    /// </summary>
    /// <param name="Img"></param>
    /// <param name="Width"></param>
    /// <param name="Height"></param>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    /// <returns></returns>
    public void Crop(string filepath, int Width, int Height, int X, int Y, string aa)
    {

        try
        {
            // string type = filepath.Substring(filepath.LastIndexOf(".")).ToLower();  //获得后缀名.jpg
            string path = filepath.Substring(0, filepath.LastIndexOf("/") + 1);
            using (var OriginalImage = new Bitmap(filepath))
            {
                double bili_w = OriginalImage.Width / 300.0;
                double bili_h = OriginalImage.Height / 300.0;

                int X1 = (int)(X * bili_w);
                int Y1 = (int)(Y * bili_h);
                int Width1 = (int)(Width * bili_w);
                int Height1 = (int)(Height * bili_h);
                using (var bmp = new Bitmap(240, 240, OriginalImage.PixelFormat))
                {
                    bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);//分辨率
                    using (Graphics Graphic = Graphics.FromImage(bmp))
                    {
                        Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                        Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        /*大图*/
                        Graphic.DrawImage(OriginalImage, new Rectangle(0, 0, 240, 240), X1, Y1, Width1, Height1,
                                        GraphicsUnit.Pixel);
                        //var ms = new MemoryStream();
                        bmp.Save(path + "max_" + aa, OriginalImage.RawFormat);

                    }
                }
            }

            using (var OriginalImage = new Bitmap(filepath))
            {
                double bili_h = OriginalImage.Height / 300.0;
                double bili_w = OriginalImage.Width / 300.0;
                int X1 = (int)(X * bili_w);
                int Y1 = (int)(Y * bili_h);
                int Width1 = (int)(Width * bili_w);
                int Height1 = (int)(Height * bili_h);
                using (var bmp = new Bitmap(120, 120, OriginalImage.PixelFormat))/*位图大小*/
                {
                    bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);//分辨率
                    using (Graphics Graphic = Graphics.FromImage(bmp))
                    {
                        Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                        Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        /*中图*/
                        Graphic.DrawImage(OriginalImage, new Rectangle(0, 0, 120, 120), X1, Y1, Width1, Height1,
                                      GraphicsUnit.Pixel);/*填充位图的图片大小*/
                        bmp.Save(path + "mid_" + aa, OriginalImage.RawFormat);
                    }
                }
            }

            using (var OriginalImage = new Bitmap(filepath))
            {
                double bili_h = OriginalImage.Height / 300.0;
                double bili_w = OriginalImage.Width / 300.0;
                int X1 = (int)(X * bili_w);
                int Y1 = (int)(Y * bili_h);
                int Width1 = (int)(Width * bili_w);
                int Height1 = (int)(Height * bili_h);
                using (var bmp = new Bitmap(60, 60, OriginalImage.PixelFormat))
                {
                    bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);//分辨率
                    using (Graphics Graphic = Graphics.FromImage(bmp))
                    {
                        Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                        Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        /*小图*/
                        Graphic.DrawImage(OriginalImage, new Rectangle(0, 0, 60, 60), X1, Y1, Width1, Height1,
                                      GraphicsUnit.Pixel);
                        bmp.Save(path + "min_" + aa, OriginalImage.RawFormat);
                        //bmp.Save(path  + aa, OriginalImage.RawFormat);
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            throw (Ex);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="flag">0:登录失败/超时 </param>
    /// <param name="power">权限</param>
    /// <returns></returns>


    public string responseCode(string flag, string power)
    {
        return "[{\"flag\":" + flag + ",\"power\":" + power + "}]";

    }
}
