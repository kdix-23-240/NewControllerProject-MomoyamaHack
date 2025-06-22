using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool isHit = false;

    /// <summary>
    /// �N������
    /// �Փ˂����I�u�W�F�N�g���_�Ȃ�΁AisHit��true�ɂ��Čx����\�� 
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hit");
        // �Փˑ��肪�uStick�v�܂��́uGoal�v�̏ꍇ�̂ݏ���
        if (other.gameObject.CompareTag("Stick") || other.gameObject.CompareTag("Goal"))
        {
            // �v���C���[�̉�]�ƈړ����֎~
            isHit = true;
            //GameSystem.Instance.SetCanRotate(false);
            //GameSystem.Instance.SetCanMove(false);

            //// ��]�X�N���v�g�𖳌���
            //var playerObj = transform.root;
            //var rotate = playerObj.GetComponent<HandleRotate>();
            //if (rotate != null)
            //{
            //    rotate.canRotate = false;
            //    Debug.Log("[HandleCollision] ��]��~�iPlayer�o�R�j");
            //}

            //// �Y���̃��[�_���ibiribiriModal �܂��� goalModal�j��Canvas���ɕ\��
            //GameObject dialogPrefab = collision.gameObject.CompareTag("Stick") ? biribiriModal : goalModal;
            //var dialog = Instantiate(dialogPrefab);
            //dialog.transform.SetParent(parent.transform, false);

            //// Stick�i��Q���j�ɏՓ˂����ꍇ�̒ǉ�����
            //if (collision.gameObject.CompareTag("Stick"))
            //{
            //    Debug.Log("Calling StartWarningSequence()");
            //    var warningManager = FindObjectOfType<WarningManager>();
            //    //warningManager?.StartWarningSequence(); // �� ���M�Ǘ���WarningManager�ɔC����
            //    Debug.Log("Game Over");
            //}
        }
    }

    public bool GetIsHit()
    {
        return isHit;
    }
}