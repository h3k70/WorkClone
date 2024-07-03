using UnityEngine;

namespace Players.Abilities.Genjalf.Fireworks_Ability
{
    public class CircleRendererAtMovingMouseTarget : MonoBehaviour
    {
        [SerializeField] private float _radius = 1.0f; // ������, �� ������� ����� ��������� ������, �������� �� ���������� ����� �� ������.
        [SerializeField] private Transform _player; // �����

        public Transform player
        {
            get => _player;
            set => _player = value;
        }

        public float radius
        {
            get => _radius;
            set => _radius = value;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(player.position, radius);
        }
    }
}