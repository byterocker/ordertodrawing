using OrderToDrawing.Models;
using System.Collections.Generic;

namespace OrderToDrawing
{

    public static class ACAD_SerialNumberExtension
    {
        //public static List<T> ToDTO<T>(this IEnumerable<BaseModel> models)
        private static List<string> GetPO_Numbers_<T>(this List<ACAD_SerialNumber> data)
        {
            List<string> singled = new List<string>();
            foreach (ACAD_SerialNumber item in data)
            {
                if (!singled.Exists(x => x.Equals(item.PO_Number)))
                {
                    singled.Add(item.PO_Number);
                    //Console.WriteLine(item.AnlagenReferenz + " Added.");
                }
            }
            return singled;
        }
        private static List<string> GetReferenz_<T>(this List<ACAD_SerialNumber> data)
        {
            List<string> singled = new List<string>();
            foreach (ACAD_SerialNumber item in data)
            {
                if (!singled.Exists(x => x.Equals(item.Drawing_No)))
                {
                    singled.Add(item.Drawing_No);
                    //Console.WriteLine(item.AnlagenReferenz + " Added.");
                }
            }
            return singled;
        }
        private static List<string> GetSequenzes_<T>(this List<ACAD_SerialNumber> data)
        {
            List<string> singled = new List<string>();
            foreach (ACAD_SerialNumber item in data)
            {
                if (!singled.Exists(x => x.Equals(item.SO_Position)))
                {
                    singled.Add(item.SO_Position);
                    //Console.WriteLine(item.AnlagenReferenz + " Added.");
                }
            }
            return singled;
        }
        public static List<List<ACAD_SerialNumber>> Seperate<T>(this List<ACAD_SerialNumber> data)
        {
            List<List<ACAD_SerialNumber>> Result = new List<List<ACAD_SerialNumber>>();

            foreach (string a in data.GetReferenz_<List<ACAD_SerialNumber>>()) //gibt jede Referenz die enthalten ist einmal in die Liste
            {
                List<ACAD_SerialNumber> allLinesOfThisDrawing = data.FindAll(x => x.Drawing_No.Equals(a));
                foreach (string b in allLinesOfThisDrawing.GetSequenzes_<List<ACAD_SerialNumber>>()) //gibt jede Sequenz die enthalten ist einmal in die Liste
                {
                    List<ACAD_SerialNumber> allLinesOfThisDrawingAndSequenz = allLinesOfThisDrawing.FindAll(x => x.SO_Position.Equals(b));
                    Result.Add(allLinesOfThisDrawingAndSequenz);
                }

            }
            return Result;
        }
        public static List<List<ACAD_SerialNumber>> SeperateByPO_Number<T>(this List<ACAD_SerialNumber> data)
        {
            List<List<ACAD_SerialNumber>> Result = new List<List<ACAD_SerialNumber>>();

            foreach (string a in data.GetPO_Numbers_<List<ACAD_SerialNumber>>()) //gibt jede Referenz die enthalten ist einmal in die Liste
            {
                List<ACAD_SerialNumber> allLinesOfThisDrawing = data.FindAll(x => x.PO_Number.Equals(a));
                Result.Add(allLinesOfThisDrawing);
            }
            return Result;
        }
    }
}
