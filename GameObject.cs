using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace ProjetFinalProg2
{
    /// <summary>
    /// Classe mère de tout les object de ce jeux
    /// </summary>
    abstract class GameObject
    {
        /// <summary>
        /// champ commun des classes filles que eux seulles peut définir et lire
        /// </summary>
        protected Vector2 position;
        protected Vector2 vitesse;
        protected Vector2 direction;
        protected Texture2D sprite;
        protected bool estVivant;
        static protected Random alea = new Random();
        private Rectangle rectCollision;
        private static ContentManager content;
        private static SpriteBatch spriteBatch;

        /// <summary>
        /// Méthode qui permet de dessiner les classes filles si elle est appelé
        /// </summary>
        /// <param name="spriteBatch">champ qui permet d'ajouter le sprite à un lot de sprite pour qu'il soit ensuite exécuté</param>
        public void Dessiner()
        {
            spriteBatch.Draw(sprite, Position, Color.White);
           
        }

        /// <summary>
        /// Permet de vérifier si deux objects ont fait une colision
        /// Il est important de mettre l'object de classe powerUp en paramètre
        /// Retourne un bool qui permet de savoir si la colision a eu lieu
        /// </summary>
        /// <param name="object2">le deuxième gameObject qui pourrait rentre en colision avec le gameObject qui est utiliser pour appeler cette méthode</param>
        /// <returns></returns>
        public bool Collision(GameObject object2)
        {
            bool collision = false;

            ///vérifie si le deuxième gameObject qui est pris en paramètre est de classe powerUp
            if (object2 is PowerUp)
            {
                if (this.estVivant == true && object2.estVivant == true)
                {
                    if (this.RectCollision.Intersects(object2.RectCollision))
                    {
                        object2.estVivant = false;
                        collision = true;
                    }
                }
            }

            ///pour toute les autres conditions
            else
            {
                if (this.estVivant == true && object2.estVivant == true)
                {
                    if (this.RectCollision.Intersects(object2.RectCollision))
                    {
                        this.estVivant = false;
                        object2.estVivant = false;
                        collision = true;
                    }
                }
            }
            return collision;
        }

        /// <summary>
        /// Propriété qui va chercher la largeur et hauteur de l'object ainsi que sa position en x et en y
        /// Avec ses informations, un rectangle est créer pour pour plus détecter une collision
        /// </summary>
        public Rectangle RectCollision
        {
            get
            {
                rectCollision.X = (int)this.Position.X;
                rectCollision.Y = (int)this.Position.Y;
                rectCollision.Width = this.sprite.Width;
                rectCollision.Height = this.sprite.Height;
                return rectCollision;
            }
        }

        /// <summary>
        /// Propriété qui rend la position en lecture seul
        /// </summary>
        public Vector2 Position { get => position;}

        /// <summary>
        /// Propriété qui permet de savoir si l'object en vivant ou est mort sans pouvoir la changer
        /// </summary>
        public bool EstVivant { get => estVivant;}
        public static ContentManager Content { get => content; set => content = value; }
        public static SpriteBatch SpriteBatch { get => spriteBatch; set => spriteBatch = value; }
    }
}
