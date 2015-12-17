using HSInfo.IPCM;
using System;
using System.IO;

namespace PipeTest {
    class Program {
        public class Foo : Message {
            public Foo() : base(ID) { }
            public Foo(string s) : base(ID) { this.s = s; }
            public override void Deserialize(BinaryReader r) {
                s = r.ReadString();
            }
            public override void Serialize(BinaryWriter w) {
                w.Write(s);
            }
            public const int ID = 12;
            public string s;
        }

        public class Bar : Message {
            public Bar() : base(ID) { }
            public Bar(int x) : base(ID) { this.x = x; }
            public override void Deserialize(BinaryReader r) {
                x = r.ReadInt32();
            }
            public override void Serialize(BinaryWriter w) {
                w.Write(x);
            }
            public const int ID = 63;
            public int x;
        }

        class MyListener : Listener {
            public override void OnMessageRecv(Message m) {
                switch (m.GetID()) {
                    case Foo.ID:
                        Console.WriteLine(((Foo)m).s);
                        break;
                    case Bar.ID:
                        Console.WriteLine(((Bar)m).x);
                        break;
                }
            }
        }

        static Message MyMessageHandler(int id) {
            switch (id) {
                case Foo.ID:
                    return new Foo();
                case Bar.ID:
                    return new Bar();
            }
            return null;
        }

        static void Main(string[] args) {
            var s = ServerStream.Create("Test", 4096);
            
            var c = ClientStream.Create("Test");
            c.RegisterListener(new MyListener());
            
            s.Write(new Foo("Hello"));

            c.CheckForMessages(MyMessageHandler);
            c.Distribute();

            Console.Read();
        }
    }
}
