using System.Net.Sockets;
using Modbus.Device;
using System.Collections.Concurrent;
using System.Diagnostics;
namespace Demo;

public partial class Form1 : Form
{
    private static readonly ConcurrentDictionary<ushort, Subscriber> Subscribers = new();
    private static readonly ConcurrentDictionary<ushort, ushort> Cache = new();

    private ModbusMaster? _master;
    public Form1()
    {
        InitializeComponent();
        Load += Form1_Load;

    }

    private void Form1_Load(object? sender, EventArgs e)
    {
        InitModbus();
        InitSubscribers();
        ThreadPool.QueueUserWorkItem(_ =>
           {
               while (true)
               {
                   var us = _master?.ReadHoldingRegisters(1, 1, 10);
                   if (us != null)
                   {
                       for (ushort j = 0; j < 10; j++)
                       {
                           var address = (ushort)(j + 1);
                           var value = us[j];
                           UpdateCache(address, value);
                           NotifySubscribers(address); // 通知订阅者
                       }
                   }
                   Thread.Sleep(1000);
               }
           });
    }

    private void InitSubscribers()
    {
        // 订阅数据变化
        Subscribe(new List<Subscriber>
            {
                new V1(new AddressSubscriber(3,_master),_master),
                // new V1(new AddressSubscriber(4)),
                new V1(new AddressSubscriber(5,_master),_master),
                // new V1(new AddressSubscriber(6)),
                new V1(new AddressSubscriber(7,_master),_master),
                new V1(new AddressSubscriber(8,_master),_master),
                new V1(new AddressSubscriber(9,_master),_master),
            });
    }

    private static void UpdateCache(ushort address, ushort value)
    {
        Cache[address] = value;
    }


    private bool InitModbus()
    {
        var tcpClient = new TcpClient("169.254.240.150", 502);
        _master = ModbusIpMaster.CreateIp(tcpClient);
        return tcpClient.Connected;
    }

    private static ushort GetCacheValue(ushort address)
    {
        return (ushort)(Cache.TryGetValue(address, out var value) ? value : 0);
    }
    private static void NotifySubscribers(ushort address)
    {
        if (Subscribers.TryGetValue(address, out var value))
        {
            var subscriber = value;
            var cacheV = GetCacheValue(address);
            ThreadPool.QueueUserWorkItem(_ =>
             {
                 lock (subscriber)
                 {
                     var st = new Stopwatch();
                     st.Start();
                     subscriber.OnDataChanged(cacheV);
                     st.Stop();
                     Console.WriteLine($"当前线程ID:[{Environment.CurrentManagedThreadId}] 线程耗时:[{st.ElapsedMilliseconds}]");
                 }
             });
        }
        else
        {
            // Console.WriteLine($"没有包含当前地址:{address}");
        }
    }
    private static void Subscribe(IEnumerable<Subscriber> subscribers)
    {
        foreach (var subscriber in subscribers)
        {
            if (!Subscribers.ContainsKey(subscriber.Addresses))
            {
                Subscribers.TryAdd(subscriber.Addresses, subscriber);
            }
        }
    }

    private void kryptonButton1_Click(object sender, EventArgs e)
    {

    }
}
