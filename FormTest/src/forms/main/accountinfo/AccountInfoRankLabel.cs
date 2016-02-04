using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSServer {
    public class AccountInfoRankLabel {
        public AccountInfoRankLabel(MainForm form) {
            m_form = form;
        }
        public void Set(int rank, int stars) {
            m_form.lblAccountInfoRank.Text = "Rank: " + rank + " (+" + stars + ")";
        }
        private MainForm m_form;
    }
}
