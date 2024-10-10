using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  // DOTween�̖��O���
using System.Collections;  // �R���[�`�����g�p���邽�߂ɕK�v

public class FallingImagesManager : MonoBehaviour
{
    [SerializeField] private Image imagePrefab;   // Image��Prefab
    [SerializeField] private float minX = -100f;  // ���������X���W�̍ŏ��l
    [SerializeField] private float maxX = 100f;   // ���������X���W�̍ő�l
    [SerializeField] private float startY = 200f; // ���������Y���W
    [SerializeField] private float endY = -1000f;
    [SerializeField] private float fallDuration = 2f;  // �����A�j���[�V�����̍Đ�����
    [SerializeField] private float rotateSpeed = 360f; // ��]���x�i�x/�b�j
    [SerializeField] private int objectCount = 5;  // ��������I�u�W�F�N�g�̑���
    [SerializeField] private float spawnInterval = 0.5f;  // �����Ԋu
    [SerializeField] private float waitTimeAfterSpawn = 1f;  // ������̑ҋ@����

    [SerializeField] private Transform parentTransform; // �e�ɂ���I�u�W�F�N�g��Transform

    private void Start()
    {
        // �I�u�W�F�N�g�̐������J�n
        StartGeneratingImages();
    }

    // �I�u�W�F�N�g�̐������J�n����
    private void StartGeneratingImages()
    {
        // ��莞�Ԃ��ƂɃI�u�W�F�N�g�𐶐�����
        InvokeRepeating(nameof(GenerateMultipleImages), 0f, spawnInterval);
    }

    // ��x�ɕ����̉摜�𐶐����A�A�j���[�V������ݒ肷�郁�\�b�h
    private void GenerateMultipleImages()
    {
        // �R���[�`�����g���Đ����Ƒҋ@�����s
        StartCoroutine(GenerateAndAnimateImage());
    }

    // �摜�𐶐����A�A�j���[�V������ݒ肷��R���[�`��
    private IEnumerator GenerateAndAnimateImage()
    {
        // �����_���ȃC���f�b�N�X�z��𐶐�
        int[] randomIndices = GenerateRandomIndices(objectCount);

        // 1��̐����ŕ����̃I�u�W�F�N�g�𐶐�
        for (int i = 0; i < objectCount; i++)
        {
            // �����_���ȃC���f�b�N�X���g�p
            int randomIndex = randomIndices[i];

            // Image�̃C���X�^���X�𐶐�
            Image newImage = Instantiate(imagePrefab, parentTransform);

            RectTransform imageRectTransform = newImage.GetComponent<RectTransform>();

            // X���W�𓙊Ԋu�ɐݒ�i�����_���ȃC���f�b�N�X���g�p�j
            //float r = Random.Range(-50f, 50f);
            float spacing = (maxX - minX) / objectCount;  // ���Ԋu�̌v�Z
            float baseX = minX + (spacing * randomIndex);  // �����_���ȃC���f�b�N�X�ɂ��X���W

            imageRectTransform.anchoredPosition = new Vector2(baseX, startY); // Y���W�͐ݒ肵���l�Ƀ����_���ȃI�t�Z�b�g������������

            // ��莞�ԑҋ@
            yield return new WaitForSeconds(waitTimeAfterSpawn);

            // �t�F�[�h�A�E�g�̂��߂ɁA�����ȐF�ɃA�j���[�V����
            newImage.DOColor(new Color(imagePrefab.color.r, imagePrefab.color.g, imagePrefab.color.b, 0f), fallDuration)
                .SetEase(Ease.Linear);

            // �����Ɖ�]�̃A�j���[�V������DOTween�Ŏ��s
            AnimateFallingAndRotating(imageRectTransform);
        }
    }

    // DOTween���g���ė����Ɖ�]�̃A�j���[�V���������s���郁�\�b�h
    private void AnimateFallingAndRotating(RectTransform imageRectTransform)
    {
        // Y���W��ς��A���Ԃ���ɃA�j���[�V����
        imageRectTransform.DOAnchorPosY(imageRectTransform.anchoredPosition.y + endY, fallDuration)
            .SetEase(Ease.Linear)  // ���`�̃X���[�Y�ȃA�j���[�V����
            .OnComplete(() =>
            {
                Destroy(imageRectTransform.gameObject);  // �A�j���[�V����������ɍ폜
            });

        // ��]����]���x�ɉ�����fallDuration�b�ԂŃ��[�v������
        imageRectTransform.DORotate(new Vector3(0, 0, rotateSpeed * fallDuration), fallDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear);  // ���`�̃X���[�Y�ȉ�]
    }

    // �����_���ȃC���f�b�N�X�z��𐶐����郁�\�b�h
    private int[] GenerateRandomIndices(int count)
    {
        int[] indices = new int[count];

        // ���ԂɃC���f�b�N�X����
        for (int i = 0; i < count; i++)
        {
            indices[i] = i;
        }

        // �z����V���b�t��
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, count);
            int temp = indices[i];
            indices[i] = indices[randomIndex];
            indices[randomIndex] = temp;
        }

        return indices;
    }
}
