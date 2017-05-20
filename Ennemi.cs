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
    class Ennemi : GameObject
    {
        /// <summary>
        /// Champs qui déterme les images des ennemis
        /// </summary>
        private string image1 = "media/ennemi 1";
        private string image2 = "media/ennemi 2";
        private string image3 = "media/ennemi 3";
        private string image4 = "media/ennemi 4";

        /// <summary>
        /// nombre aleatoire qui détermine l'image de l'Ennemi
        /// </summary>
        private static Texture2D[] image = new Texture2D[4];

        /// <summary>
        /// Constructeur des Ennemis
        /// </summary>
        /// <param name="content"></param>
        public Ennemi()
        {
            image[0] = Content.Load<Texture2D>(image1);
            image[1] = Content.Load<Texture2D>(image2);
            image[2] = Content.Load<Texture2D>(image3);
            image[3] = Content.Load<Texture2D>(image4);

            estVivant = true;
            position.X = alea.Next(0,Game1.Fenetre.Right-46);
            position.Y = alea.Next(-250,-200);
            direction.Y = alea.Next(4, 15);
            sprite = image[alea.Next(0, 4)];
        }

        /// <summary>
        /// Méthode qui permet de faire bouger mon Ennemi
        /// </summary>
        /// <param name="nombreEnnemi"></param>
        public void UpdateEnnemiPosition(int nombreEnnemi)
        {
            if (position.Y > Game1.Fenetre.Bottom)
            {
                position.Y = -50;
                position.X = alea.Next(0, Game1.Fenetre.Right - 46);
            }
            position.Y += (int)direction.Y;
        }
    }
}
