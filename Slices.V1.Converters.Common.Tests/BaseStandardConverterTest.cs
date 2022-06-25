using Moq;
using Slices.TestsSupport;
using Slices.V1.Converters.Common.Exceptions;
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
    public async Task FromExternalPassthrough()
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

        // Since the serializer is stubbed, the stream should never be actually used.
        // MockBehavior.Strict will let us know if that's not the case.
        Stream stream = new Mock<Stream>(MockBehavior.Strict).Object;
        
        DummyModel externalModel = new();
        SfdoResource sfdo = new();

        serializerMock.Setup(serializer => serializer.FromXmlAsync(stream)).Returns(Task.FromResult(externalModel));
        importerMock.Setup(importer => importer.FromExternal(externalModel)).Returns(sfdo);

        converterMock.SetupGet(converter => converter.SupportedFormats).CallBase();
        converterMock.SetupGet(converter => converter.ExternalStandard).Returns("test");
        
        converterMock.Setup(converter => converter.FromSerializedExternalAsync(stream, null)).CallBase();
        converterMock.Setup(converter => converter.FromSerializedExternalAsync(stream, "xml")).CallBase();
        converterMock.Setup(converter => converter.FromSerializedExternalAsync(stream, "invalid")).CallBase();

        Assert.Same(sfdo, await converterMock.Object.FromSerializedExternalAsync(stream, null));
        Assert.Same(sfdo, await converterMock.Object.FromSerializedExternalAsync(stream, "xml"));

        UnsupportedExternalFormatException exception = await Assert.ThrowsAsync<UnsupportedExternalFormatException>(
            () => converterMock.Object.FromSerializedExternalAsync(stream, "invalid")
        );
        
        Assert.Equal("test", exception.ExternalStandard);
        Assert.Equal("invalid", exception.Format);
    }

    [Fact]
    public async Task ToExternalPassthrough()
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

        MemoryStream stream = new();
        DummyModel externalModel = new();
        SfdoResource sfdo = new();

        exporterMock.Setup(exporter => exporter.ToExternal(sfdo)).Returns(externalModel);
        serializerMock.Setup(serializer => serializer.ToXmlAsync(externalModel, stream))
            .Returns<DummyModel, Stream>((r, s) =>
            {
                byte[] content = { 0x00 };
                return Task.Run(() => s.WriteAsync(content, 0, 1));
            });

        converterMock.SetupGet(converter => converter.SupportedFormats).CallBase();
        converterMock.SetupGet(converter => converter.ExternalStandard).Returns("test");

        converterMock.Setup(converter => converter.ToSerializedExternalAsync(sfdo, null, stream)).CallBase();
        converterMock.Setup(converter => converter.ToSerializedExternalAsync(sfdo, "xml", stream)).CallBase();
        converterMock.Setup(converter => converter.ToSerializedExternalAsync(sfdo, "invalid", stream)).CallBase();

        Assert.Same(externalModel, converterMock.Object.ToExternal(sfdo));

        await converterMock.Object.ToSerializedExternalAsync(sfdo, null, stream);
        Assert.Equal(1, stream.Length);
        stream.Clear();

        await converterMock.Object.ToSerializedExternalAsync(sfdo, "xml", stream);
        Assert.Equal(1, stream.Length);
        stream.Clear();

        UnsupportedExternalFormatException exception = await Assert.ThrowsAsync<UnsupportedExternalFormatException>(
            () => converterMock.Object.ToSerializedExternalAsync(sfdo, "invalid", stream)
        );
        
        Assert.Equal("test", exception.ExternalStandard);
        Assert.Equal("invalid", exception.Format);
    }
}