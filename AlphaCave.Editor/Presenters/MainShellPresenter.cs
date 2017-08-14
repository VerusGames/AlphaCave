using AlphaCave.Editor.Forms;
using AlphaCave.Editor.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCave.Editor.Presenters
{
    public class MainShellPresenter
    {
        private IMainShell shell;

        public MainShellPresenter(IMainShell shell)
        {
            this.shell = shell;
            shell.OnObjectSelected += (o) => shell.ShowEditor(o);
            shell.NewFileClicked += Shell_NewFileClicked;
            shell.OpenFileClicked += Shell_OpenFileClicked;
            shell.SaveFileClicked += Shell_SaveFileClicked;
            shell.SaveFileAsClicked += Shell_SaveFileAsClicked;
            shell.OpenFolderClicked += Shell_OpenFolderClicked;
            shell.ExitClicked += Shell_ExitClicked;
            shell.ExportClick += Shell_ExportClick;
        }

        private void Shell_ExportClick(IEditorObject selectedObject)
        {
            var filename = shell.ShowFileSave();

            if (String.IsNullOrEmpty(filename))
                return;

            selectedObject.Export(filename);
        }

        private void Shell_OpenFolderClicked(object sender)
        {
            throw new NotImplementedException();
        }

        private void Shell_ExitClicked(object sender)
        {
            shell.Close();
        }

        private void Shell_SaveFileAsClicked(Objects.IEditorObject selectedObject)
        {
            var filename = shell.ShowFileSave(selectedObject.Name);

            if (String.IsNullOrEmpty(filename))
                return;

            InternalSaveFile(filename, selectedObject);
        }

        private void Shell_SaveFileClicked(Objects.IEditorObject selectedObject)
        {
            if(string.IsNullOrEmpty(selectedObject.FilePath))
            {
                Shell_SaveFileAsClicked(selectedObject);
                return;
            }

            InternalSaveFile(selectedObject.FilePath, selectedObject);
        }

        private void InternalSaveFile(string path, IEditorObject selectedObject)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                selectedObject.Serialize(new BinaryWriter(fs));
                selectedObject.FilePath = path;
            }

            try
            {
                
            }
            catch (UnauthorizedAccessException)
            {
                shell.ShowMessage("Unauthorized access!", "You do not have the permission to save to this location.", MessageType.Error);
            }
            catch (System.Security.SecurityException)
            {
                shell.ShowMessage("Unauthorized access!", "You do not have the permission to save to this location.", MessageType.Error);
            }
            catch (Exception)
            {
                shell.ShowMessage("Error!", "Could not save file.", MessageType.Error);
            }
        }

        private void Shell_OpenFileClicked(object sender)
        {
            var filenames = shell.ShowFileOpen();

            if (filenames.Length == 0)
                return;

            var filename = filenames[0];

            if (String.IsNullOrEmpty(filename))
                return;

            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                var obj = EditorObject.Deserialize(new BinaryReader(fs));
                obj.FilePath = filename;
                shell.OpenObjects.Add(obj);
            }

            try
            {
                
            }
            catch (UnauthorizedAccessException)
            {
                shell.ShowMessage("Unauthorized access!", "You do not have the permission to open this file.", MessageType.Error);
            }
            catch (System.Security.SecurityException)
            {
                shell.ShowMessage("Unauthorized access!", "You do not have the permission to open this file.", MessageType.Error);
            }
            catch(FileNotFoundException)
            {
                shell.ShowMessage("File not found!", "The specified file could not be found!", MessageType.Error);
            }
            catch (Exception)
            {
                shell.ShowMessage("Error!", "Could not open file.", MessageType.Error);
            }
        }

        private void Shell_NewFileClicked(object sender)
        {
            throw new NotImplementedException();
        }
    }
}
