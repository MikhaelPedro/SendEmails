using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using System.Net;
using System.Net.Mail;
using SendEmail.Models;
using System.Text;

namespace SendEmail.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //public string Senha()
        //{
        //    //string password = "";
        //    string carac = "abcdefghijklmnopqrstuvxwyzABCDEFGHIJKLMNOPQRSTUVXWYZ0123456789#/$!?=-\"";
        //    Random rnd = new Random();

        //    StringBuilder password = new StringBuilder();
        //    for (int m = 1; m <= 12; m++)
        //    {
        //        int pos = rnd.Next(0, carac.Length);
        //        password.Append(carac[pos].ToString());
        //    }
        //    return password.ToString();
        //}

        public JsonResult Reader()
        {

            var files = Request.Files[0];
            var fileName = Path.GetFileName(files.FileName);
            var path = Path.Combine(Server.MapPath("~/Arquivos"), fileName);
            files.SaveAs(path);

            List<EmailModel> users = new List<EmailModel>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var package = new ExcelPackage(new FileInfo(path));
            var workbook = package.Workbook;
            var sheets = workbook.Worksheets[0];

            var excel = workbook.Worksheets.FirstOrDefault();

            int contaColuna = excel.Dimension.End.Column;
            int contaLinha = excel.Dimension.End.Row;

            for (int row = 2; row <= contaLinha; row++)
            {
                EmailModel user = new EmailModel();
                for (int col = 1; col <= contaColuna; col++)
                {
                    if (col == 1)
                    {
                        user.Name = excel.Cells[row, col].Value?.ToString();
                    }
                    else if (col == 2)
                    {
                        user.Email = excel.Cells[row, col].Value?.ToString();
                    }
                    string carac = "abcdefghijklmnopqrstuvxwyzABCDEFGHIJKLMNOPQRSTUVXWYZ0123456789#/$!?=-\"";
                    Random rnd = new Random();

                    StringBuilder password = new StringBuilder();
                    for (int m = 1; m <= 12; m++)
                    {
                        int pos = rnd.Next(0, carac.Length);
                        password.Append(carac[pos].ToString());
                    }
                    user.Password = password.ToString(); //Guid.NewGuid().ToString().Replace("-", "");
                    users.Add(user);
                }
                
            }
            SendMail(users);
            return Json(new { success = true });
        }


        public void SendMail(List<EmailModel> email)
        {
            foreach (var item in email)
            {
                try
                {
                    if (item.Email == null)
                    {
                        continue;
                    }
                    //MailMessage mail = new MailMessage();
                    ////mail.To.Add(item.Email);
                    //mail.From = new MailAddress("Mikhaelpedro18@gmail.com");
                    //mail.Subject = "Recuperação de Login";
                    //string Body = $"Ola {item.Name}, o seu E-mail é: {item.Email} e a sua senha: {item.Password} ";
                    //mail.Body = Body;
                    //mail.IsBodyHtml = true;

                    //preparar a mensagem para enviar
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("mikhaelpedro@gmail.com");
                    message.Subject = "Recuperação de Senha";
                    message.IsBodyHtml = true;
                    message.Body = "<strong>BEM VINDO: </strong>" + item.Name + "<br><br>" + "Usuername: " + item.Email + "<br>" + "Senha: " + item.Password; //+ senha.ToString();
                    message.To.Add(item.Email);

                    //Instância smtp do servidor, neste caso o gmail.
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("mikhaelpedro", "76917763");// Login e senha do e-mail.
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }

        public JsonResult Sender(string name, string email)
        {
            EmailModel user = new EmailModel();
            user.Name = (name);
            user.Email = (email);

            string carac = "abcdefghijklmnopqrstuvxwyzABCDEFGHIJKLMNOPQRSTUVXWYZ0123456789#/$!?=-\"";
            Random rnd = new Random();

            StringBuilder password = new StringBuilder();
            for (int m = 1; m <= 12; m++)
            {
                int pos = rnd.Next(0, carac.Length);
                password.Append(carac[pos].ToString());
            }
            user.Password = password.ToString();
            SendMailOnly(user.Name, user.Email, user.Password);
            return Json(new { success = true });
            }

        public void SendMailOnly(string name, string email, string password)
        {
            
            //preparar a mensagem para enviar
            MailMessage message = new MailMessage();
            message.From = new MailAddress("mikhaelpedro@gmail.com");
            message.Subject = "Recuperação de Senha";
            message.IsBodyHtml = true;
            message.Body = "<strong>BEM VINDO: </strong>" + name + "<br><br>" + "Usuername: " + email + "<br>" + "Senha: " + password; //+ senha.ToString();
            message.To.Add(email);

            //Instância smtp do servidor, neste caso o gmail.
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("mikhaelpedro", "76917763");// Login e senha do e-mail.
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);

        }
    }

}