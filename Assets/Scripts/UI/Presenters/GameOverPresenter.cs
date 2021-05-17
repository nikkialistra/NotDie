using Entities.Player;
using UI.Views;

namespace UI.Presenters
{
    public class GameOverPresenter
    {
        private readonly Hp _hp;
        private readonly GameOverView _gameOverView;

        public GameOverPresenter(Hp hp, GameOverView gameOverView)
        {
            _hp = hp;
            _gameOverView = gameOverView;
        }

        public void SetUp()
        {
            _hp.GameOver += _gameOverView.GameOver;
        }
    }
}