using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Model
{
    class Boat
    {
        private int _type;
        private int _length;
        private Member _ownedBy;

        public Boat(int type, int length, Member ownedBy)
        {
            this._length = length;
            this._type = type;
            this._ownedBy = ownedBy;
        }

        public int Type
        {
            get { return _type; }
            set
            {
                _type = value;
            }
        }

        public int Length
        {
            get { return _length; }
            set
            {
                _length = value;
            }
        }

        public Member OwnedBy
        {
            get { return _ownedBy; }
            set
            {
                _ownedBy = value;
            }
        }
    }
}
