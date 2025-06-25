using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using System.Collections;

public class WarningSender : MonoBehaviour
{
    private Coroutine warningCoroutine;
    private bool isWarningSequenceRunning = false;
    private float bilibiliTime = 2f; // �x���Ԃ̑ҋ@���ԁi�b�j

    public void OnChangeWarningLevel(int level)
    {
        switch (level)
        {
            case 1:
                Debug.Log("---------------------------------------------------");
                Debug.Log("WarningSender:WarningLevel3 �����o");
                Debug.Log("WarningSender:�_�ɋ߂Â������ł��A���Ē����Ă�������");
                Debug.Log("---------------------------------------------------");
                SendWarning('1');
                break;
            case 2:
                Debug.Log("---------------------------------------------------");
                Debug.Log("WarningSender:WarningLevel2 �����o");
                Debug.Log("WarningSender:�_�ɋ߂Â��܂����A�C��t���Ă�������");
                Debug.Log("---------------------------------------------------");
                SendWarning('2');
                break;
            case 3:
                Debug.Log("---------------------------------------------------");
                Debug.Log("WarningSender:WarningLevel1 �����o");
                Debug.Log("WarningSender:�_�ɏ����߂Â��܂���");
                Debug.Log("---------------------------------------------------");
                SendWarning('3');
                break;
            case 4:
                Debug.Log("---------------------------------------------------");
                Debug.Log("WarningSender:WarningLevel4 �����o");
                Debug.Log("WarningSender:�_�ɐG��܂����A�Q�[���I�[�o�[�ł�");
                Debug.Log("---------------------------------------------------");
                StartWarningSequence();
                break;
            case 5:
                Debug.Log("---------------------------------------------------");
                Debug.Log("WarningSender:���S�ȏ�Ԃɖ߂�܂���");
                Debug.Log("---------------------------------------------------");
                SendWarning('5');
                break;
            default:
                Debug.Log("---------------------------------------------------");
                Debug.LogWarning("WarningSender:�s���Ȓl�̌x���ł�");
                Debug.LogWarning("[WarningSender] Invalid warning level: " + level);
                Debug.Log("---------------------------------------------------");
                break;
        }
    }
    /// <summary>
    /// �Փˎ��ȂǂɌĂяo���A�x��4��5��i�K�I�ɑ��M����
    /// </summary>
    private void StartWarningSequence()
    {
        if (!isWarningSequenceRunning)
        {
            if (warningCoroutine != null)
                StopCoroutine(warningCoroutine);

            warningCoroutine = StartCoroutine(WarningSequenceCoroutine());
        }
    }

    private IEnumerator WarningSequenceCoroutine()
    {
        isWarningSequenceRunning = true;

        SendWarning('4');
        Debug.Log("[WarningManager] Sent '4'");

        yield return new WaitForSeconds(bilibiliTime);

        SendWarning('5');
        Debug.Log("[WarningManager] Sent '5'");

        isWarningSequenceRunning = false;
    }

    private void SendWarning(char levelChar)
    {
        if (Get_Information.Instance != null)
        {
            Get_Information.Instance.SetOutgoingByte((byte)levelChar);
            //Debug.Log($"[WarningManager] Sent warning level command: '{levelChar}'");
        }
    }
}