using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    private float maxMana = 3f;
    private float currentMana;

    private void Start()
    {
        currentMana = maxMana;
        StartCoroutine(RegenMana());
    }

    public float GetManaRatio()
    {
        return (float)currentMana / (float)maxMana;
    }

    public bool TryUseMana(float amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            return true;
        }

        return false;
    }

    private IEnumerator RegenMana()
    {
        while (true)
        {
            if (currentMana < maxMana)
            {
                currentMana += 1f;
                currentMana = Mathf.Min(currentMana, maxMana);
            }
            yield return new WaitForSeconds(7f);
        }
    }
}
