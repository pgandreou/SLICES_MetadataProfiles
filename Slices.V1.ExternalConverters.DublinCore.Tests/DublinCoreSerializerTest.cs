using Slices.TestsSupport;

namespace Slices.V1.ExternalConverters.DublinCore.Tests;

public class DublinCoreSerializerTest
{
    [Fact]
    public void Test1()
    {
        DublinCoreSerializer serializer = new();

        string Serialized = @"<?xml version='1.0' encoding='utf-8'?>
<oai_dc:dc xmlns:dc=""http://purl.org/dc/elements/1.1/"" xmlns:oai_dc=""http://www.openarchives.org/OAI/2.0/oai_dc/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.openarchives.org/OAI/2.0/oai_dc/ http://www.openarchives.org/OAI/2.0/oai_dc.xsd"">
  <dc:creator>Aleksandr Popov</dc:creator>
  <dc:creator>ivan-immunomind</dc:creator>
  <dc:creator>MVolobueva</dc:creator>
  <dc:creator>Vadim I. Nazarov</dc:creator>
  <dc:creator>immunarch.bot</dc:creator>
  <dc:creator>Eugene Rumynskiy</dc:creator>
  <dc:creator>gracecodeadventures</dc:creator>
  <dc:creator>tsvvas</dc:creator>
  <dc:creator>Maksym Zarodniuk</dc:creator>
  <dc:date>2022-05-31</dc:date>
  <dc:description>10x format in repLoad(): added C genes support
Support for vdjtools format with ""freq"" header instead of ""frequency""
Bugfix for filter: exclude must not remove everything when string is not matching
Germline formulas fixes
Fixed ""missing value"" error in repDiversity
Deprecated .format argument removed from repLoad
Various bugfixes in code and documentation</dc:description>
  <dc:identifier>https://zenodo.org/record/6599744</dc:identifier>
  <dc:identifier>10.5281/zenodo.6599744</dc:identifier>
  <dc:identifier>oai:zenodo.org:6599744</dc:identifier>
  <dc:relation>url:https://github.com/immunomind/immunarch/tree/0.6.9</dc:relation>
  <dc:relation>doi:10.5281/zenodo.3367200</dc:relation>
  <dc:relation>url:https://zenodo.org/communities/covid-19</dc:relation>
  <dc:relation>url:https://zenodo.org/communities/zenodo</dc:relation>
  <dc:rights>info:eu-repo/semantics/openAccess</dc:rights>
  <dc:title>immunomind/immunarch: Immunarch 0.6.9</dc:title>
  <dc:type>info:eu-repo/semantics/other</dc:type>
  <dc:type>software</dc:type>
</oai_dc:dc>";

        DublinCoreObject result = serializer.FromXml(Serialized);

        Assert.NotNull(result);
    }

    [Fact]
    public void Test2()
    {
        DublinCoreSerializer serializer = new();
        DublinCoreObject record = new();

        record.creator = new[] { "C_A", "C_B" };
        record.date = DateTime.Now;
        record.description = "abc";
        record.identifier = new[] { "I_A", "I_B" };
        record.rights = "abc2";
        record.title = "abc3";
        record.type = new[] { "T_A", "T_B" };

        var s = serializer.ToXml(record);

        Assert.NotEmpty(s);
    }

    [Fact]
    public void Test3()
    {
        using TextReader textReader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\immunarch-0-6-9.xml");

        DublinCoreSerializer serializer = new();
        DublinCoreObject result = serializer.FromXml(textReader);

        Assert.NotNull(result);
    }
}