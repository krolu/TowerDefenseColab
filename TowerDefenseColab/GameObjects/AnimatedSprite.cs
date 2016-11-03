using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefenseColab.GameMechanisms;

namespace TowerDefenseColab.GameObjects
{
    public class AnimatedSprite
    {
        public List<Animation> Animations { get; set; }
        public Animation CurrentAnimation { get; set; }
        
        private Image _image;
        private Size _frameSize;
        private Point _location;

        public AnimatedSprite(Image image, Size frameSize)
        {
            _image = image;
            _frameSize = frameSize;
        }
        
        public void Update(TimeSpan timeDelta, PointF location)
        {
            CurrentAnimation.Update(timeDelta);
            _location = new Point((int)(location.X - _frameSize.Width / 2f), (int)(location.Y - _frameSize.Height / 2f));
        }

        public void Render(Graphics g)
        {
            Rectangle sourceRectangle = new Rectangle(
                CurrentAnimation.CurrentFrame * _frameSize.Width,
                0,
                _frameSize.Width,
                _frameSize.Height);
            Rectangle destRectangle = new Rectangle(
                _location, 
                _frameSize);
            
            g.DrawImage(_image, destRectangle, sourceRectangle, GraphicsUnit.Pixel);
        }
    }
}
