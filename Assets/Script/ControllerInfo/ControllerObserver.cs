using UnityEngine;

public class ControllerObserver : MonoBehaviour
{

    void Awake()
    {
        
    }
    void Start()
    {

    }
    void Update()
    {
        // Get_Information�����������̏ꍇ�̓X�L�b�v
        if (Get_Information.Instance == null) return;

        // �V���A������p�x�f�[�^���擾
        float[] data = Get_Information.Instance.GetReceivedData();
        ControllerModel.GetInstance.RotateX.Value = -data[0];
        ControllerModel.GetInstance.RotateY.Value = data[2];// y���v�f��z���v�f���t�ɂȂ��Ă���̂Œ���
        ControllerModel.GetInstance.RotateZ.Value = data[1];
        ControllerModel.GetInstance.Bend.Value = data[3];
    }
}