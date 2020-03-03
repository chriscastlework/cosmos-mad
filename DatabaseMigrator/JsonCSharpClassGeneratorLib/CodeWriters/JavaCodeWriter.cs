namespace DatabaseMigrator.JsonCSharpClassGeneratorLib.CodeWriters
{
    using System;
    using System.IO;
    using System.Text;

    public class JavaCodeWriter : ICodeWriter
    {
        public string FileExtension
        {
            get { return ".java"; }
        }

        public string DisplayName
        {
            get { return "Java"; }
        }

        public string GetTypeName(JsonType type, IJsonClassGeneratorConfig config)
        {
            throw new NotImplementedException();
        }

        public void WriteClass(IJsonClassGeneratorConfig config, StringBuilder sw, JsonType type)
        {
            throw new NotImplementedException();
        }

        public void WriteFileStart(IJsonClassGeneratorConfig config, StringBuilder sw)
        {
            foreach (var line in JsonClassGenerator.FileHeader)
            {
                sw.AppendLine("// " + line);
            }
        }

        public void WriteFileEnd(IJsonClassGeneratorConfig config, StringBuilder sw)
        {
            throw new NotImplementedException();
        }

        public void WriteNamespaceStart(IJsonClassGeneratorConfig config, StringBuilder sw, bool root)
        {
            throw new NotImplementedException();
        }

        public void WriteNamespaceEnd(IJsonClassGeneratorConfig config, StringBuilder sw, bool root)
        {
            throw new NotImplementedException();
        }
    }
}
