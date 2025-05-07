
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;

namespace TC_JsonConverter
{
    public class TeamcenterJsonConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jToken = JToken.ReadFrom(reader);
            JObject jObject = jToken.ToObject<JObject>();
            ObjectType type;
            if (jObject.ContainsKey("objectType") && jToken["objectType"] != null && jToken["objectType"].ToString() != "")
            {
                type = jToken["objectType"].ToObject<ObjectType>();
            }
            else if (jObject.ContainsKey("familyID"))
            {
                type = ObjectType.VariantRule;
                Console.WriteLine(jObject["familyName"]);
                Console.WriteLine(jObject["familyID"]);
                Console.WriteLine(jObject["featureName"]);
                Console.WriteLine(jObject["featureID"]);
            }
            else if (jToken["objectType"] != null && jToken["objectType"].ToString() == "")
            {
                type = ObjectType.Unknown;
                _ = jObject.Remove("objectType");
                jObject.Add("objectType", "Unknown");
            }
            else
            {
                type = ObjectType.Unknown;
                jObject.Add("objectType", "Unknown");
            }
            TcObject result;
            switch (type)
            {
                case ObjectType.CT4MB:
                    result = new CT4MB();
                    break;
                case ObjectType.CT4Cabin:
                    result = new CT4Cabin();
                    break;
                case ObjectType.CT4_Structure:
                    result = new CT4_Structure();
                    break;
                case ObjectType.CT4Modul:
                    result = new CT4Modul();
                    break;
                case ObjectType.Part:
                    result = new Part();
                    break;
                case ObjectType.VariantRule:
                    result = new VariantRule();
                    break;
                default:
                    result = new Unknown();
                    break;
            }

            serializer.Populate(jObject.CreateReader(), result);

            if (result.Children != null)
            {
                foreach (TcObject child in result.Children)
                {
                    child.Parent = result;
                }
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // We cannot directly serialize "value" here, as that would call our own converter once more
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            // Not needed, as we register our converter directly
            throw new NotImplementedException();
        }
    }

    public enum ObjectType
    {
        Unknown,
        CT4MB,
        CT4Cabin,
        Part,
        CT4Modul,
        CT4_Structure,
        VariantRule
    }

    [JsonConverter(typeof(TeamcenterJsonConverter))]
    public abstract class TcObject : IEquatable<TcObject>, IComparable<TcObject>
    {
        public TcObject Parent { get; set; }

        [JsonProperty(PropertyName = "acadHandle")]
        public string AcadHandle { get; set; }

        [JsonProperty(PropertyName = "objectType")]
        public ObjectType Type { get; set; }

        [JsonProperty(PropertyName = "findNo")]
        public string FindNo { get; set; }

        [JsonProperty(PropertyName = "objectName")]
        public string ObjectName { get; set; }

        [JsonProperty(PropertyName = "coordinates")]
        public string Coordinates { get; set; }

        [JsonProperty(PropertyName = "objectDescription")]
        public string ObjectDescription { get; set; }

        [JsonProperty(PropertyName = "genericObjectID")]
        public string GenericObjectID { get; set; }

        [JsonProperty(PropertyName = "objectID")]
        public string ObjectID { get; set; }

        [JsonProperty(PropertyName = "revisionID")]
        public string RevisionID { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public string Quantity { get; set; }

        [JsonProperty(PropertyName = "children")]
        public List<TcObject> Children { get; set; }

        /*
        public override bool Equals(object obj)
        {
            return obj is TcObject item && ObjectID.Equals(item.ObjectID);
        }
        */

        public static implicit operator TcObject(VariantRule v)
        {
            throw new NotImplementedException();
        }
        public TcObject Find(Func<TcObject, bool> myFunc)
        {
            foreach (TcObject node in Children)
            {
                if (myFunc(node))
                {
                    return node;
                }
                else
                {
                    TcObject test = node.Find(myFunc);
                    if (test != null)
                    {
                        return test;
                    }
                }
            }

            return null;
        }
        public List<TcObject> FindAll(Func<TcObject, bool> myFunc)
        {
            List<TcObject> result = new List<TcObject>();
            if (Children != null && Children.Any())
            {
                foreach (TcObject node in Children)
                {
                    if (myFunc(node))
                    {
                        result.Add(node);
                    }
                    else
                    {
                        List<TcObject> subResult = node.FindAll(myFunc);
                        if (subResult != null && subResult.Any())
                        {
                            result.AddRange(subResult);
                        }
                    }
                }
            }
            return result != null && result.Any() ? result : null;
        }

        public bool Equals(TcObject other)
        {
            return other.GetHashCode() == GetHashCode();
        }

        public int CompareTo(TcObject other)
        {
            throw new NotImplementedException();
        }
    }
    public static class TcObjectExtension
    {
        public static List<TcObject> FindAll(this List<TcObject> data, Func<TcObject, bool> myFunc)
        {
            List<TcObject> result = new List<TcObject>();
            foreach (TcObject item in data)
            {
                if (myFunc(item))
                {
                    result.Add(item);
                }
            }
            return result != null && result.Any() ? result : null;
        }
        private static List<string> GetObjectIds<T>(this List<TcObject> data)
        {
            List<string> singled = new List<string>();
            foreach (TcObject item in data)
            {
                if (!singled.Exists(x => x.Equals(item.ObjectID)))
                {
                    singled.Add(item.ObjectID);
                    //Console.WriteLine(item.AnlagenReferenz + " Added.");
                }
            }
            return singled;
        }
        public static List<List<TcObject>> Seperate<T>(this List<TcObject> data)
        {
            List<List<TcObject>> Result = new List<List<TcObject>>();

            foreach (string a in data.GetObjectIds<List<TcObject>>()) //gibt jede Referenz die enthalten ist einmal in die Liste
            {
                List<TcObject> AllWithThisId = data.FindAll(x => x.ObjectID.Equals(a));
                Result.Add(AllWithThisId);

            }
            return Result;
        }

    }

    public class CT4MB : TcObject
    {
        public CT4MB()
        {
            Type = ObjectType.CT4MB;
        }

        [JsonProperty(PropertyName = "headOfDesign")]
        public string HeadOfDesign { get; set; }

        [JsonProperty(PropertyName = "designer")]
        public string Designer { get; set; }

        [JsonProperty(PropertyName = "dateDesigned")]
        public string DateDesigned { get; set; }

        [JsonProperty(PropertyName = "designNo")]
        public string DesignNo { get; set; }

        [JsonProperty(PropertyName = "dateModified")]
        public string DateModified { get; set; }

        [JsonProperty(PropertyName = "workflow")]
        public string Workflow { get; set; }
        
        public override bool Equals(object obj)
        {
            //List<TcObject> other = ((TcObject)obj).Children;
            //return this.Children.All(x => other.Contains(x)) && other.All(y => this.Children.Contains(y));
            return obj.GetHashCode() == GetHashCode();
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                foreach (var foo in this.Children)
                {
                    hash = hash * 31 + foo.GetHashCode();
                }
                return hash;
            }
        }

    }

    public class CT4Cabin : TcObject
    {
        public CT4Cabin()
        {
            Type = ObjectType.CT4Cabin;
        }

        [JsonProperty(PropertyName = "positionDesignator")]
        public string PositionDesignator { get; set; }

        [JsonProperty(PropertyName = "pseudoSerialNumber")]
        public string PseudoSerialNumber { get; set; }

        [JsonProperty(PropertyName = "rotation")]
        public string Rotation { get; set; }

        [JsonProperty(PropertyName = "objectGroupID")]
        public string ObjectGroupID { get; set; }

        public override bool Equals(object obj)
        {
            //List<TcObject> other = ((TcObject)obj).Children;
            //return this.Children.All(x => other.Contains(x)) && other.All(y => this.Children.Contains(y));
            return obj.GetHashCode() == GetHashCode();
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 20;
                foreach (var foo in this.Children)
                {
                    hash = hash * 31 + foo.GetHashCode();
                }
                return hash;
            }
        }

    }
    public class CT4_Structure : TcObject
    {
        public CT4_Structure()
        {
            Type = ObjectType.CT4_Structure;
        }

        [JsonProperty(PropertyName = "positionDesignator")]
        public string PositionDesignator { get; set; }

        [JsonProperty(PropertyName = "rotation")]
        public string Rotation { get; set; }

        [JsonProperty(PropertyName = "objectGroupID")]
        public string ObjectGroupID { get; set; }

        public override bool Equals(object obj)
        {
            //List<TcObject> other = ((TcObject)obj).Children;
            //return this.Children.All(x => other.Contains(x)) && other.All(y => this.Children.Contains(y));
            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 21;
                foreach (var foo in this.Children)
                {
                    hash = hash * 31 + foo.GetHashCode();
                }
                return hash;
            }
        }

    }
    public class CT4Modul : TcObject
    {
        public CT4Modul()
        {
            Type = ObjectType.CT4Modul;
        }

        [JsonProperty(PropertyName = "positionDesignator")]
        public string PositionDesignator { get; set; }

        [JsonProperty(PropertyName = "rotation")]
        public string Rotation { get; set; }

        [JsonProperty(PropertyName = "variantRules")]
        public VariantRules VariantRules { get; set; }

        public string Group_Identificator { get; set; }

        [JsonProperty(PropertyName = "solutionVariantCategory")]
        public string SolutionVariantCategory { get; set; }

        public override bool Equals(object obj)
        {
            //return obj is CT4Modul item && GenericObjectID.Equals(item.GenericObjectID) && VariantRules.Equals(item.VariantRules);
            return obj.GetHashCode() == GetHashCode();
        }
        public override int GetHashCode()
        {
            return GenericObjectID.GetHashCode() ^
                VariantRules.GetHashCode();
        }

    }
    public class Part : TcObject
    {
        public Part()
        {
            Type = ObjectType.Part;
        }

        [JsonProperty(PropertyName = "positionDesignator")]
        public string PositionDesignator { get; set; }

        [JsonProperty(PropertyName = "rotation")]
        public string Rotation { get; set; }

        public override bool Equals(object obj)
        {
            //return obj is Part item && ObjectID.Equals(item.ObjectID);
            return obj.GetHashCode() == GetHashCode();
        }
        public override int GetHashCode()
        {
            return ObjectID.GetHashCode();
        }
    }

    public class Unknown : TcObject
    {
        public Unknown()
        {
            Type = ObjectType.Unknown;
        }

        [JsonProperty(PropertyName = "positionDesignator")]
        public string PositionDesignator { get; set; }

        [JsonProperty(PropertyName = "rotation")]
        public string Rotation { get; set; }

        [JsonProperty(PropertyName = "variantRules")]
        public List<VariantRule> VariantRules { get; set; }

        public string Group_Identificator { get; set; }

        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == GetHashCode();
        }
        

    }

    public class VariantRule
    {
        [JsonProperty(PropertyName = "variantRules")]
        public ObjectType Type { get; set; }

        public VariantRule()
        {
            Type = ObjectType.VariantRule;
        }
        [JsonProperty(PropertyName = "familyName")]
        public string FamilyName { get; set; }

        [JsonProperty(PropertyName = "familyID")]
        public string FamilyID { get; set; }

        [JsonProperty(PropertyName = "featureName")]
        public string FeatureName { get; set; }

        [JsonProperty(PropertyName = "featureID")]
        public string FeatureID { get; set; }

        public override bool Equals(object obj)
        {
            //return obj is VariantRule item && FamilyID.Equals(item.FamilyID) && FeatureID.Equals(item.FeatureID);
            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return FamilyID.GetHashCode() ^
                FeatureID.GetHashCode();
        }
        public override string ToString()
        {
            return FamilyName + "(" + FamilyID + ")" + " : " + FeatureName + "(" + FeatureID + ")";
        }
    }
    public class VariantRules : List<VariantRule>
    {
        public override bool Equals(object obj)
        {
            //VariantRules other = (VariantRules)obj;
            //return this.All(x => other.Contains(x)) && other.All(y => Contains(y));
            return obj.GetHashCode() == GetHashCode();
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 18;
                foreach (VariantRule foo in this)
                {
                    hash = (hash * 31) + foo.GetHashCode();
                }
                return hash;
            }
        }
        public override string ToString()
        {
            string result = "";
            foreach (var item in this)
            {
                result = result + item.ToString() + ", ";
            }
            return result;
        }

    }
    
    
    

}
