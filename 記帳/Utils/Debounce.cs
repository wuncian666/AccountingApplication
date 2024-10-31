using System;
using System.Threading;
using System.Windows.Forms;

namespace 記帳.Utils
{
    static class Debounce
    {
        private static System.Threading.Timer timer;
        private static object state;
        public static void DebounceClick(this Form form, Action action, int delay)
        {
            state = form;
            form.Tag = action;

            if (timer != null)
            {
                timer.Change(delay, Timeout.Infinite);
                return;
            }

            timer = new System.Threading.Timer(CallBack, form, delay, Timeout.Infinite);
        }

        private static void CallBack(object data)
        {
            Form form = (Form)state;
            Action action = (Action)form.Tag;
            form.Invoke((Action)(() => { action.Invoke(); }));
        }
    }
}
