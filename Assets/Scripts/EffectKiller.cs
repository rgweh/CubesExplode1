using System.Collections;
using UnityEngine;

public class EffectKiller : MonoBehaviour
{
    [SerializeField] private float _aliveTime;

    private void OnEnable()
    {
        StartCoroutine(WaitAndKillEffect());
    }

    private IEnumerator WaitAndKillEffect()
    {
        WaitForSeconds wait = new WaitForSeconds(_aliveTime);

        yield return wait;

        Destroy(gameObject);

        yield break;
    }
}
