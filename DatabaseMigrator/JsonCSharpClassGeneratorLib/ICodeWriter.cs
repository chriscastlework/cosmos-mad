namespace DatabaseMigrator.JsonCSharpClassGeneratorLib
{
    using System.IO;
    using System.Text;

    public interface ICodeWriter
    {
        string FileExtension { get; }
        string DisplayName { get; }
        string GetTypeName(JsonType type, IJsonClassGeneratorConfig config);
        void WriteClass(IJsonClassGeneratorConfig config, StringBuilder sw, JsonType type);
        void WriteFileStart(IJsonClassGeneratorConfig config, StringBuilder sw);
        void WriteFileEnd(IJsonClassGeneratorConfig config, StringBuilder sw);
        void WriteNamespaceStart(IJsonClassGeneratorConfig config, StringBuilder sw, bool root);
        void WriteNamespaceEnd(IJsonClassGeneratorConfig config, StringBuilder sw, bool root);
    }
}
