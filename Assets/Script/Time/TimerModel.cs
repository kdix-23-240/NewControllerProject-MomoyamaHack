using System.Collections;
using UniRx;
using UnityEngine;

/// <summary>
/// �ǂ̃N���X������g����ėp�^�C�}�[
/// �R���[�`�����s��MonoBehaviour���ōs��
/// </summary>
public class TimerModel
{
    public ReactiveProperty<bool> IsCompleted { get; } = new ReactiveProperty<bool>(false);
    public float ElapsedTime { get; private set; } = 0f;

    /// <summary>
    /// �^�C�}�[�R���[�`��
    /// </summary>
    /// <param name="duration">�^�C�}�[���ԁi�b�j</param>
    /// <returns></returns>
    public IEnumerator TimerCoroutine(float duration)
    {
        IsCompleted.Value = false;
        ElapsedTime = 0f;
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            ElapsedTime = Time.time - startTime;
            yield return null;
        }
        ElapsedTime = duration;
        IsCompleted.Value = true;
        Debug.Log($"[UniversalTimer] Timer ended after {ElapsedTime} seconds.");
    }

    /// <summary>
    /// �^�C�}�[�����Z�b�g
    /// </summary>
    public void Reset()
    {
        IsCompleted.Value = false;
        ElapsedTime = 0f;
    }
}