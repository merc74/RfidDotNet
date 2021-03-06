using System;
using FluentAssertions;
using maxbl4.RfidDotNet.AlienTech;
using maxbl4.RfidDotNet.AlienTech.Extensions;
using maxbl4.RfidDotNet.GenericSerial;
using maxbl4.RfidDotNet.GenericSerial.Ext;
using Xunit;

namespace maxbl4.RfidDotNet.Tests
{
    public class UnifiedTagStreamFactoryTests
    {
        [Fact]
        public void Should_throw_if_not_registered()
        {
            var factory = new UniversalTagStreamFactory();
            Assert.Throws<ArgumentOutOfRangeException>(() => factory.CreateStream("protocol=alien"));
            Assert.Throws<ArgumentOutOfRangeException>(() => factory.CreateStream("protocol=serial"));
        }
        
        [Fact]
        public void Should_create_instance_from_connection_string()
        {
            var factory = new UniversalTagStreamFactory();
            factory.UseAlienProtocol();
            var stream = factory.CreateStream("protocol=alien; network=localhost");
            stream.Should().BeOfType<ReconnectingAlienReaderProtocol>();
            
            factory = new UniversalTagStreamFactory();
            factory.UseSerialProtocol();
            stream = factory.CreateStream("protocol=serial; serial=COM4");
            stream.Should().BeOfType<SerialUnifiedTagStream>();
        }
    }
}