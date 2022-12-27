using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BaseSkill : MonoBehaviour, ISkill
{
	[SerializeField]
	protected Sprite _icon;
	[SerializeField]
	protected LayerMask _targetLayer;

	[Header("State effects")]
	[SerializeField]
	[SerializeInterface(typeof(IStateEffect))]
	private List<Object> effectsPrefab;
	protected List<IStateEffect> _effects => effectsPrefab.OfType<IStateEffect>().ToList();

	[Header("Visual effects")]
	[SerializeField]
	protected ParticleSystem _particleAttackEffectPrefab;

	protected ParticleSystem _particleAttackEffect;
	public UnityEvent OnSkillFinished { get; } = new UnityEvent();
	public Sprite IconSprite { get => _icon; }
	public LayerMask TargetLayer { get => _targetLayer; }

	private void Start()
	{
		if (_particleAttackEffectPrefab)
		{
			_particleAttackEffect = Instantiate(_particleAttackEffectPrefab, this.transform.position, this.transform.rotation, this.transform);
			_particleAttackEffect.Stop();
		}
	}

	public virtual void BreakSkill()
	{
		StopAllCoroutines();
	}

	public virtual void StartSkill(ICharacter target) { }
}