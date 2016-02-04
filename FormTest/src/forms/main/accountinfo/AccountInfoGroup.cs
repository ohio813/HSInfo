using HSInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSServer {
    public class AccountInfoGroup {
        /* --- Constructors --- */
        public AccountInfoGroup(MainForm form) {
            m_form = form;
            GoldLabel = new AccountInfoGoldLabel(m_form);
            DustLabel = new AccountInfoDustLabel(m_form);
            PacksLabel = new AccountInfoPacksLabel(m_form);
            RankLabel = new AccountInfoRankLabel(m_form);
            HSMPServer.Get().RegisterUIListener(MsgAccountInfoResponse.ID, form, OnMsgAccountInfoResponse);
            HSMPServer.Get().RegisterUIListener(MsgCurrencyBalance.ID, form, OnMsgCurrencyBalance);
        }
        /* --- Instance Methods (Auxiliary) --- */
        private void OnMsgAccountInfoResponse(HSInfo.Message msg) {
            var c = (MsgAccountInfoResponse)msg;
            SetAccountName(c.Name);
            GoldLabel.Set(c.Gold);
            DustLabel.Set(c.Dust);
            PacksLabel.Set(c.Packs);
            RankLabel.Set(c.Rank, c.Stars);
        }
        private void OnMsgCurrencyBalance(HSInfo.Message msg) {
            var c = (MsgCurrencyBalance)msg;
            switch (c.Type) {
                case MsgCurrencyBalance.CurrencyType.GOLD:
                    GoldLabel.Set(c.Amount);
                    break;
                case MsgCurrencyBalance.CurrencyType.DUST:
                    DustLabel.Set(c.Amount);
                    break;
                case MsgCurrencyBalance.CurrencyType.PACKS:
                    PacksLabel.Set(c.Amount);
                    break;
            }
        }
        public void SetAccountName(string name) {
            m_form.groupAccountInfo.Text = "Account Info (" + name + ")";
        }
        /* --- Instance Fields --- */
        private MainForm m_form;
        public AccountInfoGoldLabel GoldLabel { get; private set; }
        public AccountInfoDustLabel DustLabel { get; private set; }
        public AccountInfoPacksLabel PacksLabel { get; private set; }
        public AccountInfoRankLabel RankLabel { get; private set; }
    }
}
