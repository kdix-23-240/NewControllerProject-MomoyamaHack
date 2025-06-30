using UniRx;

public class ControllerModel
{
    // �V���O���g���̃C���X�^���X��ێ�����ÓI�ȃv���p�e�B
    private static ControllerModel _instance;
    public static ControllerModel GetInstance
    {
        get
        {
            // �C���X�^���X���܂����݂��Ȃ��ꍇ�A�V�����쐬����
            if (_instance == null)
            {
                _instance = new ControllerModel();
            }
            return _instance;
        }
    }

    // �e��]���ƃx���h��ReactiveProperty
    // private readonly �t�B�[���h�͕s�v�B����public�v���p�e�B���g��
    public ReactiveProperty<float> RotateX { get; private set; }
    public ReactiveProperty<float> RotateY { get; private set; }
    public ReactiveProperty<float> RotateZ { get; private set; }
    public ReactiveProperty<float> Bend { get; private set; }

    // �R���X�g���N�^��private�ɂ��邱�ƂŁA�O������̃C���X�^���X����h��
    private ControllerModel()
    {
        RotateX = new ReactiveProperty<float>(0f);
        RotateY = new ReactiveProperty<float>(0f);
        RotateZ = new ReactiveProperty<float>(0f);
        Bend = new ReactiveProperty<float>(0f);
    }
}