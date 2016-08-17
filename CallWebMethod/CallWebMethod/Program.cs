using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CallWebMethod
{
    class Program
    {
        static void Main(string[] args)
        {

            var res = ExampleAPIProxy.ExampleWebMethod("Fahrenheit", 100);
            Console.Write(res);
            Console.Read();

        }
    }
    
    
    //Following is the webservice that is hosted at : http://localhost:86/Convert.asmx
    //https://msdn.microsoft.com/en-us/library/8wbhsy70(v=vs.90).aspx


    // Following is method to invoke it

    public class ExampleAPIProxy
    {
        private static WebService ExampleAPI = new WebService("http://localhost:86/Convert.asmx");    // DEFAULT location of the WebService, containing the WebMethods

        public static void ChangeUrl(string webserviceEndpoint)
        {
            ExampleAPI = new WebService(webserviceEndpoint);
        }

        public static string ExampleWebMethod(string name, int number)
        {
            ExampleAPI.PreInvoke();

            ExampleAPI.AddParameter(name, number.ToString());                    // Case Sensitive! To avoid typos, just copy the WebMethod's signature and paste it
            //ExampleAPI.AddParameter("number", number.ToString());     // all parameters are passed as strings
            try
            {
                ExampleAPI.Invoke("FahrenheitToCelsius");                // name of the WebMethod to call (Case Sentitive again!)
            }
            finally { ExampleAPI.PosInvoke(); }

            return ExampleAPI.ResultString;                           // you can either return a string or an XML, your choice
        }
    }
}
