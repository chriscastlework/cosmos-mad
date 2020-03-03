//namespace DatabaseMigrator.JsonCSharpClassGeneratorLib.CodeWriters
//{
//    using System.IO;
//    using System.Text;

//    public class VisualBasicCodeWriter : ICodeWriter
//    {
//        public string FileExtension
//        {
//            get { return ".vb"; }
//        }

//        public string DisplayName
//        {
//            get { return "Visual Basic .NET"; }
//        }

//        private const string NoRenameAttribute = "<Obfuscation(Feature:=\"renaming\", Exclude:=true)>";
//        private const string NoPruneAttribute = "<Obfuscation(Feature:=\"trigger\", Exclude:=false)>";

//        public string GetTypeName(JsonType type, IJsonClassGeneratorConfig config)
//        {
//            var arraysAsLists = config.ExplicitDeserialization;

//            switch (type.Type)
//            {
//                case JsonTypeEnum.Anything: return "Object";
//                case JsonTypeEnum.Array: return arraysAsLists ? "IList(Of " + GetTypeName(type.InternalType, config) + ")" : GetTypeName(type.InternalType, config) + "()";
//                case JsonTypeEnum.Dictionary: return "Dictionary(Of String, " + GetTypeName(type.InternalType, config) + ")";
//                case JsonTypeEnum.Boolean: return "Boolean";
//                case JsonTypeEnum.Float: return "Double";
//                case JsonTypeEnum.Integer: return "Integer";
//                case JsonTypeEnum.Long: return "Long";
//                case JsonTypeEnum.Date: return "DateTime";
//                case JsonTypeEnum.NonConstrained: return "Object";
//                case JsonTypeEnum.NullableBoolean: return "Boolean?";
//                case JsonTypeEnum.NullableFloat: return "Double?";
//                case JsonTypeEnum.NullableInteger: return "Integer?";
//                case JsonTypeEnum.NullableLong: return "Long?";
//                case JsonTypeEnum.NullableDate: return "DateTime?";
//                case JsonTypeEnum.NullableSomething: return "Object";
//                case JsonTypeEnum.Object: return type.AssignedName;
//                case JsonTypeEnum.String: return "String";
//                default: throw new System.NotSupportedException("Unsupported json type");
//            }
//        }

//        private bool ShouldApplyNoRenamingAttribute(IJsonClassGeneratorConfig config)
//        {
//            return config.ApplyObfuscationAttributes && !config.ExplicitDeserialization && !config.UsePascalCase;
//        }
//        private bool ShouldApplyNoPruneAttribute(IJsonClassGeneratorConfig config)
//        {
//            return config.ApplyObfuscationAttributes && !config.ExplicitDeserialization && config.UseProperties;
//        }

//        public void WriteClass(IJsonClassGeneratorConfig config, StringBuilder sw, JsonType type)
//        {
//            var visibility = config.InternalVisibility ? "Friend" : "Public";

//            if (config.UseNestedClasses)
//            {
//                sw.AppendLine("    {0} Partial Class {1}", visibility, config.MainClass);
//                if (!type.IsRoot)
//                {
//                    if (ShouldApplyNoRenamingAttribute(config)) sw.AppendLine("        " + NoRenameAttribute);
//                    if (ShouldApplyNoPruneAttribute(config)) sw.AppendLine("        " + NoPruneAttribute);
//                    sw.AppendLine("        {0} Class {1}", visibility, type.AssignedName);
//                }
//            }
//            else
//            {
//                if (ShouldApplyNoRenamingAttribute(config)) sw.AppendLine("    " + NoRenameAttribute);
//                if (ShouldApplyNoPruneAttribute(config)) sw.AppendLine("    " + NoPruneAttribute);
//                sw.AppendLine("    {0} Class {1}", visibility, type.AssignedName);
//            }

//            var prefix = config.UseNestedClasses && !type.IsRoot ? "            " : "        ";

//            WriteClassMembers(config, sw, type, prefix);

//            if (config.UseNestedClasses && !type.IsRoot)
//                sw.AppendLine("        End Class");

//            sw.AppendLine("    End Class");
//            sw.AppendLine();

//        }


//        private void WriteClassMembers(IJsonClassGeneratorConfig config, StringBuilder sw, JsonType type, string prefix)
//        {
//            foreach (var field in type.Fields)
//            {
//                if (config.UsePascalCase || config.ExamplesInDocumentation) sw.AppendLine();

//                if (config.ExamplesInDocumentation)
//                {
//                    sw.AppendLine(prefix + "''' <summary>");
//                    sw.AppendLine(prefix + "''' Examples: " + field.GetExamplesText());
//                    sw.AppendLine(prefix + "''' </summary>");
//                }


//                if (config.UsePascalCase)
//                {
//                    sw.AppendLine(prefix + "<JsonProperty(\"{0}\")>", field.JsonMemberName);
//                }

//                if (config.UseProperties)
//                {
//                    sw.AppendLine(prefix + "Public Property {1} As {0}", field.Type.GetTypeName(), field.MemberName);
//                }
//                else
//                {
//                    sw.AppendLine(prefix + "Public {1} As {0}", field.Type.GetTypeName(), field.MemberName);
//                }
//            }

//        }





//        public void WriteFileStart(IJsonClassGeneratorConfig config, StringBuilder sw)
//        {
//            foreach (var line in JsonClassGenerator.FileHeader)
//            {
//                sw.AppendLine("' " + line);
//            }
//            sw.AppendLine();
//            sw.AppendLine("Imports System");
//            sw.AppendLine("Imports System.Collections.Generic");
//            if (ShouldApplyNoRenamingAttribute(config) || ShouldApplyNoPruneAttribute(config))
//                sw.AppendLine("Imports System.Reflection");
//            if (config.UsePascalCase)
//                sw.AppendLine("Imports Newtonsoft.Json");
//            sw.AppendLine("Imports Newtonsoft.Json.Linq");
//            if (config.SecondaryNamespace != null && config.HasSecondaryClasses && !config.UseNestedClasses)
//            {
//                sw.AppendLine("Imports {0}", config.SecondaryNamespace);
//            }
//        }

//        public void WriteFileEnd(IJsonClassGeneratorConfig config, StringBuilder sw)
//        {
//        }


//        public void WriteNamespaceStart(IJsonClassGeneratorConfig config, StringBuilder sw, bool root)
//        {
//            sw.AppendLine();
//            sw.AppendLine("Namespace Global.{0}", root && !config.UseNestedClasses ? config.Namespace : (config.SecondaryNamespace ?? config.Namespace));
//            sw.AppendLine();
//        }

//        public void WriteNamespaceEnd(IJsonClassGeneratorConfig config, StringBuilder sw, bool root)
//        {

//            sw.AppendLine("End Namespace");

//        }
//    }
//}