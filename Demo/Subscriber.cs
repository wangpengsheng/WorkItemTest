namespace Demo
{
    public abstract class Subscriber
    {
        public Subscriber(Modbus.Device.IModbusMaster master)
        {
            _master = master;
        }
        public readonly Modbus.Device.IModbusMaster _master;
        public ushort Addresses { get; set; }

        public abstract void OnDataChanged(ushort value);
    }
    public class AddressSubscriber : Subscriber
    {
        public AddressSubscriber(ushort address,Modbus.Device.IModbusMaster master):base(master)
        {
            Addresses = address;
        }

        public override void OnDataChanged(ushort value)
        {
            _master?.WriteSingleRegister(1,Convert.ToUInt16(Addresses + 10),value);
        }
    }

   public abstract class ValueDecorator : Subscriber
        {
            protected Subscriber sub;

            protected ValueDecorator(Subscriber subscriber,Modbus.Device.IModbusMaster master):base(master)
            {
                Addresses = subscriber.Addresses;
                sub = subscriber;
            }
            public abstract bool Decorate();

            public abstract void After();
        }
    public class V1 : ValueDecorator
    {
        private ushort CurrentValue { get; set; }
        private ushort OldValue { get; set; }

        public V1(Subscriber subscriber,Modbus.Device.IModbusMaster master) : base(subscriber,master)
        {
        }

        public override void OnDataChanged(ushort value)
        {
            CurrentValue = value;
            if (Decorate())
            {
                Console.WriteLine($"监视地址:[{Addresses}]值改变.Current:[{this.CurrentValue}]OldValue:[{this.OldValue}]");
                sub.OnDataChanged(CurrentValue);
                OldValue = CurrentValue;
            }
            else
            {
                // Console.WriteLine($"监视地址:[{Addresses}]值相同.Current:[{this.CurrentValue}]OldValue:[{this.OldValue}]");
            }

            After();
        }

        public override bool Decorate()
        {
            return CurrentValue != OldValue;
        }

        public override void After()
        {
            Thread.Sleep(TimeSpan.FromSeconds(0.1));
        }
    }
}