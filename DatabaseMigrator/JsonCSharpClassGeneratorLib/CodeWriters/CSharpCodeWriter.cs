namespace DatabaseMigrator.JsonCSharpClassGeneratorLib.CodeWriters
{
    using System;
    using System.IO;
    using System.Text;

    public class CSharpCodeWriter : ICodeWriter
    {
        public string FileExtension
        {
            get { return ".cs"; }
        }

        public string DisplayName
        {
            get { return "C#"; }
        }


        private const string NoRenameAttribute = "[Obfuscation(Feature = \"renaming\", Exclude = true)]";
        private const string NoPruneAttribute = "[Obfuscation(Feature = \"trigger\", Exclude = false)]";

        public string GetTypeName(JsonType type, IJsonClassGeneratorConfig config)
        {
            var arraysAsLists = !config.ExplicitDeserialization;

            switch (type.Type)
            {
                case JsonTypeEnum.Anything: return "object";
                case JsonTypeEnum.Array: return arraysAsLists ? "IList<" + GetTypeName(type.InternalType, config) + ">" : GetTypeName(type.InternalType, config) + "[]";
                case JsonTypeEnum.Dictionary: return "Dictionary<string, " + GetTypeName(type.InternalType, config) + ">";
                case JsonTypeEnum.Boolean: return "bool";
                case JsonTypeEnum.Float: return "double";
                case JsonTypeEnum.Integer: return "int";
                case JsonTypeEnum.Long: return "long";
                case JsonTypeEnum.Date: return "DateTime";
                case JsonTypeEnum.NonConstrained: return "object";
                case JsonTypeEnum.NullableBoolean: return "bool?";
                case JsonTypeEnum.NullableFloat: return "double?";
                case JsonTypeEnum.NullableInteger: return "int?";
                case JsonTypeEnum.NullableLong: return "long?";
                case JsonTypeEnum.NullableDate: return "DateTime?";
                case JsonTypeEnum.NullableSomething: return "object";
                case JsonTypeEnum.Object: return type.AssignedName;
                case JsonTypeEnum.String: return "string";
                default: throw new System.NotSupportedException("Unsupported json type");
            }
        }


        private bool ShouldApplyNoRenamingAttribute(IJsonClassGeneratorConfig config)
        {
            return config.ApplyObfuscationAttributes && !config.ExplicitDeserialization && !config.UsePascalCase;
        }
        private bool ShouldApplyNoPruneAttribute(IJsonClassGeneratorConfig config)
        {
            return config.ApplyObfuscationAttributes && !config.ExplicitDeserialization && config.UseProperties;
        }

        public void WriteFileStart(IJsonClassGeneratorConfig config, StringBuilder sw)
        {
            if (config.UseNamespaces)
            {
                foreach (var line in JsonClassGenerator.FileHeader)
                {
                    sw.AppendLine("// " + line);
                }
                sw.AppendLine();
                sw.AppendLine("using System;");
                sw.AppendLine("using System.Collections.Generic;");
                if (ShouldApplyNoPruneAttribute(config) || ShouldApplyNoRenamingAttribute(config))
                    sw.AppendLine("using System.Reflection;");
                if (!config.ExplicitDeserialization && config.UsePascalCase)
                    sw.AppendLine("using Newtonsoft.Json;");
                sw.AppendLine("using Newtonsoft.Json.Linq;");
                if (config.ExplicitDeserialization)
                    sw.AppendLine("using JsonCSharpClassGenerator;");
                if (config.SecondaryNamespace != null && config.HasSecondaryClasses && !config.UseNestedClasses)
                {
                    sw.AppendLine(String.Format("using {0};", config.SecondaryNamespace));
                }
            }

            if (config.UseNestedClasses)
            {
                sw.AppendLine(String.Format("    {0} class {1}", config.InternalVisibility ? "internal" : "public", config.MainClass));
                sw.AppendLine("    {");
            }
        }

        public void WriteFileEnd(IJsonClassGeneratorConfig config, StringBuilder sw)
        {
            if (config.UseNestedClasses)
            {
                sw.AppendLine("    }");
            }
        }


        public void WriteNamespaceStart(IJsonClassGeneratorConfig config, StringBuilder sw, bool root)
        {
            sw.AppendLine();
            sw.AppendLine(String.Format("namespace {0}", root && !config.UseNestedClasses ? config.Namespace : (config.SecondaryNamespace ?? config.Namespace)));
            sw.AppendLine("{");
            sw.AppendLine();
        }

        public void WriteNamespaceEnd(IJsonClassGeneratorConfig config, StringBuilder sw, bool root)
        {
            sw.AppendLine("}");
        }

        public void WriteClass(IJsonClassGeneratorConfig config, StringBuilder sw, JsonType type)
        {
            var visibility = config.InternalVisibility ? "internal" : "public";



            if (config.UseNestedClasses)
            {
                if (!type.IsRoot)
                {
                    if (ShouldApplyNoRenamingAttribute(config)) sw.AppendLine("        " + NoRenameAttribute);
                    if (ShouldApplyNoPruneAttribute(config)) sw.AppendLine("        " + NoPruneAttribute);
                    sw.AppendLine(String.Format("        {0} class {1}", visibility, type.AssignedName));
                    sw.AppendLine("        {");
                }
            }
            else
            {
                if (ShouldApplyNoRenamingAttribute(config)) sw.AppendLine("    " + NoRenameAttribute);
                if (ShouldApplyNoPruneAttribute(config)) sw.AppendLine("    " + NoPruneAttribute);
                sw.AppendLine(String.Format("    {0} class {1}", visibility, type.AssignedName));
                sw.AppendLine("    {");
            }

            var prefix = config.UseNestedClasses && !type.IsRoot ? "            " : "        ";


            var shouldSuppressWarning = config.InternalVisibility && !config.UseProperties && !config.ExplicitDeserialization;
            if (shouldSuppressWarning)
            {
                sw.AppendLine("#pragma warning disable 0649");
                if (!config.UsePascalCase) sw.AppendLine();
            }

            if (type.IsRoot && config.ExplicitDeserialization) WriteStringConstructorExplicitDeserialization(config, sw, type, prefix);

            if (config.ExplicitDeserialization)
            {
                if (config.UseProperties) WriteClassWithPropertiesExplicitDeserialization(sw, type, prefix);
                else WriteClassWithFieldsExplicitDeserialization(sw, type, prefix);
            }
            else
            {
                WriteClassMembers(config, sw, type, prefix);
            }

            if (shouldSuppressWarning)
            {
                sw.AppendLine();
                sw.AppendLine("#pragma warning restore 0649");
                sw.AppendLine();
            }


            if (config.UseNestedClasses && !type.IsRoot)
                sw.AppendLine("        }");

            if (!config.UseNestedClasses)
                sw.AppendLine("    }");

            sw.AppendLine();


        }



        private void WriteClassMembers(IJsonClassGeneratorConfig config, StringBuilder sw, JsonType type, string prefix)
        {
            foreach (var field in type.Fields)
            {
                if (config.UsePascalCase || config.ExamplesInDocumentation) sw.AppendLine();

                if (config.ExamplesInDocumentation)
                {
                    sw.AppendLine(prefix + "/// <summary>");
                    sw.AppendLine(prefix + "/// Examples: " + field.GetExamplesText());
                    sw.AppendLine(prefix + "/// </summary>");
                }

                if (config.UsePascalCase)
                {

                    sw.AppendLine(String.Format(prefix + "[JsonProperty(\"{0}\")]", field.JsonMemberName));
                }

                if (config.UseProperties)
                {
                    sw.AppendLine(String.Format(prefix + "public {0} {1} {{ get; set; }}", field.Type.GetTypeName(), field.MemberName));
                }
                else
                {
                    sw.AppendLine(String.Format(prefix + "public {0} {1};", field.Type.GetTypeName(), field.MemberName));
                }
            }

        }







        #region Code for (obsolete) explicit deserialization
        private void WriteClassWithPropertiesExplicitDeserialization(StringBuilder sw, JsonType type, string prefix)
        {

            sw.AppendLine(prefix + "private JObject __jobject;");
            sw.AppendLine(String.Format(prefix + "public {0}(JObject obj)", type.AssignedName));
            sw.AppendLine(prefix + "{");
            sw.AppendLine(prefix + "    this.__jobject = obj;");
            sw.AppendLine(prefix + "}");
            sw.AppendLine();

            foreach (var field in type.Fields)
            {

                string variable = null;
                if (field.Type.MustCache)
                {
                    variable = "_" + char.ToLower(field.MemberName[0]) + field.MemberName.Substring(1);
                    sw.AppendLine(prefix + "[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]");
                    sw.AppendLine(String.Format(prefix + "private {0} {1};", field.Type.GetTypeName(), variable));
                }


                sw.AppendLine(String.Format(prefix + "public {0} {1}", field.Type.GetTypeName(), field.MemberName));
                sw.AppendLine(prefix + "{");
                sw.AppendLine(prefix + "    get");
                sw.AppendLine(prefix + "    {");
                if (field.Type.MustCache)
                {
                    sw.AppendLine(String.Format(prefix + "        if ({0} == null)", variable));
                    sw.AppendLine(String.Format(prefix + "            {0} = {1};", variable, field.GetGenerationCode("__jobject")));
                    sw.AppendLine(String.Format(prefix + "        return {0};", variable));
                }
                else
                {
                    sw.AppendLine(String.Format(prefix + "        return {0};", field.GetGenerationCode("__jobject")));
                }
                sw.AppendLine(prefix + "    }");
                sw.AppendLine(prefix + "}");
                sw.AppendLine();

            }

        }


        private void WriteStringConstructorExplicitDeserialization(IJsonClassGeneratorConfig config, StringBuilder sw, JsonType type, string prefix)
        {
            sw.AppendLine();
            sw.AppendLine(String.Format(prefix + "public {1}(string json)", config.InternalVisibility ? "internal" : "public", type.AssignedName));
            sw.AppendLine(prefix + "    : this(JObject.Parse(json))");
            sw.AppendLine(prefix + "{");
            sw.AppendLine(prefix + "}");
            sw.AppendLine();
        }

        private void WriteClassWithFieldsExplicitDeserialization(StringBuilder sw, JsonType type, string prefix)
        {


            sw.AppendLine(String.Format(prefix + "public {0}(JObject obj)", type.AssignedName));
            sw.AppendLine(prefix + "{");

            foreach (var field in type.Fields)
            {
                sw.AppendLine(String.Format(prefix + "    this.{0} = {1};", field.MemberName, field.GetGenerationCode("obj")));

            }

            sw.AppendLine(prefix + "}");
            sw.AppendLine();

            foreach (var field in type.Fields)
            {
                sw.AppendLine(String.Format(prefix + "public readonly {0} {1};", field.Type.GetTypeName(), field.MemberName));
            }
        }
        #endregion

    }
}
