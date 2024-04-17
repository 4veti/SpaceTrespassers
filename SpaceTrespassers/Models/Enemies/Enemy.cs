using SpaceTrespassers.Models.Interfaces;

namespace SpaceTrespassers.Models.Enemies
{
    public abstract class Enemy : PictureBox, IEnemy
    {
        private int hoverDistance;

        private int health;
        private bool hovered;

        public Enemy(int hp, int hoverDistance)
        {
            health = hp;
            this.hoverDistance = hoverDistance;
        }

        public void Hover()
        {
            if (hovered)
            {
                Left += hoverDistance;
            }
            else
            {
                Left -= hoverDistance;
            }

            hovered = !hovered;
        }

        public virtual void TakeDamage(int damage = 1)
        {
            health -= damage;

            if (health <= 0)
            {
                Dispose();
            }
        }
    }
}
