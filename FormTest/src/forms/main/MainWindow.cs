
using HSInfo;
using System;
using System.Windows.Forms;

namespace HSServer {
    public class MainWindow {
        /* --- Constructors --- */
        public MainWindow(MainForm form) {
            m_form = form;
            m_menuStrip = new MenuStrip(form);
            m_mainPanel = new MainPanel(form);
            m_connectPanel = new ConnectPanel(form);
            HSMPServer.Get().RegisterUIListener(MsgStatus.ID, form, OnMsgStatus);
            HSMPServer.Get().RegisterListener(MsgDebug.ID, OnMsgDebug);
            HSMPServer.Get().RegisterListener(MsgDeckList.ID, OnMsgDeckList);

            m_bot = new BotHandler();
        }
        /* --- Instance Methods (Auxiliary) --- */
        private void OnMsgDeckList(HSInfo.Message msg) {
            var deckList = (MsgDeckList)msg;
            foreach (var d in deckList.Decks) {
                Console.WriteLine(d.Name + " : " + d.HeroClass);
            }
        }
        private void OnMsgStatus(HSInfo.Message msg) {
            MsgStatus status = (MsgStatus)msg;
            switch (status.Status) {
                case MsgStatus.State.CONNECTED:
                    HSMPServer.Get().Send(new MsgAccountInfoRequest());
                    HSMPServer.Get().Send(new MsgStatus(MsgStatus.State.CONNECTED, MsgStatus.User.SERVER, "Success"));
                    m_mainPanel.Show();
                    m_connectPanel.Hide();
                    m_form.lblConnecting.Hide();
                    break;
                case MsgStatus.State.DISCONNECTED:
                    m_mainPanel.Hide();
                    m_connectPanel.Show();
                    break;
                case MsgStatus.State.HANDSHAKE_FAILED:
                    m_form.lblConnecting.Text = status.Message;
                    break;
            }
        }
        private void OnMsgDebug(HSInfo.Message msg) {
            MsgDebug debug = (MsgDebug)msg;
            Console.WriteLine(debug.Message);
        }
        /* --- Instance Fields --- */
        private ConnectPanel m_connectPanel;
        private MainPanel m_mainPanel;
        private MenuStrip m_menuStrip;

        private BotHandler m_bot;

        private MainForm m_form;
    }
}
