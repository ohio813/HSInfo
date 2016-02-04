using HSInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSServer {
    public class MainPanel {
        public MainPanel(MainForm form) {
            m_form = form;
            m_accountInfoGroup = new AccountInfoGroup(form);
            form.DebugBtn.Click += OnButtonDebug;
        }
        public void Hide() {
            m_form.pnlInfo.Hide();
        }
        public void Show() {
            m_form.pnlInfo.Show();
        }
        private void OnButtonDebug(object sender, EventArgs args) {
            HSMPServer.Get().SendDebugMessage("hi");
        }
        private MainForm m_form;
        private AccountInfoGroup m_accountInfoGroup;
    }
}
