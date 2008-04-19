using System;
using System.Web;
using System.Net;
using System.Web.Hosting;
using System.Drawing;
using System.IO;
using System.Configuration;
using Thon.ZaszBlog.Support;

namespace Thon.ZaszBlog.Support.Web.HttpHandlers
{
  /// <summary>
  /// Creates images of monsters based on a md5 hash. It is used for displaying avatar icons in the comments.
  /// This handler is based on the MonsterID HttpHandler by Alexander Schuc
  /// http://blog.furred.net/page/MonsterID-Demo.aspx
  /// </summary>
  public class MonsterHandler : IHttpHandler
  {
  
    #region Private fields

    private const string CACHE_PATH = "~/App_Data/ZaszBlog/MonsterCache/{0}";
    private const string PARTS_PATH = "~/ZaszBlog/Images/MonsterParts";

    private static readonly string[] parts = new string[] { "legs", "hair", "arms", "body", "eyes", "mouth" };
    private static readonly int[] partcount = new int[] { 5, 5, 5, 15, 15, 10 };

    #endregion

    #region IHttpHandler Members

    public void ProcessRequest(HttpContext context)
    {
      if (String.IsNullOrEmpty(context.Request.QueryString["seed"]) || String.IsNullOrEmpty(context.Request.QueryString["size"]))
        return;

      string md5 = context.Request.QueryString["seed"];// md-5 hashes, take 6 digits out
      int seed = int.Parse(md5.Substring(0, 6), System.Globalization.NumberStyles.HexNumber);
      int size = int.Parse(context.Request.QueryString["size"]);

      string filename = GetMonsterFilename(seed, size);

      if (!File.Exists(filename))
        CreateMonster(seed, size, filename);

      SetHttpHeaders(context, md5);
      context.Response.WriteFile(filename);
    }

    public bool IsReusable
    {
      get { return true; }
    }

    #endregion
    
    // Same concept of caching. I wrote in some other .cs file ,I copy-pasted it everywhere..
    private static void SetHttpHeaders(HttpContext context, string md5)
    {
        string etag = "\"" + md5.GetHashCode() + "\"";
        string incomingEtag = context.Request.Headers["If-None-Match"];

        context.Response.ContentType = "image/png";
        context.Response.Cache.SetCacheability(HttpCacheability.Public);
        context.Response.Cache.SetExpires(DateTime.Now.ToUniversalTime().AddYears(1));
        context.Response.Cache.SetETag(md5);

        if (String.Compare(incomingEtag, etag) == 0)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotModified;
            context.Response.End();
        }
    }
          
    // Gets the filename of the monster based on the seed and size, from the cache folder/sizesubfolder    
    private static string GetMonsterFilename(int seed, int size)
    {      
        return Path.Combine(HostingEnvironment.MapPath(string.Format(CACHE_PATH, size)), string.Format("{0}.png", seed));
    }

    // Creates the monster image and saves it to the cache on disk.
    private static void CreateMonster(int seed, int size, string filename)
    {
        string sourcedir = HostingEnvironment.MapPath(PARTS_PATH);
        Random rnd = new Random(seed);
        int[] currentParts = new int[parts.Length];

        for (int i = 0; i < currentParts.Length; i++)
        {
            currentParts[i] = rnd.Next(1, partcount[i]);
        }

        using (Bitmap bmp = new Bitmap(size, size))
        {
            Bitmap overlay;
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                gfx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                for (int i = 0; i < currentParts.Length; i++)
                {
                    using (overlay = new Bitmap(Path.Combine(sourcedir, string.Format("{0}_{1}.png", parts[i], currentParts[i]))))
                    {
                        Rectangle rect = new Rectangle(new Point(0), overlay.Size);
                        gfx.DrawImage(overlay, new Rectangle(new Point(0), bmp.Size), rect, GraphicsUnit.Pixel);
                    }
                }
            }
            string path = Path.GetDirectoryName(filename);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

  }
}
