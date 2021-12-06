using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestConsume
{
    public class Consume
    {
        public bool sslVerification { get; set;}

        public Consume()
        {
            this.sslVerification = true;
        }

        public String conectar(string endPoint, string method, string dataJSON)
        {
            evaluarSSL();
            string response;
            method = method.ToUpper();
            switch (method)
            {
                case "POST":
                    response = this.postRequest(endPoint, dataJSON);
                    break;
                case "GET":
                    response = "POR EL MOMENTO NO SE HA AGREGADO UNA FUNCIONALIDAD ACEPTABLE PARA EL METODO GET CON DATOS DENTRO DEL BODY";
                    break;
                default:
                    response = "METODO HTTP INCORRECTO, ESTA FUNCION POR EL MOMENTO SOLO ADMITE GET O POST COMO METODOS HTTP";
                    break;
            }
            return response;
        }

        public String conectar(string endPoint, string method, Dictionary<String, String> dataDictionary)
        {
            evaluarSSL();
            string response;
            method = method.ToUpper();
            switch (method)
            {
                case "POST":
                    string json = JsonConvert.SerializeObject(dataDictionary);
                    response = this.postRequest(endPoint, json);
                    //response = this.postRequest(endPoint, dataDT);
                    break;
                case "GET":
                    response = "POR EL MOMENTO NO SE HA AGREGADO UNA FUNCIONALIDAD ACEPTABLE PARA EL METODO GET CON DATOS DENTRO DEL BODY";
                    break;
                default:
                    response = "METODO HTTP INCORRECTO, ESTA FUNCION SOLO ADMITE GET O POST COMO METODOS HTTP";
                    break;
            }
            return response;
        }

        public String conectar(string endPoint, string method)
        {
            evaluarSSL();
            string response;
            method = method.ToUpper();
            switch (method)
            {
                case "POST":
                    response = this.postRequest(endPoint);
                    break;
                case "GET":
                    response = this.getRequest(endPoint);
                    break;
                default:
                    response = "METODO HTTP INCORRECTO, ESTA FUNCION SOLO ADMITE GET O POST COMO METODOS HTTP";
                    break;
            }
            return response;
        }

        internal String postRequest(string endPoint, string dataJSON)
        {
            string response = request(endPoint, dataJSON);
            return response;
        }

        internal String postRequest(string endPoint)
        {
            string response = request(endPoint);
            return response;
        }

        internal String getRequest(string endPoint, string dataJSON = null)
        {
            string response = getLogin(endPoint);
            return response;
        }

        internal String request(string endPoint, string dataJSON = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.ContentType = "application/json";
            request.Method = "POST";                     
            request.AutomaticDecompression = DecompressionMethods.GZip;

            string json = String.Empty;
            try 
            {
                if (!string.IsNullOrEmpty(dataJSON))
                {
                    StreamWriter streamWriter = new StreamWriter(request.GetRequestStream());
                    streamWriter.Write(dataJSON);
                    streamWriter.Flush();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();

                StreamReader reader = new StreamReader(stream);
                json = reader.ReadToEnd();
            }catch(Exception ex)
            {
                return "Error al alcanzar el endpoint " + ex.Message;
            }
            

            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            return json;
        }

        internal String getLogin(string endPoint)
        {   
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            string json = String.Empty;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();

                StreamReader reader = new StreamReader(stream);
                json = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return "Error al alcanzar el endpoint " + ex.Message;
            }

            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            return json;
        }

        internal void evaluarSSL()
        {
            if (this.sslVerification)
            {
                activarSSL();
            }
            else {
                desactivarSSL();
            }
        }

        internal void activarSSL()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = null;
        }

        internal void desactivarSSL()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
                ((sender, certificate, chain, sslPolicyErrors) => true);
        }
    }
}
