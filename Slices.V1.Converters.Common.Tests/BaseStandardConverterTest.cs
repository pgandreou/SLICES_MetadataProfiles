using Moq;
using Slices.V1.Model;

namespace Slices.V1.Converters.Common.Tests;

public class BaseStandardConverterTest
{
    // ReSharper disable once MemberCanBePrivate.Global (mocking fails otherwise)
    public sealed class DummyModel
    {
    }

    [Fact]
    public void FromExternalPassthrough()
    {
        Mock<ISlicesImporter<DummyModel>> importerMock = new(MockBehavior.Strict);
        Mock<ISlicesExporter<DummyModel>> exporterMock = new(MockBehavior.Strict);

        Mock<BaseStandardConverter<DummyModel>> converterMock = new(
            MockBehavior.Strict, importerMock.Object, exporterMock.Object
        )
        {
            CallBase = true
        };

        DummyModel externalModel = new();
        SfdoResource sfdo = new();

        importerMock.Setup(importer => importer.FromExternal(externalModel)).Returns(sfdo);

        Assert.Same(sfdo, converterMock.Object.FromExternal(externalModel));
    }

    [Fact]
    public void ToExternalPassthrough()
    {
        Mock<ISlicesImporter<DummyModel>> importerMock = new(MockBehavior.Strict);
        Mock<ISlicesExporter<DummyModel>> exporterMock = new(MockBehavior.Strict);

        Mock<BaseStandardConverter<DummyModel>> converterMock = new(
            MockBehavior.Strict, importerMock.Object, exporterMock.Object
        )
        {
            CallBase = true
        };

        DummyModel externalModel = new();
        SfdoResource sfdo = new();

        exporterMock.Setup(exporter => exporter.ToExternal(sfdo)).Returns(externalModel);

        Assert.Same(externalModel, converterMock.Object.ToExternal(sfdo));
    }
}

public class BaseXmlStandardConverterTest
{
    // ReSharper disable once MemberCanBePrivate.Global (mocking fails otherwise)
    public sealed class DummyModel
    {
    }

    [Fact]
    public void FromExternalPassthrough()
    {
        Mock<ISlicesImporter<DummyModel>> importerMock = new(MockBehavior.Strict);
        Mock<ISlicesExporter<DummyModel>> exporterMock = new(MockBehavior.Strict);
        Mock<IStandardXmlSerializer<DummyModel>> serializerMock = new(MockBehavior.Strict);

        Mock<BaseXmlStandardConverter<DummyModel>> converterMock = new(
            MockBehavior.Strict, importerMock.Object, exporterMock.Object, serializerMock.Object
        )
        {
            CallBase = true
        };

        StringReader reader = new("text");
        DummyModel externalModel = new();
        SfdoResource sfdo = new();

        serializerMock.Setup(serializer => serializer.FromXml(reader)).Returns(externalModel);
        importerMock.Setup(importer => importer.FromExternal(externalModel)).Returns(sfdo);

        converterMock.Setup(converter => converter.FromSerializedExternal(reader, null)).CallBase();
        converterMock.Setup(converter => converter.FromSerializedExternal(reader, "xml")).CallBase();
        converterMock.Setup(converter => converter.FromSerializedExternal(reader, "invalid")).CallBase();

        Assert.Same(sfdo, converterMock.Object.FromSerializedExternal(reader, null));
        Assert.Same(sfdo, converterMock.Object.FromSerializedExternal(reader, "xml"));

        Assert.Throws<ArgumentOutOfRangeException>(
            () => converterMock.Object.FromSerializedExternal(reader, "invalid")
        );
    }
    
    [Fact]
    public void ToExternalPassthrough()
    {
        Mock<ISlicesImporter<DummyModel>> importerMock = new(MockBehavior.Strict);
        Mock<ISlicesExporter<DummyModel>> exporterMock = new(MockBehavior.Strict);
        Mock<IStandardXmlSerializer<DummyModel>> serializerMock = new(MockBehavior.Strict);

        Mock<BaseXmlStandardConverter<DummyModel>> converterMock = new(
            MockBehavior.Strict, importerMock.Object, exporterMock.Object, serializerMock.Object
        )
        {
            CallBase = true
        };

        DummyModel externalModel = new();
        StringWriter writer = new();
        SfdoResource sfdo = new();

        exporterMock.Setup(exporter => exporter.ToExternal(sfdo)).Returns(externalModel);
        serializerMock.Setup(serializer => serializer.ToXml(externalModel, writer))
            .Callback<DummyModel, TextWriter>((r, w) => w.Write("test"));
        
        converterMock.Setup(converter => converter.ToSerializedExternal(sfdo, null, writer)).CallBase();
        converterMock.Setup(converter => converter.ToSerializedExternal(sfdo, "xml", writer)).CallBase();
        converterMock.Setup(converter => converter.ToSerializedExternal(sfdo, "invalid", writer)).CallBase();
        
        Assert.Same(externalModel, converterMock.Object.ToExternal(sfdo));
        
        converterMock.Object.ToSerializedExternal(sfdo, null, writer);
        Assert.Equal("test", writer.ToString());
        writer.GetStringBuilder().Clear();
        
        converterMock.Object.ToSerializedExternal(sfdo, "xml", writer);
        Assert.Equal("test", writer.ToString());

        Assert.Throws<ArgumentOutOfRangeException>(
            () => converterMock.Object.ToSerializedExternal(sfdo, "invalid", writer)
        );
    }
}