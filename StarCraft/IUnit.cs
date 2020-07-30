using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCraft
{
    interface IUnit
    {
        void SetDestination();
        void Move();
        void update();
        void ImageUpdate();
    }
}
