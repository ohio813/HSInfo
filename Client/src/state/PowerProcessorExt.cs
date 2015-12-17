
/*
 * File: PowerProcessorExt.cs
 * Notes:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSInfo {
    namespace Client {
        class PowerProcessorExt {
            /* --- Constructors --- */
            PowerProcessorExt() {

            }
            /* --- Instance Methods (Interface) --- */
            public void Clear() {
                m_powerQueue.Clear();
                m_currentTaskList = null;
            }

            /* --- Instance Fields --- */
            private bool m_buildingTaskList;
            private PowerTaskList m_busyTaskList;
            private PowerTaskList m_currentTaskList;
            private PowerTaskList m_earlyConcedeTaskList;
            private PowerTaskList m_gameOverTaskList;
            private bool m_handledFirstEarlyConcede;
            private bool m_historyBlocking;
            private int m_nextTaskListId;
            private PowerQueue m_powerQueue;
            private Stack<PowerTaskList> m_previousStack;
        }
    }
}
