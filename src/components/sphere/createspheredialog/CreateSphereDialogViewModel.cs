using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Spherification.src.components.sphere.createspheredialog
{
    class CreateSphereDialogViewModel
    {

        private readonly Action<CreateSphereDialogViewModel> closeHandler;

        public ActionCommand okAction
        {
            get
            {
                return new ActionCommand(p => ok());
            }
        }

        public ActionCommand cancelAction
        {
            get
            {
                return new ActionCommand(p => cancel());
            }
        }

        public Color? color { get; set; }
        public String name { get; set; }
        public int radius { get; set; }
        public int accuracy { get; set; }

        public string MessageText { get; set; }  // Message displayed to user

        public string UserInput { get; set; }   // User input returned

        public bool Cancel { get; set; }  // Flagged true if user clicks cancel button
        //Constructor

        public CreateSphereDialogViewModel(Action<CreateSphereDialogViewModel> closeHandler)
        {
            Cancel = false;
            this.closeHandler = closeHandler;
        }


        public void ok()
        {
            Cancel = false;
            closeHandler(this);
        }

        public void cancel()
        {
            Cancel = true;
            closeHandler(this);
        }
    }
}

