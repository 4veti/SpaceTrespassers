using SpaceTrespassers.Models.Interfaces;
using SpaceTrespassers.Models.Projectiles;
using SpaceTrespassers.Utils;

namespace SpaceTrespassers.Models
{
    public class SpaceShip : PictureBox
    {
        private Missle missle;

        public SpaceShip()
        {
            this.Image = Properties.Resources.basicShip;
            this.SizeMode = PictureBoxSizeMode.AutoSize;
            this.Left = (this.Width / 2) - this.Width / 2;
            this.Top = this.Height - 150;
        }

        public void MoveRight()
        {
            this.Left += Constants.PlayerMoveSpeed;
            if (missle != null && GameState.armedMissle)
            {
                missle.Left += Constants.PlayerMoveSpeed;
            }
        }

        public void MoveLeft()
        {
            this.Left -= Constants.PlayerMoveSpeed;
            if (missle != null && GameState.armedMissle)
            {
                missle.Left -= Constants.PlayerMoveSpeed;
            }
        }

        public void Fire()
        {
            if (GameState.armedMissle)
            {
                GameState.armedMissle = false;
                missle.FireMissle();
            }
            else
            {
                Laser projectile = new Laser();
                projectile.Top = this.Top - 20;
                projectile.Left = this.Left + 32;
                Form1.ActiveForm.Controls.Add(projectile);
            }
        }

        public void ArmMissle()
        {
            GameState.armedMissle = true;
            missle = new Missle();
            missle.Top = this.Top - 25;
            missle.Left = this.Left + 27;
            missle.ReadyMissle();
            Form1.ActiveForm.Controls.Add(missle);
        }
    }
}
