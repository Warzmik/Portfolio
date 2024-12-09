namespace Characters.Enemy 
{
    public interface IEnemyState
    {
        public string stateName { get; }
        public bool isActive { get; }

        public void ActivateState();
        public void DesactivateState();
    }
}
