using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSServer {
    public class AccountInfoGoldLabel {
        public AccountInfoGoldLabel(MainForm form) {
            m_form = form;
        }
        public void Set(long amount) {
            m_form.lblAccountInfoGold.Text = "Gold: " + amount;
        }
        private MainForm m_form;
    }
}
