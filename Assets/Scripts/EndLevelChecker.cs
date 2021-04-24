using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/EndLevelChecker")]
public class EndLevelChecker : ManagerBase,IAwake
{
    [SerializeField] private int _killCountToWin; 
    private int _killCount;
    private GameObject _scoreBoard;
    public void OnAwake()
    {
        if (Toolbox.Get<SceneController>().GetIsMainScene()) return;
        _scoreBoard=GameObject.FindGameObjectWithTag("ScoreBoard");
        _scoreBoard.SetActive(false);
    }

    public override void ClearScene()
    {
        _killCount = 0;
    }
    public void UpdateKillCount()
    {
        _killCount++;
        if (_killCount >= _killCountToWin)
        {
            _scoreBoard.SetActive(true);
        }
    }
}
