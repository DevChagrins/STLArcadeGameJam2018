using UnityEngine;

namespace Chagrins
{
    public class BaseBehaviour : MonoBehaviour
    {
        private Animator _animator;
        private AudioSource _audio;
        private Camera _camera;
        private Collider _collider;
        private Collider2D _collider2D;
        private BoxCollider2D _boxCollider2D;
        private GameObject _gameObject;
        private Light _light;
        private ParticleSystem _particleSystem;
        private RectTransform _rectTransform;
        private Renderer _renderer;
        private Rigidbody _rigidBody;
        private Rigidbody2D _rigidBody2D;
        private Sprite _sprite;
        private SpriteRenderer _spriteRenderer;
        private Transform _transform;

        protected virtual bool LocateComponentsInChildren => false;

        public Animator animator => _animator != null ? _animator : (_animator = LocateComponent<Animator>());
        public RectTransform rectTransform => _rectTransform != null ? _rectTransform : (_rectTransform = LocateComponent<RectTransform>());
        public Rigidbody rigidBody => _rigidBody != null ? _rigidBody : (_rigidBody = LocateComponent<Rigidbody>());
        public Rigidbody2D rigidBody2D => _rigidBody2D != null ? _rigidBody2D : (_rigidBody2D = LocateComponent<Rigidbody2D>());
        public SpriteRenderer spriteRenderer => _spriteRenderer != null ? _spriteRenderer : (_spriteRenderer = LocateComponent<SpriteRenderer>());
        public BoxCollider2D boxCollider2D => _boxCollider2D != null ? _boxCollider2D : (_boxCollider2D = LocateComponent<BoxCollider2D>());
        public new AudioSource audio => _audio != null ? _audio : (_audio = LocateComponent<AudioSource>());
        public new Camera camera => _camera != null ? _camera : (_camera = LocateComponent<Camera>());
        public new Collider collider => _collider != null ? _collider : (_collider = LocateComponent<Collider>());
        public new Collider2D collider2D => _collider2D != null ? _collider2D : (_collider2D = LocateComponent<Collider2D>());
        public new GameObject gameObject => _gameObject != null ? _gameObject : (_gameObject = base.gameObject);
        public new Light light => _light != null ? _light : (_light = LocateComponent<Light>());
        public new ParticleSystem particleSystem => _particleSystem != null ? _particleSystem : (_particleSystem = LocateComponent<ParticleSystem>());
        public new Renderer renderer => _renderer != null ? _renderer : (_renderer = LocateComponent<Renderer>());
        public new Transform transform => _transform != null ? _transform : (_transform = base.transform);

        private T LocateComponent<T>() where T : Component
        {
            T component = GetComponent<T>();
            if (component == null && LocateComponentsInChildren) component = GetComponentInChildren<T>();
            return component;
        }
    }
}