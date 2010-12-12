using System;
using System.Diagnostics;
using System.IO;

namespace read_more
{
    public class TraceLog
    {
        private static TraceLog log = null;
        
        private TraceLog()
        {
            StreamWriter errLog = new StreamWriter(CommonUtil.GetRelativeAppDir("errorlog.log"),true);
            TextWriterTraceListener myTextListener = new
               TextWriterTraceListener(errLog);
            Trace.Listeners.Add(myTextListener);
            Trace.AutoFlush = true;
        }

        public static TraceLog GetInstance()
        {
            if(log==null)
            {
                log=new TraceLog();
            }
            return log;
        }

        public void WriteError(Exception e)
        {
            Trace.WriteLine(e.Message);
            Trace.Indent();
            Trace.WriteLine(e.StackTrace);
            Trace.Unindent();
            Trace.WriteLine("----------------------------------------------------------------------");
        }
    }
}
