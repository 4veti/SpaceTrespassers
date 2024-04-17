using SpaceTrespassers.Models.Interfaces;

namespace SpaceTrespassers.Models.Projectiles
{
    public class Missle : PictureBox, IProjectile
    {
        private bool fired;
        private bool primed;
        private int moveIncrement;

        public int Damage => 5;

        public Missle()
        {
            this.Image = Properties.Resources.Missle;
            this.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        public new void Move()
        {
            if (!primed && moveIncrement < 60)
            {
                moveIncrement += 2;
            }

            if (fired)
            {
                this.Top -= moveIncrement;
            }
            else if (!primed)
            {
                this.Top += moveIncrement;
            }
        }

        public void ReadyMissle()
        {
            moveIncrement = 0;
            primed = true;
        }

        public void FireMissle()
        {
            fired = true;
            primed = false;
            this.Image = Properties.Resources.MissleFired_1;
        }
    }
}
