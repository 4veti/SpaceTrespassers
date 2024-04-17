using SpaceTrespassers.Models.Interfaces;

namespace SpaceTrespassers.Models.Projectiles
{
    public class Laser : PictureBox, IProjectile
    {
        private const int Speed = 30;

        public Laser()
        {
            Image = Properties.Resources.laser;
            SizeMode = PictureBoxSizeMode.AutoSize;
        }

        public int Damage => 1;

        public new void Move()
        {
            if (Top <= 0)
            {
                Dispose();
            }

            Top -= Speed;
        }
    }
}
