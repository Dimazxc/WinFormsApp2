using System.Windows.Forms;

namespace WinFormsApp2.Helpers
{
    public static class FormSwitcher
    {
        public static void SwitchForm(Form formToClose, Form formToOpen)
        {
            formToClose.Hide();
            formToOpen.ShowDialog();
            formToClose.Close();
        }
    }
}
