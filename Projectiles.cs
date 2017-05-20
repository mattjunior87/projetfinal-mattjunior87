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
    /// Class qui hérite de GameObject et qui permet de creer des objects Projectiles
    /// </summary>
    class Projectiles : GameObject
    {
        /// <summary>
        /// Champs qui définit les images de chaque projectiles
        /// </summary>
        private string image1 = "media/projectile 1";
        private string image2 = "media/projectile 2";
        
        /// <summary>
        /// Champs qui permet de déterminer les projectiles différents en leur attribuant un chiffre
        /// </summary>
        private int typeProjectile;

        /// <summary>
        /// Constructeur de projectiles
        /// </summary>
        /// <param name="content"></param>
        public Projectiles()
        {
            typeProjectile = alea.Next(0, 2);
            estVivant = true;
            position.X = -100;
            position.Y = 0;

            if (typeProjectile == 0)
            {
                sprite = Content.Load<Texture2D>(image1);
                direction.Y = 15;
            }
            if (typeProjectile == 1)
            {
                sprite = Content.Load<Texture2D>(image2);
                direction.Y = 25;
            }
        }

        /// <summary>
        /// permet de definir une nouvelle position au projectile
        /// </summary>
        /// <param name="positionProjectile">Nouvelle position qui veut etre attribuée au Projectile</param>
        public void PositionProjectile(Vector2 positionProjectile)
        {
            position = positionProjectile;
        }

        /// <summary>
        /// Permet de faire bouger le projectile
        /// </summary>
        public void UpdateProjectilesPotition()
        {
            position.Y -= direction.Y;
        }

        /// <summary>
        /// Permet au projectile de revivre
        /// </summary>
        public void Revivre()
        {
            estVivant = true;
        }


    }
}
