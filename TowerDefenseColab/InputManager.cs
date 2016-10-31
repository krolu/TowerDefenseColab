using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TowerDefenseColab
{
    public class InputManager
    {
        private Dictionary<Keys, bool> _keyStates = new Dictionary<Keys, bool>();
        public delegate void OnClickHandler(MouseEventArgs e);
        public event OnClickHandler OnClick;

        public Point GetMousePosition()
        {
            return Cursor.Position;
        }

        public void KeyPressed(Keys key)
        {
            _keyStates[key] = true;
        }

        public void KeyReleased(Keys key)
        {
            _keyStates[key] = false;
        }

        public bool GetKeyState(Keys key)
        {
            bool downState;
            _keyStates.TryGetValue(key, out downState);
            return downState;
        }

        public void MouseClicked(MouseEventArgs e)
        {
            if (OnClick != null)
            {
                OnClick.Invoke(e);
            }
        }
    }
}
