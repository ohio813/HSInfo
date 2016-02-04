

using System;
using System.Windows.Forms;
using HSInfo;
using System.ComponentModel;

namespace HSServer {
    public partial class MainForm : Form {
        /* --- Constructors --- */
        public MainForm() {
            InitializeComponent();
        }
        /* --- Instance Methods (Interface) --- */
        public void ShowNotifyIcon(string title, string msg, int duration) {
            MethodInvoker m = delegate {
                notifyIcon1.BalloonTipTitle = title;
                notifyIcon1.BalloonTipText = msg;
                notifyIcon1.ShowBalloonTip(duration);
            };
            Invoke(m);
        }
        /* --- Instance Methods (Auxiliary) --- */
        protected override void OnClosed(EventArgs e) {
            HSMPServer.Get().Send(new MsgStatus(MsgStatus.State.DISCONNECTED, MsgStatus.User.CLIENT, "NORMAL"));
            HSMPServer.Get().StopListenerThread();
            HSMPServer.Get().Close();
            notifyIcon1.Visible = false;
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) {
            Activate();
        }
        private void MainForm_Load(object sender, EventArgs e) {
            HSMPServer.Create("foobar", 8192);
            HSMPServer.Get().StartListenerThread();
            m_window = new MainWindow(this);
            //notifyIcon1.Icon = new System.Drawing.Icon(@"C:\Users\mattg\Desktop\HSBot.ico", new System.Drawing.Size(128, 128));
        }
        /* --- Instance Fields --- */
        private MainWindow m_window;
    }
}
