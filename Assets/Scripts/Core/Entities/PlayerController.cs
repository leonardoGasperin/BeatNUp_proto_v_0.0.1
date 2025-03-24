using UnityEngine;

namespace Core.Entities
{
    public sealed class PlayerController : MonoBehaviour
    {
        private Player player;

        private void Start()
        {
            player = gameObject.GetComponent<Player>();
        }

        private void FixedUpdate()
        {
            if (player == null || !player.isLive) return;

            if (Input.GetButton("Horizontal"))
            {
                player.MoveHorizontal((int)Input.GetAxisRaw("Horizontal"));
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
                player.TryAttack();
            }

            player.isBlocking = Input.GetButton("Fire2");
        }
    }
}
