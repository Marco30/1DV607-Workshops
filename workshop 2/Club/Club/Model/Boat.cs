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

        public Boat()// constructor
        {
            this._length = 0;
            this._type = 0;
            this._ownedBy = 0;
        }

        public int Type// funktion som används för att till dela typ och hämta ut typ
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

   
        public string BoatToString()//funktion omvandlar this_type till text och skapar en string med all båt information
        {
            string returnString;
            string type = "";

            if (this._type == 1)
            {
                type = "Motorbåt";
            }

            if (this._type == 2)
            {
                type = "Segelbåt";
            }

            if (this._type == 3)
            {
                type = "Roddbåt";
            }

            if (this._type == 4)
            {
                type = "Kanot";
            }

            if (this._type == 5)
            {
                type = "Övrigt";
            }

            returnString = String.Format("\nBåttyp:\t\t{0}\nLängd:\t\t{1}", type, _length);

            return returnString;
        }

        public string BoatToStringFull()//funktion omvandlar this_type till text och skapar en string med all båt information
        {
            string returnString;
            string type = "";

            if (this._type == 1)
            {
                type = "Motorbåt";
            }

            if (this._type == 2)
            {
                type = "Segelbåt";
            }

            if (this._type == 3)
            {
                type = "Roddbåt";
            }

            if (this._type == 4)
            {
                type = "Kanot";
            }

            if (this._type == 5)
            {
                type = "Övrigt";
            }

            returnString = String.Format("\nBåttyp:\t\t{0}\nLängd:\t\t{1}\nÄgare:\t\t{2}", type, _length, _ownedBy);

            return returnString;
        }


    }
}
