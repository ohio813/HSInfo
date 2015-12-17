using System;
using System.Collections.Generic;

using System.Windows.Forms;

namespace FormTest {
    public partial class Form1 : Form {
        
        class MyListener : HSInfo.MessageListener {
            public override void OnMessageRecv(HSInfo.Message m) {
                //DialogManager.Get().ShowMessageOfTheDay("Message Recv (" + m.GetID() + ")");
                switch (m.GetID()) {
                    case HSInfo.MessageTypes.DebugMessage.ID:
                        Console.WriteLine(((HSInfo.MessageTypes.DebugMessage)m).m_msg);
                        break;
                    case HSInfo.MessageTypes.CurrentScene.ID:
                        Console.WriteLine("Current Scene: " + HSInfo.MessageTypes.CurrentScene.IDToName(((HSInfo.MessageTypes.CurrentScene)m).m_id));
                        break;
                }
            }
        }

        public void T() {
            for (;;) {

                m_client.CheckForMessages(HSInfo.MessageHandler.Alloc);
                m_client.Distribute();

                System.Threading.Thread.Sleep(50);

            }
        }

        public Form1() {
            InitializeComponent();
            init();

            m_thread = new System.Threading.Thread(T);
            m_thread.Start();
        }

        public void init() {
            Client.Injector.Inject("hearthstone.exe", "HSInfoServer.dll", "HSInfo.Server", "Hook", "Load");
            System.Threading.Thread.Sleep(500);
            m_client = HSInfo.ClientMessageStream.Create("foobar");
            m_client.RegisterListener(new MyListener());
        }

        private void SwitchInputBtn_Click(object sender, EventArgs e) {
            m_client.Write(new HSInfo.MessageTypes.SwitchInput());
        }

        private void DebugBtn_Click(object sender, EventArgs e) {
            m_client.Write(new HSInfo.MessageTypes.DebugMessage("Hello World"));
        }

        private HSInfo.ClientMessageStream m_client;
        private System.Threading.Thread m_thread;

        private void GetCurrentSceneBtn_Click(object sender, EventArgs e) {
            m_client.Write(new HSInfo.MessageTypes.CurrentScene());
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            
        }

        private void SetMouseToBtn_Click(object sender, EventArgs e) {
            string x = textBox1.Text;
            string y = textBox2.Text;
            m_client.Write(new HSInfo.MessageTypes.SetMouseTo(float.Parse(x), float.Parse(y)));

        }
    }
}
