using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindDebuff : BaseEffect
{
    [SerializeField] private ParticleSystem _visualEffectPref;

    private GameObject _target;
    private float _duration;
    private ParticleSystem _visualEffect;
    private Coroutine _effectJob;

    public void Init(GameObject target, float dutarion)
    {
        Type = EffectType.Debuff;
        _target = target;
        _duration = dutarion;
        _effectJob = StartCoroutine(EffectCoroutine());
    }

    private void VisualEffect() // ���������� ������ ��������� �� �����
    {
        _visualEffect = Instantiate(_visualEffectPref, transform); // ���������� ������ ��������� �� �����
        ParticleSystem.MainModule main = _visualEffect.main;
        main.duration = _duration;
        _visualEffect.Play();
    }

    private IEnumerator EffectCoroutine()
    {
        VisualEffect(); // ���������� ������ ��������� �� �����

        float time = 0;
        while(time < _duration)
        {
            // ������ �� ���������� ������ ���

            time += Time.deltaTime;
            yield return null;
        }
        _visualEffect.transform.parent = null;
        Destroy(gameObject);
    }
}
