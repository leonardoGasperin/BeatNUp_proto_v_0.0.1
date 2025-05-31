using UnityEngine;

namespace Core.Entities
{
    public sealed class PlayerController : MonoBehaviour
    {
        private Player player;
        private int direction = 0;

        private void Start()
        {
            player = gameObject.GetComponent<Player>();
        }

        private void FixedUpdate()
        {
            if (player == null || !player.isLive) return;
            if (Input.GetButton("Horizontal"))
            {
                direction = (int)Input.GetAxisRaw("Horizontal");
                player.MoveHorizontal(direction);
            }
        }

        private void Update()
        {
            if (player == null || !player.isLive) return;

            if (Input.GetButtonDown("Jump"))
            {
                player.TryJump();
            }

            if (Input.GetButtonDown("Fire1"))
            {
                player.TryAttack(direction);
            }

            player.isBlocking = Input.GetButton("Fire2");
        }
    }
}
