using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Diagnostics;
using System.Web;


namespace SuperHeroCatalogue.Infra.Data.Log
{
    public class LoggerService
    {
        public static string LogFolder
        {
            get
            {

                return ConfigurationManager.AppSettings["PathLog"] ?? HttpContext.Current.Server.MapPath("~/App_Data/Log/");
            }
        }

        string category = ConfigurationManager.AppSettings["LoggerCategory"] ?? "Error";

        public void LogException(DbCommand comando, Exception ex)
        {
            LogEntry entry = new LogEntry();
            entry.ExtendedProperties = ConvertToIDictionary(comando.Parameters);
            SetURLToEntry(entry);
            entry.Severity = TraceEventType.Critical;
            entry.Message = "Procedure: " + comando.CommandText + " - Mensagem: " + ex.Message + " - Stack Trace: " + ex.StackTrace;

            entry.Categories.Add(category);

            //Logger.SetLogWriter(new LogWriterFactory().Create(), false);
            Logger.Write(entry);
        }

        public void LogException(Exception ex)
        {
            this.LogMessage(ex.ToString(), TraceEventType.Error, this.category);
        }

        public void LogException(Exception ex, params string[] categories)
        {
            this.LogMessage(ex.ToString(), TraceEventType.Error, categories);
        }

        public void LogMessage(string message)
        {
            this.LogMessage(message, TraceEventType.Information, this.category);
        }

        public void LogMessage(string message, params string[] categories)
        {
            this.LogMessage(message, TraceEventType.Information, categories);
        }

        public void LogMessage(string message, TraceEventType eventType, params string[] categories)
        {
            LogEntry entry = new LogEntry();
            entry.Message = message;
            entry.Severity = eventType;

            SetURLToEntry(entry);

            foreach (string categoria in categories)
                entry.Categories.Add(categoria);

            Logger.Write(entry);
        }

        static void SetURLToEntry(LogEntry entry)
        {
            try
            {
                if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Request != null)
                {
                    entry.ExtendedProperties.Add("URL", System.Web.HttpContext.Current.Request.RawUrl);
                    if (System.Web.HttpContext.Current.Request.UrlReferrer != null)
                    {
                        entry.ExtendedProperties.Add("URL Referrer", System.Web.HttpContext.Current.Request.UrlReferrer.ToString());
                    }
                }
            }
            catch { }
        }

        static IDictionary<string, object> ConvertToIDictionary(DbParameterCollection parameters)
        {
            Dictionary<string, object> collection = new Dictionary<string, object>();

            foreach (DbParameter item in parameters)
                collection.Add(item.ParameterName, item.Value.ToString());

            return collection;
        }

    }
}
