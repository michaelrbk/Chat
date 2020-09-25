using Chat.CommandInterpreter.Services;
using Chat.Models;
using Microsoft.Extensions.Options;
using Xunit;
using Xunit.Abstractions;

namespace Chat.Test
{
    public class CommandInterpreterTests
    {
        private static ITestOutputHelper _output;
        private static Settings _settings;
        private static IOptions<Settings> _options;
        private static CommandInterpreterService _commandInterpreter;
        public CommandInterpreterTests(ITestOutputHelper output)
        {
            _output = output;
            //Set URL of the WS

            _settings = new Settings() { QueueUrl = "localhost", StockWebServiceUrl = "https://stooq.com/q/l/?s={stock_code}&f=sd2t2ohlcv&h&e=csv" };
            _options = Options.Create(_settings);
            _commandInterpreter = new CommandInterpreterService(_options);
        }

        [Fact]
        public void StockCommandTest()
        {
            string command = "/stock= aapl.us";
            _output.WriteLine("command:" + command);

            _commandInterpreter.InterpretCommand(command);
            Assert.True(true);
        }

        [Fact]
        public void InvalidCommandTest()
        {
            string command = "/sxxx= aapl.us";
            _output.WriteLine("command:" + command);

            _commandInterpreter.InterpretCommand(command);
            Assert.True(true);
        }
    }
}
