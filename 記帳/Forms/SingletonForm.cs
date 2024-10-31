using System.Collections.Generic;
using System.Windows.Forms;
using 記帳.Models.Enums;

namespace 記帳.Forms
{
    internal class SingletonForm
    {
        private static Form form = null;

        private static Dictionary<FormType, Form> forms = new Dictionary<FormType, Form>();

        public static Form GetForm(FormType type)
        {
            form?.Hide();

            switch (type)
            {
                case FormType.AddOneRecordForm:
                    form = new AddRecordForm();
                    break;

                case FormType.RecordForm:
                    form = new RecordForm();
                    break;

                case FormType.AccountForm:
                    form = new AccountForm();
                    break;

                case FormType.ChartForm:
                    form = new ChartForm();
                    break;

                default:
                    form = new AddRecordForm();
                    break;
            }

            if (!forms.ContainsKey(type))
            {
                forms.Add(type, form);
            }
            return form;
        }
    }
}