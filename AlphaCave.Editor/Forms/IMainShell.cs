using AlphaCave.Editor.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlphaCave.Editor.Delegates;

namespace AlphaCave.Editor.Forms
{
    public interface IMainShell
    {
        ObservableCollection<IEditorObject> OpenObjects { get; }

        event ObjectSelectHandler OnObjectSelected;

        event SenderHandler NewFileClicked;
        event SenderHandler OpenFileClicked;
        event ObjectSelectHandler SaveFileClicked;
        event ObjectSelectHandler SaveFileAsClicked;
        event SenderHandler OpenFolderClicked;
        event SenderHandler ExitClicked;
        event ObjectSelectHandler ExportClick;

        void ShowEditor(IEditorObject editorObject);

        string[] ShowFileOpen();
        string ShowFolderOpen();
        string ShowFileSave(string filename = null);

        void ShowMessage(string title, string message, MessageType type);

        void Close();

    }

    public enum MessageType
    {
        Info,
        Warning,
        Error
    }
}
