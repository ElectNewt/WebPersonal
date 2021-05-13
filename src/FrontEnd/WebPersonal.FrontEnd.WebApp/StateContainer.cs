using System;

namespace WebPersonal.FrontEnd.WebApp
{
    public class StateContainer
    {
        public string SelectedCssClass { get; private set; } = "rojo";

        public event Action CambiarColor;

        public void AsignarColorCss(string newCssClass)
        {
            SelectedCssClass = newCssClass;
            ExecuteAction();
        }

        private void ExecuteAction() => CambiarColor?.Invoke();
    }
}
