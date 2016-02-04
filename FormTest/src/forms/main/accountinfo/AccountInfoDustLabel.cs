using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSServer {
    public class AccountInfoDustLabel {
        public AccountInfoDustLabel(MainForm form) {
            m_form = form;
        }
        public void Set(long amount) {
            m_form.lblAccountInfoDust.Text = "Dust: " + amount;
        }
        private MainForm m_form;
    }
}
