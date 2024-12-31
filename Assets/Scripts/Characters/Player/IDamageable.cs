namespace Characters.Player
{
    public interface IDamageable
    {
        public float life { get; }
        public void Hit(float attackForce);
    }
}
