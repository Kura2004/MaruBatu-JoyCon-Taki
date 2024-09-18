using UnityEngine;
using DG.Tweening; // DOTween ���O��Ԃ�ǉ�

public class InvertColorsEffect : CameraPostProcessingEffect
{
    private float interval = 1f; // �G�t�F�N�g�؂�ւ��Ԋu
    private float duration = 5f; // �A�j���[�V�����̑��Đ�����

    private Sequence sequence;

    // �V���O���g���̃C���X�^���X
    public static InvertColorsEffect Instance => SingletonMonoBehaviour<InvertColorsEffect>.Instance;

    public void StartAnimation(float newInterval, float newDuration)
    {
        interval = newInterval;
        duration = newDuration;

        // DOTween�ŃA�j���[�V������ݒ�
        sequence = DOTween.Sequence();

        // �G�t�F�N�g�̐؂�ւ����s���^�C�~���O�ŃA�j���[�V�������쐬
        sequence
            .AppendCallback(() => SetEffect(EffectType.InvertColors))
            .AppendInterval(interval)
            .AppendCallback(() => SetEffect(EffectType.None))
            .AppendInterval(interval)
            .SetLoops((int)(duration / (2 * interval)), LoopType.Yoyo) // �G�t�F�N�g�����݂ɐ؂�ւ�
            .OnKill(() => SetEffect(EffectType.None)); // �A�j���[�V�������I��������G�t�F�N�g�𖳌��ɂ���
    }

    public void StopAnimation()
    {
        if (sequence != null)
        {
            sequence.Kill(); // DOTween�ō쐬�����A�j���[�V�������~
        }
        SetEffect(EffectType.None);
    }
}

