using SpaceTrespassers.Models.Interfaces;
using SpaceTrespassers.Models.Projectiles;

namespace SpaceTrespassers.Models.Enemies
{
    public class MediumEnemy : Enemy, IShootableEnemy
    {
        private const int DefaultHealth = 2;
        private const int DefaultHoverSpeed = 40;

        public MediumEnemy(int leftPosition, int topPosition)
            : base(DefaultHealth, DefaultHoverSpeed)
        {
            Image = Properties.Resources.TrespasserMedium;
            SizeMode = PictureBoxSizeMode.AutoSize;
            Left = leftPosition;
            Top = topPosition;
        }

        public void FireAttack()
        {
            Control projectile = new EnemyProjectile();
            projectile.Top = this.Top + 20;
            projectile.Left = this.Left + 32;
            Form1.ActiveForm.Controls.Add(projectile);
        }

        public override void TakeDamage(int damage = 1)
        {
            base.TakeDamage(damage);

            Image = Properties.Resources.TrespasserMediumDamaged;
            SizeMode = PictureBoxSizeMode.AutoSize;
        }
    }
}
