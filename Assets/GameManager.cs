using System;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _timeText;
    [SerializeField]
    private float _countdownSec = 3 * 60f;
    [SerializeField]
    private GameObject _playerGObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���ԊǗ�
        _timeText.text = CountDown(Time.deltaTime);
        // �X�R�A�\��
        _scoreText.text = _playerGObj.GetComponent<CarMove>().GetScore().ToString();

    }

    string CountDown(float delta_time)
    {
        // ���ݎ��Ԃ�^����ƃJ�E���^�_�E�������b�̃e�L�X�g��Ԃ�
        _countdownSec -= Time.deltaTime;
        if (_countdownSec <= 0)
            _countdownSec = 0;
        var timeSpan = new TimeSpan(0, 0, (int)_countdownSec);
        return timeSpan.ToString(@"mm\:ss");
    }
}
