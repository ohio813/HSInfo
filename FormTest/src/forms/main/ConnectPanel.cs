using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSServer {
    public class ConnectPanel {
        /* --- Constructors --- */
        public ConnectPanel(MainForm form) {
            m_form = form;
            m_form.btnConnect.Click += OnButtonClick;
        }
        public void Hide() {
            m_form.pnlConnect.Hide();
        }
        public void Show() {
            m_form.pnlConnect.Show();
        }
        private void OnButtonClick(object sender, EventArgs e) {
            int err = HSInfo.Injector.Inject("hearthstone.exe", "HSInfoServer.dll", "HSClient", "Hook", "Load");
            if (err == 0) {
                m_form.lblConnecting.Text = "Connecting...";
                m_form.lblConnecting.Visible = true;
            } else {
                m_form.lblConnecting.Text = "Injection Failed";
            }
            m_form.lblConnecting.Visible = true;
        }
        /* --- Instance Fields --- */
        private MainForm m_form;
    }
}
