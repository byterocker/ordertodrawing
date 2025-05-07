using System;
using System.Collections.Generic;
using System.Text;

namespace OrderToDrawing
{
    public class OrderObject : IEquatable<OrderObject>, IComparable<OrderObject>
    {

        public int OrderObjectId { get; set; }
        public string OrderId { get; set; }
        public string Position { get; set; }
        public string ArtikelCode { get; set; }
        public string AnlagenReferenz { get; set; }
        public string AnlagenSequenz { get; set; }
        public string AcadNumber { get; set; }
        public string Seriennummer { get; set; }


        public OrderObject()
        {
            Console.WriteLine("neues Objekt erstellt!");
        }


        public override string ToString()
        {
            return "ID: " + OrderId + "   Position: " + Position + "   AnlagenReferenz: " + AnlagenReferenz + "   AnlagenSequenz: " + AnlagenSequenz;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            OrderObject objAsOrderObject = obj as OrderObject;
            if (objAsOrderObject == null) return false;
            else return Equals(objAsOrderObject);
        }
        public override int GetHashCode()
        {
            return OrderObjectId;
        }
        public bool Equals(OrderObject other)
        {
            if (other == null) return false;
            return (OrderObjectId.Equals(other.OrderObjectId));
        } // Should also override == and != operators.
        public int CompareTo(OrderObject comparePart)
        {
            // A null value means that this object is greater.
            if (comparePart == null)
                return 1;

            else
                return this.Seriennummer.CompareTo(comparePart.Seriennummer);
        }
    }
    public static class OrderObjectExtension
    {
        //public static List<T> ToDTO<T>(this IEnumerable<BaseModel> models)
        private static List<string> GetReferenz_<T>(this List<OrderObject> data)
        {
            List<string> singled = new List<string>();
            foreach (OrderObject item in data)
            {
                if (!singled.Exists(x => x.Equals(item.AnlagenReferenz)))
                {
                    singled.Add(item.AnlagenReferenz);
                    //Console.WriteLine(item.AnlagenReferenz + " Added.");
                }
            }
            return singled;
        }
        private static List<string> GetSequenzes_<T>(this List<OrderObject> data)
        {
            List<string> singled = new List<string>();
            foreach (OrderObject item in data)
            {
                if (!singled.Exists(x => x.Equals(item.AnlagenSequenz)))
                {
                    singled.Add(item.AnlagenSequenz);
                    //Console.WriteLine(item.AnlagenReferenz + " Added.");
                }
            }
            return singled;
        }
        public static List<List<OrderObject>> Seperate<T>(this List<OrderObject> data)
        {
            List<List<OrderObject>> Result = new List<List<OrderObject>>();

            foreach (string a in data.GetReferenz_<List<OrderObject>>()) //gibt jede Referenz die enthalten ist einmal in die Liste
            {
                List<OrderObject> allLinesOfThisDrawing = data.FindAll(x => x.AnlagenReferenz.Equals(a));
                foreach (string b in allLinesOfThisDrawing.GetSequenzes_<List<OrderObject>>()) //gibt jede Sequenz die enthalten ist einmal in die Liste
                {
                    List<OrderObject> allLinesOfThisDrawingAndSequenz = allLinesOfThisDrawing.FindAll(x => x.AnlagenSequenz.Equals(b));
                    Result.Add(allLinesOfThisDrawingAndSequenz);
                }

            }
            return Result;
        }
    }
}
