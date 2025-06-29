using System.Drawing.Text;
using UnityEngine;

public class GriConDirectionSetting : MonoBehaviour
{
    private bool isSet1 = false; // 1�����ڂ̐ݒ肪�����������ǂ���
    private bool isSet2 = false; // 2�����ڂ̐ݒ肪�����������ǂ���
    private bool isSet3 = false; // 3�����ڂ̐ݒ肪�����������ǂ���
    private bool isSet4 = false; // 4�����ڂ̐ݒ肪�����������ǂ���
    private float flameCounter = 0f; // �t���[���J�E���^�[
    [SerializeField] private float selectTime = 2f; // �X�e�[�W�I���̂��߂̎���臒l
    public static string stageName = null; // �X�e�[�W��

    private void Update()
    {
        CheckIsSet(); // �e�����̐ݒ���m�F

        if (CheckIsAllSet())
        {
            flameCounter += Time.deltaTime; // �t���[���J�E���^�[���X�V
        }
        else
        {
            flameCounter = 0f; // �S�����̐ݒ肪�������Ă��Ȃ��ꍇ�̓J�E���^�[�����Z�b�g
        }

        if(flameCounter >= selectTime)
        {
            Debug.Log("�S�����̐ݒ肪�������܂����B�V�[����ύX���܂��B");
            UnityEngine.SceneManagement.SceneManager.LoadScene(stageName);
            stageName = null; // �V�[���ύX��̓X�e�[�W�������Z�b�g
        }
    }

    /// <summary>
    /// ���̃I�u�W�F�N�g�̎q�R���|�[�l���g�̃J�������擾���A���̃J��������U������ray���΂�
    /// �e�����ɓ��������I�u�W�F�N�g�̖��O���擾���A���̔ԍ��̕����̐ݒ����������
    /// ray���O�ꂽ��ݒ���������Ȃ�
    /// </summary>
    private void CheckIsSet()
    {
        Camera camera = GetComponentInChildren<Camera>();
        if (camera == null)
        {
            Debug.LogError("GriConDirectionSetting: �q�I�u�W�F�N�g�ɃJ������������܂���B");
            return;
        }
        RaycastHit hit;
        Vector3[] directions = new Vector3[]
        {
            camera.transform.forward, // �O��
            -camera.transform.forward, // ���
            camera.transform.right, // �E
            -camera.transform.right, // ��
            camera.transform.up, // ��
            -camera.transform.up // ��
        };
        for (int i = 0; i < directions.Length; i++)
        {
            if (Physics.Raycast(camera.transform.position, directions[i], out hit))
            {
                string objectName = hit.collider.gameObject.name;
                Debug.Log($"���� {i + 1} �̃I�u�W�F�N�g��: {objectName}");
                switch (i)
                {
                    case 0: isSet1 = true; break;
                    case 1: isSet2 = true; break;
                    case 2: isSet3 = true; break;
                    case 3: isSet4 = true; break;
                }
            }
            else
            {
                Debug.Log($"���� {i + 1} �̃I�u�W�F�N�g��������܂���B�ݒ�͊������܂���B");
                switch (i)
                {
                    case 0: isSet1 = false; break;
                    case 1: isSet2 = false; break;
                    case 2: isSet3 = false; break;
                    case 3: isSet4 = false; break;
                }
            }
        }
    }

    private bool CheckIsAllSet()
    {
        if(isSet1 && isSet2 && isSet3 && isSet4)
        {
            Debug.Log("�S�����̐ݒ肪�������܂����B");
            return true;
        }
        else
        {
            Debug.Log("�S�����̐ݒ肪�������Ă��܂���B");
            return false;
        }
    }
}