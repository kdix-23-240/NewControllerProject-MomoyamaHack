public class GripChecker
{
    private float bend = 0f; // �Ȃ�����ێ�����ϐ�
    private bool isGrip = false; // �O���b�v��Ԃ�ێ�����ϐ�
    private float bendWall = 4f; // �O���b�v�ƔF������Ȃ�����臒l
    /// <summary>
    /// �O���b�v��Ԃ��`�F�b�N���郁�\�b�h
    /// </summary>
    /// <returns>�O���b�v��Ԃ�true�Ȃ��true�A�����łȂ����false</returns>
    public bool CheckGrip()
    {
        return isGrip;
    }
}