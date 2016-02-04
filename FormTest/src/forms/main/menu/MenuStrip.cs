using HSInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSServer {
    public class MenuStrip {
        public MenuStrip(MainForm form) {
            form.againstPlayerToolStripMenuItem.Click += OnAgainstPlayerClicked;
        }
        private void OnAgainstPlayerClicked(object sender, EventArgs e) {
            var msg = new MsgTaskSet();
            msg.TaskType = HSClient.BotTask.Task.PLAY_TOURNAMENT;
            msg.TaskParams = new TaskParamPlayTournament(0, false);
            HSMPServer.Get().Send(msg);
        }
        private void OnTavernBrawlClicked(object sender, EventArgs e) {

        }
        private void OnAgainstInnkeeperClicked(object sender, EventArgs e) {

        }
    }
}
