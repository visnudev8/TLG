using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Xml;
using System.Xml.Xsl;



namespace Learning.Controllers
{
    public class XmlworkController : Controller
    {
        // GET: Xmlwork
        // GET: Home  
        public ActionResult Xmltransform()
        {
            return View();
        }





        // GET: Home  
        public ActionResult DXmltransform()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DXmltransform(HttpPostedFileBase[] files, HttpPostedFileBase file2)
        {


            var InputxslFileName = Path.GetFileName(file2.FileName);
            var xslServerSavePath = Path.Combine(Server.MapPath("~/Uploadfiles/") + InputxslFileName);
            //Save file to server folder  
            file2.SaveAs(xslServerSavePath);

            //Ensure model state is valid  
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Uploadfiles/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);


                        //file Extension name
                        var Fileext = Path.GetExtension(file.FileName);
                        var Filnamewext = Path.GetFileNameWithoutExtension(file.FileName);

//                        var xslpath = Path.Combine(Server.MapPath("~/Uploadfiles/") + "f.xsl");

                        XslCompiledTransform trans = new XslCompiledTransform();

                        trans.Load(xslServerSavePath);

                        XmlTextWriter myWriter = new XmlTextWriter(Path.Combine(Server.MapPath("~/Uploadfiles/") + Filnamewext + ".html"), null);
                        trans.Transform(ServerSavePath, null, myWriter);

                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                    }

                }
            }
            return View();
        }




        [HttpPost]
        public ActionResult Xmltransform(HttpPostedFileBase[] files)
        {

            //Ensure model state is valid  
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Uploadfiles/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);


                        //file Extension name
                        var Fileext = Path.GetExtension(file.FileName);
                        var Filnamewext = Path.GetFileNameWithoutExtension(file.FileName);

                        var xslpath = Path.Combine(Server.MapPath("~/Uploadfiles/") + "f.xsl");

                        XslCompiledTransform trans = new XslCompiledTransform();

                        trans.Load(xslpath);

                        XmlTextWriter myWriter = new XmlTextWriter(Path.Combine(Server.MapPath("~/Uploadfiles/") + Filnamewext + ".html"), null);
                        trans.Transform(ServerSavePath, null, myWriter);

                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                    }

                }
            }
            return View();
        }
    }
}
