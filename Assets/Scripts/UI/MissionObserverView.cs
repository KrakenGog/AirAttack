using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionObserverView : MonoBehaviour
{
    [SerializeField] private Image _fillBar;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _progress;
    [SerializeField] private string _completeText;
    [SerializeField] private Color _completeColor;

    public void Initialize(GameObserver gameObserver)
    {
        UpdateState(gameObserver);

        gameObserver.OnChanged += (_) =>
        {
            UpdateState(gameObserver);
        };

        gameObserver.OnReached += (_) =>
        {
            _progress.text = _completeText;
            _progress.color = _completeColor;
        };
    }

    private void UpdateState(GameObserver gameObserver)
    {
        _progress.text = $"{gameObserver.Progress}/{gameObserver.Aim}";
        _description.text = gameObserver.Description;
        _fillBar.fillAmount = gameObserver.Progress / gameObserver.Aim;
    }

}
