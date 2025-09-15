using UnityEngine;

public class NodeControl : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _panelCanvas;
    [SerializeField]
    private GameObject _placeToChoose;
    [SerializeField]
    private DefenseTowerData _towerData;


    public void OpenChoicePanel()
    {
        _panelCanvas.alpha = 1;
    }

    public void CloseChoicePanel()
    {
        _panelCanvas.alpha = 0;
    }

    public void ChooseTower(int id)
    {
        _placeToChoose.SetActive(false);
        CloseChoicePanel();
        GameObject.Instantiate(_towerData.GetTowerById(id), _placeToChoose.transform.position, _placeToChoose.transform.rotation);
        
    }
}
