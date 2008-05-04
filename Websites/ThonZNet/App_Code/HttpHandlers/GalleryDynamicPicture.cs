using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Thon.Gallery
{
    public class GalleryDynamicPicture : IHttpHandler
    {
        public void ProcessRequest(HttpContext Context)
        {
            try
            {
                Image OriginalImg;
                Image NewImg;
                int ImgHeight;
                int ImgWidth;
                int MaxWidth;
                int MaxHeight;

                OriginalImg = Image.FromFile(Context.Server.MapPath(Context.Request.QueryString["Target"]));
                ImgHeight = OriginalImg.Height;
                ImgWidth = OriginalImg.Width;

                if (!string.IsNullOrEmpty(Context.Request.QueryString["Width"]))
                    MaxWidth = int.Parse(Context.Request.QueryString["Width"]);
                else
                    MaxWidth = ImgWidth;

                if (!string.IsNullOrEmpty(Context.Request.QueryString["Height"]))
                    MaxHeight = int.Parse(Context.Request.QueryString["Height"]);
                else
                    MaxHeight = ImgHeight;

                if (Context.Request["Download"] == "True")
                {
                    string SaveName = Context.Request.QueryString["Target"].Substring(Context.Request.QueryString["Target"].LastIndexOf("/"));
                    SaveName = SaveName.Substring(SaveName.LastIndexOf("/"));
                    Context.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + SaveName + "\"");
                }

                Context.Response.ContentType = "image/jpeg";

                if (ImgWidth > MaxWidth | ImgHeight > MaxHeight)
                {

                    //Determine what dimension is off by more
                    double ScaleFactor;

                    if (((double)MaxHeight / (double)ImgHeight) < ((double)MaxWidth / (double)ImgWidth))
                    {
                        //Scale by the height
                        ScaleFactor = (double)MaxHeight / (double)ImgHeight;
                    }
                    else
                    {
                        //Scale by the Width
                        ScaleFactor = (double)MaxWidth / (double)ImgWidth;
                    }

                    ImgWidth = (int)(ImgWidth * ScaleFactor);
                    ImgHeight = (int)(ImgHeight * ScaleFactor);

                    //Normal, small files, decent quality
                    NewImg = GenerateThumbnail(OriginalImg, ImgWidth, ImgHeight);
                    NewImg.Save(Context.Response.OutputStream, ImageFormat.Jpeg);
                    NewImg.Dispose();
                }

                else
                    //Just return the original, no working on it
                    Context.Response.WriteFile(Context.Server.MapPath(Context.Request.QueryString["Target"]));
                OriginalImg.Dispose();
            }
            catch { }
        }

        private System.Drawing.Image GenerateThumbnail(Image Original, int Width, int Height)
        {
            Bitmap tn = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(tn);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(Original, new Rectangle(0, 0, tn.Width, tn.Height), 0, 0, Original.Width, Original.Height, GraphicsUnit.Pixel);
            g.Dispose();
            return (System.Drawing.Image)tn;
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}