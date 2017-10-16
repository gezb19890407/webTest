using log4net.Appender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using log4net.Core;
using log4net.Layout;

namespace testWebApplication.web.log4netTest
{
    public class CacheBaseAppender : AppenderSkeleton
    {
        public static ObjectCache cache = MemoryCache.Default;

        public string Name { get; set; }

        public CacheBaseAppender(string name)
        {
            Name = name;
            if (cache[Name] == null)
            {
                cache[Name] = new List<string>();
            }
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            PatternLayout patternLayout = this.Layout as PatternLayout;

            var str = string.Empty;
            List<String> stringList = cache[Name] as List<String>;
            if (patternLayout != null)
            {
                stringList.Add(patternLayout.Format(loggingEvent));

                if (loggingEvent.ExceptionObject != null)
                {
                    stringList.Add(loggingEvent.ExceptionObject.ToString() + Environment.NewLine);
                }
            }
            else
            {
                stringList.Add(loggingEvent.LoggerName + "-" + loggingEvent.RenderedMessage + Environment.NewLine);
            }
        }
    }
}