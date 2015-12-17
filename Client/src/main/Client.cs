
/*
 * File: Tracker.cs
 * Notes:
 */

using HSInfo.IPCM;
using System;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace HSInfo {
    namespace Client {
        public class Client {
            /* --- Static Methods (Auxiliary) --- */
            private static int Inject() {
                int err;
                err = Injector.Inject("hearthstone.exe", "HSInfoServer.dll", "HSInfo.Server", "Hook", "Load");
                return err;                    
            }
            private static void Loop() {
                for (;;) {
                    s_client.CheckForMessages(MyMessageHandler);
                    s_client.Write(new Bar(42));
                    System.Threading.Thread.Sleep(2000);
                }
            }
            private static void Close() {
                s_client.Close();
            }
            class MyListener : IPCM.Listener {
                public override void OnMessageRecv(IPCM.Message m) {
                    switch (m.GetID()) {
                        case Bar.ID:
                            Console.WriteLine(((Bar)m).x);
                            break;
                    }
                }
            }
            static IPCM.Message MyMessageHandler(int id) {
                switch (id) {
                    case Bar.ID:
                        return new Bar();
                }
                return null;
            }
            public class Bar : IPCM.Message {
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
            /* --- Static Methods (Interface) --- */
            public static void Main(string[] args) {
                if (Inject() != 0) {
                    Console.WriteLine("Injection Failed...");
                    return;
                }

                System.Threading.Thread.Sleep(500);

                s_client = ClientStream.Create("foobar");
                s_client.RegisterListener(new MyListener());

                Loop();

                Close();

                Console.Read();
            }
            /* --- Static Fields --- */
            static ClientStream s_client;
            /* --- Const Fields --- */
            const long s_mapSize = 4096;
            const string s_pipeName = "\\\\.\\pipe\\mynamedpipe";
        }
    }
}