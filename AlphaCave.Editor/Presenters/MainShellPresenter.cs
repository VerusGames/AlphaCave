using AlphaCave.Editor.Forms;
using System;
using System.Collections.Generic;
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
        }
    }
}
