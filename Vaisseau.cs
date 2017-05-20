using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ProjetFinalProg2
{
    /// <summary>
    /// Classe fille qui définit le héro
    /// </summary>
    class Vaisseau : GameObject
    {
        /// <summary>
        /// Champs
        /// </summary>
        private string image = "media/héro";
        private int compteur = 0;
        private int projectileVivant = 0;
        


        /// <summary>
        /// Constructeur des vaisseau
        /// </summary>
        /// <param name="content"></param>
        public Vaisseau()
        {
            estVivant = true;
            position.X = Game1.Fenetre.Right/2-50l;
            position.Y = Game1.Fenetre.Bottom-100;
            vitesse.X = 10;
            sprite = Content.Load<Texture2D>(image);

        }

        /// <summary>
        /// Permet de controler le vaisseau avec les touches W,A,S,D
        /// W = Avancer
        /// A = Aller vers la gauche
        /// S = Reculer
        /// D = Aller vers la droite
        /// </summary>
        public void Bouger()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.X += 7;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.X -= 7;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.Y -= 7;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y += 7;
            }

        }

        /// <summary>
        /// Vérifier si le vaisseau est dans l'écran
        /// Si le vaisseau essaye d'aller hors de l'écran, il est replacer dans l'écran
        /// </summary>
        public void ResterDansRectangle()
        {
            if (position.X < Game1.Fenetre.Left)
            {
                position.X = Game1.Fenetre.Left;
            }
            if (position.X > Game1.Fenetre.Right - 150)
            {                
                position.X = Game1.Fenetre.Right - 150;
            }                
            if (position.Y < Game1.Fenetre.Top)
            {                
                position.Y = Game1.Fenetre.Top;
            }                
            if (position.Y > Game1.Fenetre.Bottom - 150)
            {               
                position.Y = Game1.Fenetre.Bottom - 150;
            }
        }

        /// <summary>
        /// Permet de lancer des projectiles à la position de vaisseau
        /// Space = Lancer projectile
        /// </summary>
        /// <param name="nombreProjectile">Le champ qui spécifie combien il y a de projectile</param
        /// <param name="updateNombreProjectile">Le champ qui spécifie le nombre de projectile lors d'un contact entre un powerUp et un vaisseau</param>
        /// <returns></returns>
        public Vector2 LancerProjectiles(int nombreProjectile, int updateNombreProjectile)
        {
            Vector2 positionProjectile;
            if (updateNombreProjectile > -1)
            {
                projectileVivant = updateNombreProjectile;
            }
            positionProjectile.X = -100;
            positionProjectile.Y = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (ProjectileVivant < nombreProjectile)
                {
                    if (compteur == 0)
                    {
                        positionProjectile.X = position.X + 35;
                        positionProjectile.Y = position.Y;
                        projectileVivant++;
                        compteur = 1;
                        
                    }
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                compteur = 0;
            }
            return positionProjectile;
        }

        /// <summary>
        /// Propriété qui permet de lire le champs projectile Vivant
        /// </summary>
        public int ProjectileVivant { get => projectileVivant; }
    }
}
