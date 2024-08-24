using System;
using System.Threading;
using System.Windows.Forms;

namespace 記帳.Utils
{
    static class Debounce
    {
        private static System.Threading.Timer timer;

        public static void DebounceClick(this Form form, Action action, int delay)
        {
            if (timer != null)
            {
                timer.Change(delay, Timeout.Infinite);
                return;
            }

            form.Tag = action;
            timer = new System.Threading.Timer(CallBack, form, delay, Timeout.Infinite);
        }

        private static void CallBack(object data)
        {
            Form form = (Form)data;
            Action action = (Action)form.Tag;
            form.Invoke((Action)(() => { action.Invoke(); }));
        }
    }
}
