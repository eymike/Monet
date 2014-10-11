using System;
using System.Windows.Forms;

namespace MonetCore
{
    public class FormService
    {
        public FormService(Form form)
        {
            Form = form;
        }

        public Form Form { get; private set; }
    }
}
