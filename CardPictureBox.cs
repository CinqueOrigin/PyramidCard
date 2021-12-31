using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace PyramidCard
{
    class CardPictureBox:PictureBox
    {
        private int rowIndex;
        private int colIndex;

        public CardPictureBox(int row, int col) :base(){
            rowIndex = row;
            colIndex = col;
        }
        public int getRowIndex()
        {
            return rowIndex;
        }
        public int getColIndex() { return colIndex; }
    }
}
