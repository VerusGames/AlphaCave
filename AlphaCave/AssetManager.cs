using AlphaCave.Graphics;
using engenious;
using engenious.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlphaCave
{
    public class AssetManager
    {
        #region Semi-Singleton

        public static AssetManager Instance
        {
            get
            {
                if (instance == null)
                    throw new Exception("AssetManager not initialized");
                return instance;
            }
            private set
            {
                if (instance != value)
                    instance = value;
            }
        }

        private static AssetManager instance;

        private static List<Action<AssetManager>> loadActions = new List<Action<AssetManager>>();

        /// <summary>
        /// Initializes the AssetManager
        /// </summary>
        /// <param name="game">The game</param>
        /// <param name="onComplete">Action to be called on complete</param>
        /// <param name="onProgress">Action to be called on progress</param>
        public static void Initialize(Game game, Action onComplete, Action<int, int> onProgress)
        {
            //Thread t = new Thread(() =>
            //{
                var am = new AssetManager(game);
                am.LoadAssets(onProgress);
                instance = am;
                onComplete?.Invoke();
            //});
            //t.Start();
        }

        public static void RegisterSpritesheet(string sheet, int tileWidth = 16, int tileHeight = 16)
        {
            loadActions.Add((AssetManager am) =>
            {
                am.Spritesheets.Add(sheet, new Spritesheet(am.game.GraphicsDevice, am.game.Content, "Spritesheets/" + sheet, tileWidth, tileHeight));
            });
        }

        #endregion

        public Dictionary<string, Spritesheet> Spritesheets { get; private set; } = new Dictionary<string, Spritesheet>();

        private Game game;

        private AssetManager(Game game)
        {
            this.game = game;

            RegisterSpritesheet("TileSheetDungeon");
            RegisterSpritesheet("TileSheetCharacter");
            //RegisterSpritesheet("TileSheetIndoor", 486);
            //RegisterSpritesheet("TileSheetOutdoor", 1767);
        }

        /// <summary>
        /// Loads all assets
        /// </summary>
        /// <param name="onProgress">Action to be called for progress notifications</param>
        private void LoadAssets(Action<int, int> onProgress)
        {
            for (int i = 0; i < loadActions.Count; i++)
            {
                loadActions[i](this);
                onProgress(i + 1, loadActions.Count);
                //Thread.Sleep(500);
            }
            Thread.Sleep(200);
        }
    }
}
