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
                    throw new NotSupportedException("AssetManager not initialized!");
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
        public static void Initialize(Game game, Action onComplete, Action<int,int> onProgress)
        {
            Thread t = new Thread(() =>
            {
                var am = new AssetManager(game);
                am.LoadAssets(onProgress);
                onComplete?.Invoke();
            });
            t.Start();
        }

        public static void RegisterSpritesheet(string sheet)
        {
            loadActions.Add((AssetManager am) =>
            {
                am.Spritesheets.Add(sheet, am.game.Content.Load<Texture2D>("Spritesheets/"+sheet));
            });
        }

        #endregion

        public Dictionary<string, Texture2D> Spritesheets { get; private set; } = new Dictionary<string, Texture2D>();

        private Game game;

        private AssetManager(Game game)
        {
            this.game = game;

            RegisterSpritesheet("TileSheetDungeon");
            RegisterSpritesheet("TileSheetIndoor");
            RegisterSpritesheet("TileSheetOutdoor");
        }

        /// <summary>
        /// Loads all assets
        /// </summary>
        /// <param name="onProgress">Action to be called for progress notifications</param>
        private void LoadAssets(Action<int, int> onProgress)
        {
           for(int i = 0; i < loadActions.Count; i++)
            {
                loadActions[i](this);
                onProgress(i, loadActions.Count);
                Thread.Sleep(500);
            }
            Thread.Sleep(200);
        }
    }
}
