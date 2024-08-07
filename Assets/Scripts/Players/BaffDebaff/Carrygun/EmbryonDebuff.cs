using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EmbryonDebuff : MonoBehaviour
{
    [SerializeField] private GameObject scraderPrefab;

    private GameObject _target;
    [HideInInspector] public GameObject Player;

    private float _duration = 3f;
    private float _damage = 3f;
    private float _finalDamage = 9f;
    private float _damageReduction = 0f;

    void Start()
    {
        _target = transform.parent.gameObject;
        _target.GetComponent<HealthComponent>().MakeMagicDamageEvent += DamageReduction;
        _target.GetComponent<HealthComponent>().MakePhisicDamageEvent += DamageReduction;

        StartCoroutine(TakeDamageForTarget());
    }

    private void DamageReduction(HealthComponent.DamageInfo damageInfo)
    {
        damageInfo.ModifiedDamage *= _damageReduction;
    }

    private IEnumerator TakeDamageForTarget()
    {
        for (int i = 0; i < _duration; i++)
        {
            _target.GetComponent<HealthComponent>().TryTakeDamage(_damage, DamageType.Physical, AttackRangeType.Inner);
            _damageReduction = Mathf.Min(_damageReduction + 0.1f, 0.3f);
            yield return new WaitForSeconds(1f);
        }
        _target.GetComponent<HealthComponent>().TryTakeDamage(_finalDamage, DamageType.Physical, AttackRangeType.Inner);

        GameObject newScrader = Instantiate(scraderPrefab);

        Vector3 directionOfMovement = -(_target.GetComponent<MoveComponent>().MoveDirection).normalized;
        float distance = 1.94f;
        Vector3 oppositePosition = _target.transform.position + directionOfMovement * distance;
        newScrader.transform.position = oppositePosition;

        newScrader.GetComponent<Scrader>().Player = Player;

        for (int i = 0; i < _duration; i++)
        {
            _damageReduction = Mathf.Min(_damageReduction - 0.1f, 0f);
            yield return new WaitForSeconds(1f);
        }
        _damageReduction = 0f;

        Destroy(gameObject);
    }

    private void OnDisable()
    {
        _target.GetComponent<HealthComponent>().MakeMagicDamageEvent -= DamageReduction;
        _target.GetComponent<HealthComponent>().MakePhisicDamageEvent -= DamageReduction;
    }
}
