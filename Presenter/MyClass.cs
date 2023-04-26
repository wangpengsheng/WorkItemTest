using System.Net.Sockets;
using AsynchronousExecutor;
using NModbus;

namespace Presenter
{
    public class MyClass : IAsyncTask<string>
    {
        private readonly ConfiguredPara _paras;
        private readonly IModbusMaster _master;

        public MyClass(ConfiguredPara paras)
        {
            _paras = paras;
            _master = new ModbusFactory().CreateMaster(new TcpClient($"{_paras.IpAddress}", _paras.Port));
            Name = _paras.Name;
        }

        public string Name { get; set; }

        public string Result { get; set; }

        public Task<string> Execute()
        {
            var uShortResultAsync = _master.ReadHoldingRegistersAsync(Convert.ToByte( _paras.SlaveAddress), _paras.StartAddress, _paras.NumberOfPoints);
            Result = string.Join(',', uShortResultAsync.Result.Select(x => x.ToString()));
            return Task.FromResult(Result);
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public ExecutionState GetState()
        {
            throw new NotImplementedException();
        }
    }

    public class ConfiguredPara
    {
        public string Name { get; set; }
        public ushort SlaveAddress { get; set; }
        public ushort StartAddress { get; set; }
        public ushort NumberOfPoints { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
    }
}