<%@ WebHandler Language="C#" Class="upload" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using UN.DAL;
using UN.BLL;
using UN.Model;
//using ooo.connector;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;




public class upload : AbstractHandler, IHttpHandler, IRequiresSessionState
{
    private string PATH = HttpContext.Current.Server.MapPath("~/uploads/").ToLower();
    private static string TOOLPATH = HttpContext.Current.Server.MapPath("~/tools/").ToLower();
    public void ProcessRequest(HttpContext context)
    {
        if (context.Session.SessionID != null && Convert.ToInt32(context.Session["power"]) >= 2)
        {
            CoreManager caremanager = new CoreManager();
            tiaominfo tm = new tiaominfo();
            string id_zuop = getandpost(context, "id");
            int paixu = 0;//排序专用
            // DB_DLL db = new DB_DLL();
            try
            {
                string type = "";
                string newname = "";
                string newpath = "";
                string a = "";
                string sfile;


                string OLDPATH = PATH + @"s/";//上传文件夹路径
                if (context.Request.Files.Count > 0)
                {

                    string tempFile = context.Request.PhysicalApplicationPath + @"uploads/s/";
                    for (int j = 0; j < context.Request.Files.Count; j++)
                    {
                        HttpPostedFile uploadFile = context.Request.Files[j];
                        if (uploadFile.ContentLength > 0)
                        {
                            uploadFile.SaveAs(tempFile + System.IO.Path.GetFileName(uploadFile.FileName));
                            sfile = uploadFile.FileName;//file文件名包括后缀
                            type = sfile.Substring(sfile.LastIndexOf(".")).ToLower();  //获得后缀名.doc

                            newname = Guid.NewGuid().ToString("N");//唯一文件名不包括后缀
                            newpath = PATH + @"z/" + newname;//路径不包括后缀
                            string downpath = @"uploads/z/" + newname;
                            File.Move(OLDPATH + sfile, (OLDPATH + newname + type).ToLower());

                            int g;

                            g = coreManager.getZuopinBLL().selectMaxSort(id_zuop);

                            //别删
                            //if (type == ".doc" || type == ".xls" || type == ".ppt" || type == ".pdf")
                            //{
                            //    a = "document";
                            //    tm.zuopId = Int32.Parse(id_zuop);
                            //    tm.oldname = sfile;
                            //    tm.newname = newname + type;
                            //    tm.leixing = a;
                            //    tm.lujing1 = downpath + type;
                            //    tm.lujing2 = downpath + ".swf";
                            //    int aa = coreManager.getTiaoMuBLL().insertItem(tm);
                            //    buttonclick(newname, type);
                            //}

                            //else//不需要转换
                            //{
                            //if (type == ".mov" || type == ".mp4" || type == ".flv" || type == ".mp3")
                            //{
                            //    a = "vedio";
                            //}
                            //else if (type == ".swf")
                            //{
                            //    a = "swf";
                            //}
                            //else if (type == ".pdf")
                            //{
                            //    a = "pdf";
                            //}
                            //else if (type == ".jpg" || type == ".png" || type == ".bmp" || type == ".gif" || type == "jpeg")
                            //{
                            //    a = "image";
                            //}
                            //else
                            //{
                            //    a = "other";
                            //}

                            //tm.zuopId = Int32.Parse(id_zuop);
                            //tm.oldname = sfile;
                            //tm.newname = newname + type;
                            //tm.leixing = a;
                            //tm.lujing1 = downpath + type;
                            //tm.lujing2 = downpath + type;

                            //int aa = coreManager.getTiaoMuBLL().insertItem(tm);

                            //File.Move(OLDPATH + newname + type, newpath + type);
                            // }



                            if (type == ".mov" || type == ".mp4" || type == ".flv" || type == ".mp3")
                            {
                                a = "vedio";
                            }
                            else if (type == ".doc" || type == ".xls" || type == ".ppt")
                            {
                                a = "office";
                                buttonclick(newname, type);
                                type = ".swf";
                            }
                            else if (type == ".swf")
                            {
                                a = "swf";
                            }
                            else if (type == ".pdf")
                            {
                                a = "pdf";
                            }
                            else if (type == ".jpg" || type == ".png" || type == ".bmp" || type == ".gif" || type == "jpeg")
                            {
                                a = "image";
                            }
                            else
                            {
                                a = "other";
                            }

                            tm.zuopId = Int32.Parse(id_zuop);
                            tm.oldname = sfile;
                            tm.newname = newname + type;
                            tm.leixing = a;
                            tm.lujing1 = downpath + type;
                            tm.lujing2 = downpath + type;
                            tm.paixu = g.ToString();

                            int aa = coreManager.getTiaoMuBLL().insertItem(tm);

                            File.Move(OLDPATH + newname + type, newpath + type);
                        }
                    }
                }
                HttpContext.Current.Response.Write(" ");
            }
            catch
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Hello World");
            }
        }
    }
    // public static string sfile;
    public void buttonclick(string newname, string type)
    {
        string sfile;
        string oldpath = PATH + @"s\";//上传文件夹路径
        string newpath = "";
        sfile = newname + type;//sfile文件名包括后缀
        newpath = PATH + @"z\" + sfile;//路径包括后缀
        try
        {
            if (type == ".doc" || type == ".xls" || type == ".ppt")
            {
                toPDF(sfile);

            }
            else if (type == ".pdf")
            {
                pdf2swf_2(oldpath + sfile);
            }
        }
        catch { }
    }

    public void toPDF(string sfile)
    {
        //此处说明用法：1.得到转换对象
        upload ctt = upload.getInstance();
        //2.把需要转换的文件扔给转换对象，转换线程会在另一个线程里去转换文件。
        ctt.putFile(PATH + @"s\" + sfile);

    }
    public void putFile(String inputFile)//
    {
        lock (obj)
        {
            fileQueue.Enqueue(inputFile);
        }
    }

    public static upload getInstance()//
    {
        if (ctt != null)
        {
            return ctt;
        }
        else
        {
            lock (obj)
            {
                ctt = new upload();
                oooExecFolder = TOOLPATH + @"\OpenOfficePortable\App\openoffice\program";////必须指向根目录下面的program目录
                new Thread(new ThreadStart(ctt.convert)).Start();
            }
            return ctt;
        }
    }
    private static upload ctt = null;
    private static Queue<String> fileQueue = new Queue<string>(1000);
    private static Object obj = new Object();
    private static String oooExecFolder = null;
    private void convert()
    {
        while (true)
        {
            Thread.Sleep(1000);
            String inputfile = null;
            lock (obj)
            {
                if (fileQueue.Count > 0)
                {
                    inputfile = fileQueue.Dequeue();
                }
            }

            if (inputfile != null)
            {
                DateTime now = DateTime.Now;
                string name = inputfile.Substring(inputfile.LastIndexOf("\\") + 1).ToLower();  //获得路径+文件名包括后缀
                string apart_houzui = name.Substring(0, name.LastIndexOf(".")).ToLower();//获得路径+文件名不包括后缀
                try
                {
                    ConvertToPDF(inputfile, PATH + @"s\" + apart_houzui + ".pdf", oooExecFolder);
                }
                catch
                {
                }
            }
        }
    }


    private void ConvertToPDF(string inputFile, string outputFile, String oooExecFolder)
    {
        String convType = ConvertExtensionToFilterType(Path.GetExtension(inputFile));
        if (convType == null)
            throw new InvalidProgramException("Unknown file type for OpenOffice. File = " + inputFile);

        StartOpenOffice();
        try
        {
           // com.timliu.pdfconv.PdfConvertor.convert(PathConverter(inputFile), PathConverter(outputFile), convType, PathConverter(oooExecFolder));
            pdf2swf_2(inputFile);

        }
        catch { throw; }
        finally { }

    }

    private static void StartOpenOffice()
    {
        Process[] ps = Process.GetProcessesByName("soffice.exe");
        if (ps != null)
        {
            if (ps.Length > 0)
                return;
            else
            {
                Process p = new Process();
                p.StartInfo.Arguments = "-headless -nofirststartwizard";
                p.StartInfo.FileName = TOOLPATH + @"\OpenOfficePortable\App\openoffice\program\soffice.exe";
                p.StartInfo.CreateNoWindow = true;
                bool result = p.Start();
                if (result == false)
                    throw new InvalidProgramException("OpenOffice failed to start.");
            }
        }
        else
        {
            throw new InvalidProgramException("OpenOffice not found.  Is OpenOffice installed?");
        }
    }

    private static string PathConverter(string file)
    {
        if (file == null || file.Length == 0)
            throw new NullReferenceException("Null or empty path passed to OpenOffice");
        file = file.Trim();
        String s = String.Format("{0}", file.Replace(@"\", "/"));
        return s;
    }

    private static string ConvertExtensionToFilterType(string extension)
    {
        switch (extension)
        {
            case ".doc":
            case ".docx":
            case ".txt":
            case ".rtf":
            case ".html":
            case ".htm":
            case ".xml":
            case ".odt":
            case ".wps":
            case ".wpd":
                return "writer_pdf_Export";
            case ".xls":
            case ".xlsb":
            case ".ods":
                return "calc_pdf_Export";
            case ".ppt":
            case ".pptx":
            case ".odp":
                return "impress_pdf_Export";
            default: return null;
        }
    }

    public void pdf2swf_2(string sfile)
    {
        string s = sfile.ToString();//全路径
        int a = s.LastIndexOf(".");

        string s2 = s.Substring(0, a);//文件名no后缀
        try
        {
            String flashPrinter = String.Concat(AppDomain.CurrentDomain.BaseDirectory, "tools/SWFTools1/pdf2swf.exe");//FlashPrinter.exe 
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(flashPrinter);
            startInfo.Arguments = String.Concat(s2 + ".pdf", " -o ", s2 + ".swf -T 9");//绝对路径
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            startInfo.UseShellExecute = false;     //不使用系统外壳程序启动 
            startInfo.RedirectStandardInput = false;   //不重定向输入 
            startInfo.RedirectStandardOutput = false;   //重定向输出 
            startInfo.CreateNoWindow = true;     //不创建窗口 
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo = startInfo;
            Boolean isStart = process.Start();
            process.WaitForExit();
            process.Close();
        }
        catch (Exception ex) { throw ex; }
        finally
        {
            if (!sfile.EndsWith(".pdf"))
            {
                File.Delete(s2 + ".pdf");
            }
            String w2 = s2.Replace("/s/", "/z/").Replace("\\s\\", "\\z\\");
            File.Move(s2 + ".swf", w2 + ".swf");

            File.Move(sfile, sfile.Replace("/s/", "/z/").Replace("\\s\\", "\\z\\"));
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}