using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceChangeScript : MonoBehaviour
{
    // �G�̃f�[�^�Ń}�e���A����ݒ肷�邩���A�䂭�䂭�̓A�j���[�V����������Ă���\�肾����
    [SerializeField] Material possessableMaterial;
    [SerializeField] Material deadMaterial;
    
    public void ChangeMaterialToPossessable(GameObject gameObject)// ���ڂ�\��Ԃ̐F�ւ̐ݒ�
    {
        gameObject.GetComponent<MeshRenderer>().material = possessableMaterial;
    }
    public void ChangeMaterialToDead(GameObject gameObject)// ���S��Ԃ̐F�ւ̐ݒ�
    {
        gameObject.GetComponent<MeshRenderer>().material = deadMaterial;
    }
}
