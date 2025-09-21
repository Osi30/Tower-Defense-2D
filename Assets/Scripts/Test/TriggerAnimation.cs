using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField]
    private bool isTrigger = false;

    [SerializeField]
    private EnemyPool _enemyPool;

    private async void Update()
    {
        if (isTrigger)
        {
            isTrigger = false;
            EnemyControl enemy = await _enemyPool.GetOneEnemy();
            enemy.InitializeEnemy();
            

            Debug.Log("Trigger");
        }
    }
}
