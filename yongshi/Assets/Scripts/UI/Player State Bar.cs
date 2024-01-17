using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStateBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;
    public Image powerImage;
    private bool isRecovering;
    private Character currentCharacter;
    private void Update()
    {
        if (healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount-=Time.deltaTime;
        }
        if(isRecovering) {
            float per = currentCharacter.currentPower / currentCharacter.maxPower;
            powerImage.fillAmount=per;
            if (per >= 1)
            {
                isRecovering = false;
                return;
            }
        }
    }
    /// <summary>
    /// ����Health�ı���ٷֱ�
    /// </summary>
    /// <param name="per">�ٷֱ�:current?max</param>
    public void OnHealthChange(float per)
    {
        healthImage.fillAmount = per;
    }
    public void OnPowerChange(Character character)
    {
        isRecovering = true;
        currentCharacter = character;
    }
}
