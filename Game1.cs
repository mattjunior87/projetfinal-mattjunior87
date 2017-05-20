using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ProjetFinalProg2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        /// <summary>
        /// champs
        /// </summary>
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Vaisseau hero;
        private Ennemi[] ennemi;
        private Projectiles[] projectile;
        private PowerUp[] powerUp;
        private int projectileVivant = 0;
        private static Rectangle fenetre;
        private bool collision;
        private int calculNombreEnnemi = -1;

        /// <summary>
        /// Champs que l'utilisateur peut définir leurs valeurs
        /// </summary>
        private int nombreEnnemi = 20;
        private int nombreProjectile = 15;
        private int nombrePowerUp = 10;




        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.ApplyChanges();

            fenetre = new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GameObject.Content = Content;
            GameObject.SpriteBatch = spriteBatch;
            
            ///instancier mon hero
            hero = new Vaisseau();

            ///instancier mon tableau ennemi ainsi que chacun de ses éléments
            ennemi = new Ennemi[nombreEnnemi];
            for (int i = 0; i < nombreEnnemi; i++)
            {
                ennemi[i] = new Ennemi();
            }

            ///instancier mon tableau projectile ainsi que chacun de ses éléments
            projectile = new Projectiles[nombreProjectile];
            for (int i = 0; i < nombreProjectile; i++)
            {
                projectile[i] = new Projectiles();
            }

            ///instancier mon tableau powerUp ainsi que chacun de ses éléments
            powerUp = new PowerUp[nombrePowerUp];
            for (int i = 0; i < nombrePowerUp; i++)
            {
                powerUp[i] = new PowerUp();
            }


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            ///J'appel mes méthodes Update de chacun de mes objects
            UpdateEnnemi();
            UpdateHero();
            UpdateMissile();
            UpdatePowerUp();

            ///J'appel ma méthode CollisionObject pour verifier si j'ai fait une colision
            CollisionObject();


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// Méthode qui permet de verifier si je fais des collision
        /// </summary>
        public void CollisionObject()
        {
            int nombreMissileAjouter;
            ///collision entre mes ennemis et mes projectiles
            for (int i = 0; i < nombreEnnemi; i++)
            {
                for (int y = 0; y < nombreProjectile; y++)
                {
                    ennemi[i].Collision(projectile[y]);
                }
            }

            ///collision entre mes ennemi et mon hero
            for (int i = 0; i < nombreEnnemi; i++)
            {
                collision = ennemi[i].Collision(hero);
                if (collision == true)
                {
                    Exit();
                }
            }

            ///collision entre mon hero et mes powerUps
            for (int i = 0; i < nombrePowerUp; i++)
            {
                collision = hero.Collision(powerUp[i]);
                if (collision == true)
                {

                    nombreMissileAjouter = powerUp[i].NombreProjectileAjouter;
                    calculNombreEnnemi = projectileVivant - nombreMissileAjouter;
                    if (calculNombreEnnemi > 0)
                    {
                        projectileVivant -= nombreMissileAjouter;
                    }
                    else
                    {
                        projectileVivant = 0;
                    }
                    for (int y = projectileVivant; y < nombreProjectile; y++)
                    {
                        projectile[y].Revivre();

                    }

                }
            }
        }

        /// <summary>
        /// Méthode qui permet de faire tous les updates des ennemis
        /// </summary>
        public void UpdateEnnemi()
        {
            for (int i = 0; i < nombreEnnemi; i++)
            {
                ennemi[i].UpdateEnnemiPosition(nombreEnnemi);
            }
        }

        /// <summary>
        /// Méthode qui permet de faire tous les updates des missiles
        /// </summary>
        public void UpdateMissile()
        {
            projectileVivant = hero.ProjectileVivant;
            if (projectileVivant < nombreProjectile)
            {
                projectile[projectileVivant].PositionProjectile(hero.LancerProjectiles(nombreProjectile, calculNombreEnnemi));
            }

            for (int i = 0; i < nombreProjectile; i++)
            {
                projectile[i].UpdateProjectilesPotition();
            }
        }

        /// <summary>
        /// Méthode qui permet de faire tous les updates du héro
        /// </summary>
        public void UpdateHero()
        {
            hero.Bouger();
            hero.ResterDansRectangle();
        }

        /// <summary>
        /// Méthode qui permet de faire tous les updates des powerUps
        /// </summary>
        public void UpdatePowerUp()
        {
            for (int i = 0; i < nombrePowerUp; i++)
            {
                powerUp[i].UpdatePowerUpPosition();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            ///Appel la méthode qui permet de déssiner le hero si il est vivant
            if (hero.EstVivant == true)
            {
                hero.Dessiner();
            }

            ///Appel la méthode qui permet de déssiner les ennemi s'ils sont vivants
            for (int i = 0; i < nombreEnnemi; i++)
            {
                if(ennemi[i].EstVivant == true)
                {
                    ennemi[i].Dessiner();
                }
            }

            ///Appel la méthode qui permet de déssiner les projectiles s'ils sont vivants
            for (int i = 0; i < nombreProjectile; i++)
            {
                if(projectile[i].EstVivant == true)
                {
                    projectile[i].Dessiner();
                }
            }

            ///Appel la méthode qui permet de déssiner les powerUp s'ils sont vivants
            for (int i = 0; i < nombrePowerUp; i++)
            {
                if (powerUp[i].EstVivant == true)
                {
                    powerUp[i].Dessiner();
                }
            }


            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        /// <summary>
        /// Permet d'avoir les dimention de l'écran dans mes autres classes en lecture seul
        /// </summary>
        public static Rectangle Fenetre { get => fenetre; }
    }
}
