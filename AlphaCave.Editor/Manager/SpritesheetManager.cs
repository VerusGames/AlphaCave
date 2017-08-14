using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.Editor.Manager
{
    public class SpritesheetManager
    {
        #region Singleton

        public static SpritesheetManager Instance
        {
            get
            {
                if (spm == null)
                    spm = new SpritesheetManager();

                return spm;
            }
        }

        private static SpritesheetManager spm;

        #endregion

        public string FolderPath { get; private set; } = "Spritesheets";

        public Dictionary<string, Bitmap> Spritesheets { get; private set; } = new Dictionary<string, Bitmap>();

        private SpritesheetManager()
        {
            LoadSpritesheets();
        }

        private void LoadSpritesheets()
        {
            foreach (var file in Directory.EnumerateFiles(FolderPath, "*.png"))
            {
                var sheet = (Bitmap)Image.FromFile(file);
                var sheetName = Path.GetFileNameWithoutExtension(file);
                Spritesheets.Add(sheetName, sheet);  
            }
        }

        public void ChangeSpritesheetPath(string path)
        {
            FolderPath = path;
            Spritesheets.Clear();
            LoadSpritesheets();
        }
    }
}
