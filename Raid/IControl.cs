using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raid
{
    public interface IControl
    {
        void Move();
        void Action(GameTime game);
    }
}
