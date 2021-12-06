using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiRestConsume;
using System.Data;

namespace PruebaConsumoAPIDLL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hola mundo");

            get();
            post();
            
            Console.ReadKey();
        }

        static void get()
        {
            string url = "https://apprest.deacero.com/APPCONTAINER_WS/WebService.asmx/ValidatePassword?Usuario=904472&Password=OIBL980304L83";            
            Consume consume = new Consume();
            consume.sslVerification = false;
            String mensaje = consume.conectar(url, "get");
            Console.WriteLine(mensaje);
        }

        static void post()
        {
             //string url = "https://dealke03/api/eurus/complemento_carga/";
            string url = "https://srvanalyt/api/eurus/model/Response";
            Dictionary<String, String> dataDictionary = new Dictionary<string, string>
            {
                {"variable1", "52"},
                {"variable2", "52"},
                {"variable3", "52"},
                {"variable4", "52"},
                {"variable5", "52"}
            };

            string data = "{\"id_modelo\":5}";

            Consume consume = new Consume();
            consume.sslVerification = false;
            String mensaje = consume.conectar(url, "post", dataDictionary);
            Console.WriteLine(mensaje);
        }
    }
}
