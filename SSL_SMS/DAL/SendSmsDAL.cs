using SSL_SMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Net;
using System.IO;

namespace SSL_SMS.DAL
{
    public class SendSmsDAL
    {
        internal string SendSms(string contacts, string messages)
        {
            string result = "";
            WebRequest request = null;
            HttpWebResponse response = null;

            try
            {
                String to = contacts;
                String token = "a614b41374ce79aaefdd8c08585306ec";
                String message = System.Uri.EscapeUriString(messages);
                String url = "http://api.greenweb.com.bd/api.php?token=" + token + "&to=" + to + "&message=" + message;
                request = WebRequest.Create(url);

                // Send the 'HttpWebRequest' and wait for response.
                response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader reader = new
                System.IO.StreamReader(stream, ec);
                result = reader.ReadToEnd();
                Console.WriteLine(result);
                reader.Close();
                stream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (response != null)
                    response.Close();
            }

            //if ((result.ToLower()).Contains("error"))
            //    return false;
            //else
            //    return true;

            return result;
        }
    }
}