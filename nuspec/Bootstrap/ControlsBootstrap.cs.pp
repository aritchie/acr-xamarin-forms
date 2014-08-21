// HACK: this is to deal with the linker nuking the assembly
using Acr.XamForms.Controls.iOS;

namespace $rootnamespace$.Bootstrap
{
    public class ControlsBootstrap 
    {
        public ControlsBootstrap() 
        {
            new UserDialogService();
        }
    }
}