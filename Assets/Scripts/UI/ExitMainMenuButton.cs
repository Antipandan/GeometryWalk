using UnityEngine.SceneManagement;

namespace DefaultNamespace.UI
{
    public sealed class ExitMainMenuButton : ButtonBase
    {
        protected override void SubscribeEvents()
        {
            if (button != null)
            {
                button.onClick.RemoveListener(ExitMainMenuButtonPressed);
                button.onClick.AddListener(ExitMainMenuButtonPressed);
            }
        }

        protected override void UnsubscribeEvents()
        {
            button.onClick.RemoveListener(ExitMainMenuButtonPressed);
        }

        private void ExitMainMenuButtonPressed()
        {
            GameUtilities.LoadMainMenu();
        }
    }
}