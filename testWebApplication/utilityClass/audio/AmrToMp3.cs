using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using utilityClass.generic;
using utilityClass.cmd;

namespace utilityClass.audio
{
    public class AmrToMp3
    {
        public static void ConvertToMp3(string pathBefore, string pathLater)
        {
            string c = GenericData.BaseDirectory + "utilityClass/ffmpeg/" + @"ffmpeg.exe -i /y " + pathBefore + " " + pathLater;
            ExcuteCmd.Execute(c);
        }
    }
}