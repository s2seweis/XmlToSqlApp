using System.Collections.Generic;
using System.IO;
using Xunit;
using XmlToSqlApp;

namespace XmlToSqlApp.Tests
{
    public class XmlProcessorTests
    {
        private const string TestXmlPath = "XmlToDb/sample_test.xml"; // Adjust the path accordingly

        [Fact]
        public void ReadXml_ValidXml_ReturnsListOfItems()
        {
            // Arrange
            var processor = new XmlProcessor();
            // Ensure the test XML file exists
            File.WriteAllText(TestXmlPath, @"<?xml version='1.0' encoding='utf-8' ?>
                                                <Items>
                                                    <Item>
                                                        <Name>Item1</Name>
                                                        <Value>Value1</Value>
                                                    </Item>
                                                    <Item>
                                                        <Name>Item2</Name>
                                                        <Value>Value2</Value>
                                                    </Item>
                                                </Items>");

            // Act
            var result = processor.ReadXml(TestXmlPath);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Item>>(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Item1", result[0].Name);
            Assert.Equal("Value1", result[0].Value);
            Assert.Equal("Item2", result[1].Name);
            Assert.Equal("Value2", result[1].Value);
        }

        // Clean up after tests
        public void Dispose()
        {
            if (File.Exists(TestXmlPath))
            {
                File.Delete(TestXmlPath);
            }
        }
    }
}
