using SpaceTrespassers.Models.Interfaces;

namespace SpaceTrespassers.Models.Projectiles
{
    public class EnemyProjectile : PictureBox, IProjectile
    {
        private const int Speed = 20;

        public EnemyProjectile()
        {
            Image = Properties.Resources.enemyProjectile1;
            SizeMode = PictureBoxSizeMode.AutoSize;
        }

        public int Damage => 1;

        public new void Move()
        {
            if (Top >= Form.ActiveForm.Height)
            {
                Dispose();
            }

            Top += Speed;
        }
    }
}
