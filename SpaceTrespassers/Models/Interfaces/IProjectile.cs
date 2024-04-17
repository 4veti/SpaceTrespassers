namespace SpaceTrespassers.Models.Interfaces;

public interface IProjectile
{
    public int Damage { get; }
    public void Move();
}