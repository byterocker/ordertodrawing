
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TC_JsonConverter
{
    public class TeamcenterJsonConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jToken = JToken.ReadFrom(reader);
            JObject jObject = jToken.ToObject<JObject>();
            ObjectType type = ObjectType.Unknown;
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
                jObject.Remove("objectType");
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
        public Childrens Children { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as TcObject;

            if (item == null)
            {
                return false;
            }

            return this.ObjectID.Equals(item.ObjectID);
        }

        public override int GetHashCode()
        {
            return this.AcadHandle.GetHashCode();
        }

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
                        return test;
                }
            }

            return null;
        }
        public Childrens FindAll(Func<TcObject, bool> myFunc)
        {
            Childrens result = new Childrens();
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
                        var subResult = node.FindAll(myFunc);
                        if (subResult != null && subResult.Any())
                            result.AddRange(subResult);
                    }
                }
            }
            if (result != null && result.Any())
            {
                return result;
            }
            return null;
        }

        public bool Equals(TcObject other)
        {
            var item = other as TcObject;
            //MessageBox.Show(this.Type.ToString() + " " + item.Type.ToString());
            if (item == null)
            {
                return false;
            }
            else if (this.Type.Equals(item.Type))
            {
                switch (item.Type)
                {
                    case ObjectType.Unknown:
                        return Unknown.Equals(item, this);
                    case ObjectType.CT4MB:
                        return CT4MB.Equals(item, this);
                    case ObjectType.CT4Cabin:
                        return CT4Cabin.Equals(item, this);
                    case ObjectType.Part:
                        return Part.Equals(item, this);
                    case ObjectType.CT4Modul:
                        return CT4Modul.Equals(item, this);
                    case ObjectType.CT4_Structure:
                        return CT4_Structure.Equals(item, this);
                    default:
                        return false;
                }

            }
            else
                return false;

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
            if (result != null && result.Any())
            {
                return result;
            }
            return null;
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
                var AllWithThisId = data.FindAll(x => x.ObjectID.Equals(a));
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
            return false;
        }
        public override int GetHashCode()
        {
            return this.GetHashCode();
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

        [JsonProperty(PropertyName = "rotation")]
        public string Rotation { get; set; }

        [JsonProperty(PropertyName = "objectGroupID")]
        public string ObjectGroupID { get; set; }

        public override bool Equals(object obj)
        {
            TcObject item = obj as TcObject;

            if (item == null)
            {
                return false;
            }
            else if (item.Children != null && this.Children.Equals(item.Children))
            {
                return true;
            }
            else
                return false;
        }
        public override int GetHashCode()
        {
            return Children.GetHashCode();
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
            CT4_Structure item = obj as CT4_Structure;
            if (item == null)
            {
                return false;
            }
            else if (item.Children != null && this.Children.Equals(item.Children))
            {
                return true;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return Children.GetHashCode();
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

        public string GroupIdentificator { get; set; }

        [JsonProperty(PropertyName = "solutionVariantCategory")]
        public string SolutionVariantCategory { get; set; }

        public override bool Equals(object obj)
        {
            CT4Modul item = obj as CT4Modul;

            if (item == null)
            {
                return false;
            }

            if (this.GenericObjectID.Equals(item.GenericObjectID) && this.VariantRules.Equals(item.VariantRules))
                return true;
            else
                return false;

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
            Part item = obj as Part;

            if (item == null)
            {
                return false;
            }

            if (this.ObjectID.Equals(item.ObjectID))
                return true;
            else
                return false;

        }
        public override int GetHashCode()
        {
            return this.ObjectID.GetHashCode();
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

        public string GroupIdentificator { get; set; }

        public override bool Equals(object obj)
        {
            return false;
        }
        public override int GetHashCode()
        {
            return this.AcadHandle.GetHashCode();
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
            VariantRule item = obj as VariantRule;
            //MessageBox.Show(this.FamilyID + " " + item.FamilyID);

            if (item == null)
            {
                return false;
            }

            if (this.FamilyID.Equals(item.FamilyID) && this.FeatureID.Equals(item.FeatureID))
                return true;
            else
                return false;

        }

        public override int GetHashCode()
        {
            return FamilyID.GetHashCode() ^
                FeatureID.GetHashCode();
        }
    }
    public class VariantRules : List<VariantRule>
    {
        public override bool Equals(object obj)
        {
            VariantRules other = (VariantRules)obj;
            return this.All(x => other.Contains(x)) && other.All(y => this.Contains(y));
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                foreach (var foo in this)
                {
                    hash = hash * 31 + foo.GetHashCode();
                }
                return hash;
            }
        }

    }
    public class Childrens : List<TcObject>
    {

        public override bool Equals(object obj)
        {
            Childrens other = (Childrens)obj;
            return this.All(x => other.Contains(x)) && other.All(y => this.Contains(y));
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                foreach (var foo in this)
                {
                    hash = hash * 31 + foo.GetHashCode();
                }
                return hash;
            }
        }
    }



}
