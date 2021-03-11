using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShooting : MonoBehaviour
{
    public float maxShootingTimer = 0.8f;
    private float shootingTimer = 0;    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shootingTimer > 0) shootingTimer -= Time.fixedDeltaTime;
        if (shootingTimer <= 0) Launch();
    }

    private void Launch()
    {
        GameObject proj = Instantiate(Resources.Load("EnemySpell"),
        transform.position + Vector3.right * 3,
        Quaternion.identity
        ) as GameObject;
        proj.GetComponent<EnemyProjectile>().SetDirection(Vector2.right);
        shootingTimer = maxShootingTimer;
    }
}
