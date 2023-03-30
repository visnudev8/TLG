using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Schema;
using System.Xml.Linq;
using System.Text;
using Learning.Models;

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




        public ActionResult Xmlvalidationxsd()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Xmlvalidationxsd(HttpPostedFileBase[] files)
        {

            try
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


                            XmlReaderSettings booksSettings = new XmlReaderSettings();
                            booksSettings.Schemas.Add("http://www.contoso.com/books", Server.MapPath("~/Uploadfiles/f.xsd"));
                            booksSettings.ValidationType = ValidationType.Schema;
                            booksSettings.ValidationEventHandler += booksSettingsValidationEventHandler;
                            XmlReader books = XmlReader.Create(ServerSavePath, booksSettings);

                            while (books.Read()) { }


                            /* 
                                                    var xslpath = Path.Combine(Server.MapPath("~/Uploadfiles/") + "f.xsd");

                                                    XslCompiledTransform trans = new XslCompiledTransform();

                                                    trans.Load(xslpath);

                                                    XmlTextWriter myWriter = new XmlTextWriter(Path.Combine(Server.MapPath("~/Uploadfiles/") + Filnamewext + ".html"), null);
                                                    trans.Transform(ServerSavePath, null, myWriter);


                                                    */

                            //assigning file uploaded status to ViewBag for showing message to user.  
                            ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                        }

                    }
                }

            }

            catch (Exception e)
            {

                ViewBag.xmlmgs = e.Message;

            }

            return View();
        }









        //This function is for store ALL XML ERROR list

        public int nErrors = 0;
        public string strErrorMsg = string.Empty;
        public string Errors { get { return strErrorMsg; } }

//        int nErrors = 0;
   //    string strErrorMsg = string.Empty;
     //   public string Errors { get { return strErrorMsg; } }


//        string Errors = strErrorMsg;

        public ActionResult Xmlvalidationxsd2()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Xmlvalidationxsd2(HttpPostedFileBase[] files)
        {
            try
            {

                //Ensure model state is valid  
                if (ModelState.IsValid)
                {

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


                            if (!IsValidXml(ServerSavePath, Server.MapPath("~/Uploadfiles/f-xsd-for-allerrorlist.xsd")))
                            {

                                ViewBag.ermsg = Errors;
                            }

                            else
                            { 
                                ViewBag.ermsg = string.Format("Success");
                            }




//                            XmlReaderSettings booksSettings = new XmlReaderSettings();
  //                          booksSettings.Schemas.Add("http://www.contoso.com/books", Server.MapPath("~/Uploadfiles/f.xsd"));
    //                        booksSettings.ValidationType = ValidationType.Schema;
      //                      booksSettings.ValidationEventHandler += booksSettingsValidationEventHandler;
        //                    XmlReader books = XmlReader.Create(ServerSavePath, booksSettings);

          //                  while (books.Read()) { }


                            /* 
                                                    var xslpath = Path.Combine(Server.MapPath("~/Uploadfiles/") + "f.xsd");

                                                    XslCompiledTransform trans = new XslCompiledTransform();

                                                    trans.Load(xslpath);

                                                    XmlTextWriter myWriter = new XmlTextWriter(Path.Combine(Server.MapPath("~/Uploadfiles/") + Filnamewext + ".html"), null);
                                                    trans.Transform(ServerSavePath, null, myWriter);


                                                    */

                            //assigning file uploaded status to ViewBag for showing message to user.  
                            ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                        }

                    }
                }

            }

            catch (Exception e)
            {

                ViewBag.xmlmgs = e.Message;

            }

            return View();
        }





        public ActionResult Xmlvalidationxsd1()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Xmlvalidationxsd1(HttpPostedFileBase[] files)
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




                        XDocument doc = XDocument.Load(ServerSavePath);
                        XmlSchemaSet set = new XmlSchemaSet();
                        set.Add("http://www.contoso.com/books", Server.MapPath("~/Uploadfiles/f.xsd"));
                        StringBuilder errors = new StringBuilder();
                        doc.Validate(set, (sender, args) => { errors.AppendLine(args.Exception.Message); });

                        ViewBag.msg = errors.ToString();


                        /*

                        XmlReaderSettings booksSettings = new XmlReaderSettings();
                        booksSettings.Schemas.Add("http://www.contoso.com/books", Server.MapPath("~/Uploadfiles/f.xsd"));
                        booksSettings.ValidationType = ValidationType.Schema;
                        booksSettings.ValidationEventHandler += booksSettingsValidationEventHandler;
                        XmlReader books = XmlReader.Create(ServerSavePath, booksSettings);

                        while (books.Read()) { }


                        */

                        /* 
                                                var xslpath = Path.Combine(Server.MapPath("~/Uploadfiles/") + "f.xsd");

                                                XslCompiledTransform trans = new XslCompiledTransform();

                                                trans.Load(xslpath);

                                                XmlTextWriter myWriter = new XmlTextWriter(Path.Combine(Server.MapPath("~/Uploadfiles/") + Filnamewext + ".html"), null);
                                                trans.Transform(ServerSavePath, null, myWriter);


                                                */

                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                    }

                }
            }

            return View();
        }











        public ActionResult Xmlvalidationdtd()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Xmlvalidationdtd(HttpPostedFileBase[] files)
        {

            try
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

                            XmlReaderSettings settings = new XmlReaderSettings();
                            settings.DtdProcessing = DtdProcessing.Parse;
                            settings.ValidationType = ValidationType.DTD;


                            XmlUrlResolver resolver = new XmlUrlResolver();
                            resolver.Credentials = System.Net.CredentialCache.DefaultCredentials;

                            // Set the reader settings object to use the resolver.
                            settings.XmlResolver = resolver;

                            XmlReader reader = XmlReader.Create(ServerSavePath, settings);


                            while (reader.Read()) { }

                            ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                        }

                    }
                }

            }

            catch (Exception e)

            {
                ViewBag.msg += e.Message;

            }

            return View();
        }






        private void booksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                ViewBag.Example1 = "WARNING: ";

                //                Console.Write("WARNING: ");

                ViewBag.Example2 = e.Message;

                //                Console.WriteLine(e.Message);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                ViewBag.Example3 = "ERROR: ";

                //                Console.Write("ERROR: ");

                ViewBag.Example3 = e.Message;

                //                Console.WriteLine(e.Message);
            }
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




        public ActionResult Xmldetailsfile()
        {

            //Fetch all files in the Folder (Directory).
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/Uploadfiles/"));

            //Copy File names to Model collection.
            List<File1Model> files = new List<File1Model>();
            foreach (string filePath in filePaths)
            {
                files.Add(new File1Model { FileName = Path.GetFileName(filePath) });
            }

            return View(files);

        }


        [HttpPost]
        public ActionResult Xmldownldfile(string fileName)
        {


           // string path = Server.MapPath(string.Format("~/Uploadfiles/{0}", fileName));

            //Set the File Folder Path.
//                string path = Server.MapPath("~/Uploadfiles/");

                //Read the File as Byte Array.
//                byte[] bytes = System.IO.File.ReadAllBytes(path);

                //Convert File to Base64 string and send to Client.
   //             string base64 = Convert.ToBase64String(bytes, 0, bytes.Length);

       //         return Content(base64);



               byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(string.Format("~/Uploadfiles/{0}", fileName)));

//            byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath("~/Uploadfiles/") + fileName));

            //            byte[] fileBytes = Server.MapPath("~/Uploadfiles/1.pdf");

            //            byte[] fileBytes = System.IO.File.ReadAllBytes("~/Uploadfiles/1.pdf");

            string file = fileName;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file);



            //Build the File Path.
//            string path = Path.Combine(Server.MapPath("~/Uploadfiles/"),  fileName);

            //Read the File data into Byte Array.
  //          byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
    //        return File(bytes, "application/octet-stream", fileName);

       //     */

        }



        public FileResult Downloadf(string fileName)
        {

            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(string.Format("~/Uploadfiles/{0}", fileName)));

            //            byte[] fileBytes = Server.MapPath("~/Uploadfiles/1.pdf");

//            byte[] fileBytes = System.IO.File.ReadAllBytes("~/Uploadfiles/1.pdf");
            string ffile = fileName;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, ffile);
        }





        public bool IsValidXml(string xmlPath, string xsdPath)
        {
            bool bStatus = false;
            try
            {
                // Declare local objects
                XmlReaderSettings rs = new XmlReaderSettings();
                rs.ValidationType = ValidationType.Schema;
                rs.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation | XmlSchemaValidationFlags.ReportValidationWarnings;
                // Event Handler for handling exception & this will be called whenever any mismatch between XML & XSD
                rs.ValidationEventHandler += new ValidationEventHandler(ValidationEventHandler);
                rs.Schemas.Add(null, XmlReader.Create(xsdPath));
                // reading xml
                using (XmlReader xmlValidatingReader = XmlReader.Create(xmlPath, rs))
                { while (xmlValidatingReader.Read()) { } }
                ////Exception if error.
                if (nErrors > 0)
                {
                    throw new Exception(strErrorMsg);
                }
                else { bStatus = true; }//Success
            }
            catch (Exception error)
            { bStatus = false; }
            return bStatus;
        }


        void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                strErrorMsg += "WARNING: ";

            }
          
            else
            {
                strErrorMsg += "ERROR: ";
                nErrors++;
            }

            if (e.Exception.Message.Contains("'Email1' element is invalid"))
            {
                strErrorMsg = strErrorMsg + getErrorString("Provided Email data is Invalid", "CAPIEMAIL007") + "\r\n";
            }

/*
            if (e.Exception.Message.Contains("The element 'Person' has invalid child element"))
            {
                strErrorMsg = strErrorMsg + getErrorString("Provided XML contains invalid child element", "CAPINVALID005") + "\r\n";
            }


            */

            else
            {
                strErrorMsg = strErrorMsg + e.Exception.Message + "\r\n";
            }
        }


        string getErrorString(string erroString, string errorCode)
        {
            StringBuilder errXMl = new StringBuilder();
            errXMl.Append("<MyError> <errorString> ERROR_STRING </errorString> <errorCode> ERROR_CODE </errorCode> </MyError>");
            errXMl.Replace("ERROR_STRING", erroString);
            errXMl.Replace("ERROR_CODE", errorCode);
            return errXMl.ToString();

        }







    }
}
