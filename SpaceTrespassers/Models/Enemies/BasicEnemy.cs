using SpaceTrespassers.Models.Interfaces;
using SpaceTrespassers.Models.Projectiles;
using SpaceTrespassers.Properties;

namespace SpaceTrespassers.Models.Enemies
{
    public class BasicEnemy : Enemy, IShootableEnemy
    {
        private const int DefaultHealth = 1;
        private const int DefaultHoverSpeed = 40;

        public BasicEnemy(int leftPosition, int topPosition)
            : base(DefaultHealth, DefaultHoverSpeed)
        {
            Image = Resources.Trespasser;
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
    }
}
