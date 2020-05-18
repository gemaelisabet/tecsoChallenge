using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Challenge.Services
{
    public class TecsoLogger
    {      
        private static bool _logToFile;
        private static bool _logToConsole;
        private static bool _logMessage;
        private static bool _logWarning;
        private static bool _logError;
        private static bool _logToDatabase;
        //private bool _initialized;

        public TecsoLogger(bool logToFile, bool logToConsole, bool logToDatabase, bool logMessage, bool logWarning, bool logError)
        {
            _logError = logError;
            _logMessage = logMessage;
            _logWarning = logWarning;
            _logToDatabase = logToDatabase;
            _logToFile = logToFile;
            _logToConsole = logToConsole;

            if (!_logToConsole && !_logToFile && !_logToDatabase)
            {
                throw new Exception("Código incorrecto");
            }
            if ((!_logError && !_logMessage && !_logWarning))
            {
                throw new Exception("Especificar tipo de mensaje");
            }
        }

        public void LogError(string text)
        {
            Log(text, false, false, true);
        }
        public void LogWarning(string text)
        {
            Log(text, false, true, false);
        }
        public void LogMessage(string text)
        {
            Log(text, true, false, false);
        }

        private void Log(string text, bool isMessage, bool isWarning, bool isError)
        {
            // Si no es ninguno de los tipos de Log, salgo de la función
            if (!isMessage && !isWarning && !isError)
            {
                return;
            }
            text.Trim();
            
            if (text == null || text.Length == 0)
            {
                return;
            }

            int tipo = 0;
            if (isMessage && _logMessage)
            {
                tipo = 1;
            }
            if (isError && _logError)
            {
                tipo = 2;
            }
            if (isWarning && _logWarning)
            {
                tipo = 3;
            }

            //Si no se dan ninguno de los casos anteriores, no tengo que loguear, en ninguno de los almacenamientos
            if (tipo==0 ) return;

            //Comienzo la conexión a la bbdd y ejecuto la query
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["TecsoLoggerConnectionString"]);
            connection.Open();
            string tabla = System.Configuration.ConfigurationManager.AppSettings["TecsoLoggerTable"];
            string query = "INSERT INTO "+ tabla + "(TipoLog, Text, DateTime) VALUES('" + tipo.ToString() + "','" + text + "', GETDATE())";
            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
     
            command.ExecuteNonQuery();

            connection.Close();

            //Preparo el texto para crear el archivo, o agregarlo al archivo existente
            string textoArchivo = "";
            string path = ConfigurationManager.AppSettings["TecsoLoggerFilePath"] + "ArchivoLog_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            if (System.IO.File.Exists(path))
            {
                textoArchivo = System.IO.File.ReadAllText(path);
            }

            textoArchivo = textoArchivo + " "+ DateTime.Now.ToString() + " " + text;
    
            System.IO.File.WriteAllText(path, textoArchivo + Environment.NewLine);

            Console.WriteLine(DateTime.Now.ToShortDateString() + text);
        }
    }
}