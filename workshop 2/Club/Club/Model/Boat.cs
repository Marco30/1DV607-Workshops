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
        private int _ownedBy;

        public Boat(int type, int length, int ownedBy)
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

        public int OwnedBy
        {
            get { return _ownedBy; }
            set
            {
                _ownedBy = value;
            }
        }

        public string GetBoatType()
        {
            if (this._type == 1)
            {
                return "Motorbåt";
            }
            
            if (this._type == 2)
            {
                return "Segelbåt";
            }
            
            if (this._type == 3)
            {
                return "Roddbåt";
            }
            
            if (this._type == 4)
            {
                return "Kanot";
            }
            
            return "Övrigt";
        }

        public string PrintFullWithNumberList()
        {
            return String.Format("\n1. Ägare:\t{0}\n2. Typ:\t\t{1}\n3. Längd:\t{2}", _ownedBy, GetBoatType(), _length);
        }

    


    }
}
