using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameOverManager : MonoBehaviour
{
    public void Start()
    {
        // �Q�[���I�[�o�[��ʂ��\����
        this.gameObject.SetActive(false);
    }

    public void GameOverInit()
    {
        // �Q�[���I�[�o�[���A��ʕ\�����X�R�A���v�Z���`��
        this.gameObject.SetActive(true);
        
    }

   
}
