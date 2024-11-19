using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳.Models.ModelTypes;

namespace 記帳.Forms
{
    public abstract class MultipleCheckBoxBaseForm : Form
    {
        protected Button searchButton;
        protected DateTimePicker startDatePicker;
        protected DateTimePicker endDatePicker;
        protected CheckBox[] allCheckBoxes;
        protected CheckBox[] typeCheckBoxes;
        protected CheckBox[] paymentCheckBoxes;
        protected CheckBox[] targetCheckBoxes;

        public MultipleCheckBoxBaseForm()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            // 查詢按鈕
            this.searchButton = new Button
            {
                Text = "查詢",
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(100, 30)
            };
            this.Controls.Add(this.searchButton);

            this.startDatePicker = new DateTimePicker
            {
                Location = new System.Drawing.Point(120, 10),
                Size = new System.Drawing.Size(100, 30)
            };
            this.endDatePicker = new DateTimePicker
            {
                Location = new System.Drawing.Point(230, 10),
                Size = new System.Drawing.Size(100, 30)
            };
            this.Controls.Add(this.startDatePicker);
            this.Controls.Add(this.endDatePicker);

            this.AddAllOptionsCheckedBoxes();
            this.AddTypeOptionsCheckBoxes();
            this.AddPaymentOptionsCheckBoxes();

            this.paymentCheckBoxes = new CheckBox[0];
            this.targetCheckBoxes = new CheckBox[0];
        }

        private void AddAllOptionsCheckedBoxes()
        {
            string[] allCheckBoxOptions = new string[] { "所有類別", "所有付款方式", "所有對象" };
            this.allCheckBoxes = new CheckBox[allCheckBoxOptions.Length];
            int xPos = 20;
            int yPos = 50;
            int count = 0;

            foreach (string option in allCheckBoxOptions)
            {
                this.allCheckBoxes[count] = new CheckBox
                {
                    Text = option,
                    Location = new System.Drawing.Point(xPos, yPos),
                };
                this.Controls.Add(this.allCheckBoxes[count]);
                count++;
                yPos += 30;
            }
        }

        private void AddTypeOptionsCheckBoxes()
        {
            int xPos = 50;
            int yPos = 50;
            int count = 0;
            this.typeCheckBoxes = new CheckBox[ModelTypes.types.Length];
            foreach (string type in ModelTypes.types)
            {
                this.typeCheckBoxes[count] = new CheckBox
                {
                    Text = type,
                    Location = new System.Drawing.Point(xPos, yPos),
                };
                this.Controls.Add(this.typeCheckBoxes[count]);
                count++;
                xPos += 80;
            }
        }

        private void AddPaymentOptionsCheckBoxes()
        {
            int xPos = 50;
            int yPos = 80;
            int count = 0;
            this.paymentCheckBoxes = new CheckBox[ModelTypes.paymentMethods.Length];
            foreach (string payment in ModelTypes.paymentMethods)
            {
                this.paymentCheckBoxes[count] = new CheckBox
                {
                    Text = payment,
                    Location = new System.Drawing.Point(xPos, yPos),
                };
                this.Controls.Add(this.paymentCheckBoxes[count]);
                count++;
                xPos += 80;
            }
        }
    }
}