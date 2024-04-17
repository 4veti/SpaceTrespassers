using SpaceTrespassers.Models.Interfaces;
using SpaceTrespassers.Models.Projectiles;

namespace SpaceTrespassers.Models.Enemies
{
    public class ArmouredEnemy : Enemy, IShootableEnemy
    {
        private const int DefaultHealth = 5;
        private const int DefaultHoverSpeed = 40;

        public ArmouredEnemy(int leftPosition, int topPosition)
            : base(DefaultHealth, DefaultHoverSpeed)
        {
            Image = Properties.Resources.TrespasserArmoured;
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
