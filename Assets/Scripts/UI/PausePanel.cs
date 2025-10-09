

namespace Assets.Scripts.UI
{
    public class PausePanel : OpenPanel
    {
        public void Restart()
        {
            SceneController.Restart();
        }

        public void Home()
        {
            SceneController.LoadRoadMap();
        }
    }
}
