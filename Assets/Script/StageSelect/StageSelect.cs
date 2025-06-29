using System.Drawing.Drawing2D;
using UnityEngine;
public class StageSelect : MonoBehaviour
{
    [SerializeField] private Camera stageSelectCamera;
    private float bend; // �Ȃ��Z���T�[�̒l���i�[����ϐ�
    [SerializeField] private float bendWall = 4f; // �Ȃ��Z���T�[��臒l
    private bool isGrip = false; 
    [SerializeField] private float selectTime = 1f; // �X�e�[�W�I���̂��߂̎���臒l
    private float flameCounter = 0f; // �t���[���J�E���^�[
    private string stageName = null; // �X�e�[�W�����i�[����ϐ�

    void Awake()
    {

    }

    private void Start()
    {

    }

    void Update()
    {
        float[] data = Get_Information.Instance.GetReceivedData();
        bend = data[3];
        if (bendWall < bend)
        {
            isGrip = true;
            flameCounter += Time.deltaTime; // �t���[���J�E���^�[���X�V
        }
        else
        {
            isGrip = false;
            flameCounter = 0f; // �����Ă��Ȃ��ꍇ�̓J�E���^�[�����Z�b�g
        }

        if (CheckGrip())
        {

            if (flameCounter >= selectTime)
            {
                Debug.Log("�X�e�[�W������");
                int stageNum = DecideStage(); // �X�e�[�W�ԍ�������
                if (stageNum >= 0) // �L���ȃX�e�[�W�ԍ������肳�ꂽ�ꍇ
                {
                    ChangeScene(stageNum); // �V�[����ύX
                }
                else
                {
                    Debug.LogWarning("�����ȃX�e�[�W�ԍ����I������܂����B");
                }
            }   
        }
    }

    /// <summary>
    /// �J�����������Ă�������ɂ���I�u�W�F�N�g�̖��O�𐔎��Ƃ��Ď擾����
    /// ������X�e�[�W�ԍ��Ƃ��Ďg�p����
    /// �Ō�ɂ��̃X�e�[�W�ԍ���Ԃ�
    /// </summary>
    /// <returns></returns>

    private int DecideStage()
    {
        int stageNum = -1; // �����l�Ƃ��Ė����Ȓl��ݒ�
        RaycastHit hit;
        Ray ray = new Ray(stageSelectCamera.transform.position, stageSelectCamera.transform.forward);
        if(Physics.Raycast(ray, out hit, 100f)) // ���C�L���X�g�ŃI�u�W�F�N�g�����o
        {
            Debug.Log("�q�b�g�����I�u�W�F�N�g: " + hit.collider.gameObject.name);
            if (int.TryParse(hit.collider.gameObject.name, out stageNum)) // �I�u�W�F�N�g�̖��O�𐮐��ɕϊ�
            {
                Debug.Log("�X�e�[�W�ԍ�: " + stageNum);
            }
            else
            {
                Debug.LogWarning("�I�u�W�F�N�g�̖��O�������ɕϊ��ł��܂���ł����B");
                stageNum = -1; // �����Ȓl��ݒ�
            }
        }
        else
        {
            Debug.LogWarning("���C�L���X�g�ŃI�u�W�F�N�g���q�b�g���܂���ł����B");
        }
        return stageNum;
    }

    private void ChangeScene(int stageNum)
    {
        // �V�[����ύX���鏈���������Ɏ���
        Debug.Log("�V�[����ύX: " + stageNum);
        stageName = "Stage" + stageNum.ToString(); // �X�e�[�W����ݒ�
        GriConDirectionSetting.stageName = stageName; // GriConDirectionSetting�ɃX�e�[�W����n��
        UnityEngine.SceneManagement.SceneManager.LoadScene("GriConSetting");
    }

    private bool CheckGrip()
    {
        if(isGrip)
        {
            Debug.LogWarning("�����Ă���");
            return true;
        }
        else
        {
            Debug.Log("�����Ă��Ȃ�");
            return false;
        }   
    }
}