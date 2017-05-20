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
    /// Class qui fait de l'héritage avec GameObject pour créer donner des munitions à l'object de la classe Vaisseau
    /// </summary>
    class PowerUp : GameObject
    {
        /// <summary>
        /// Champs qui permet de definir l'image du powerUp
        /// </summary>
        private string image1 = "media/powerUp";

        /// <summary>
        /// Champ qui permet de déterminer le nombre de projectile qui sera ajouter lors de la colision entre le powerUp et l'object de la class Vaisseau
        /// </summary>
        private int nombreProjectileAjouter;

        /// <summary>
        /// Constructeur des powerUp
        /// </summary>
        /// <param name="content"></param>
        public PowerUp()
        {
            estVivant = true;
            position.X = alea.Next(0, Game1.Fenetre.Right - 50);
            position.Y = alea.Next(-200, -100);
            direction.Y = alea.Next(5,10);
            sprite = Content.Load<Texture2D>(image1);
            nombreProjectileAjouter = alea.Next(1, 4);
        }

        /// <summary>
        /// permet de faire bouger les powerUp
        /// </summary>
        public void UpdatePowerUpPosition()
        {
            if (position.Y > Game1.Fenetre.Bottom)
            {
                position.Y = -50;
                position.X = alea.Next(0, Game1.Fenetre.Right - 50);
            }
            position.Y += (int)direction.Y;
        }

        /// <summary>
        /// Permet au autre class de lire le nombre de projectile que le projectile ajoute lors de son contact avec un Vaisseau
        /// </summary>
        public int NombreProjectileAjouter { get => nombreProjectileAjouter;}
    }
}
