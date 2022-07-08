using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyramidCard
{
    enum Suit
    {
        club,
        diamond,
        spade,
        heart
    }
    class Card  //卡牌类
    {
        
        private Suit suit;  //花色
        private int value;  //点数
        public Card(Suit s, int v)
        {
            value = v;
            suit = s;
        }

        public Card()
        {    
        }
        public int getValue() { return this.value; }
        public string getSuit() { return suit.ToString(); }

        public bool Isequal(object o) 
        {//判断两张牌是否是同一张牌
            if (this == o) return true;
            if (!(o is Card)) return false;
            else {
                Card c = (Card)o;
                return (this.getSuit() == c.getSuit() && this.getValue() == c.getValue());
            }
        }
        //public int getHashCode();
        public String toString()
        {//将卡片转化为字符串，将来用于在文件夹中找到相应的图片；如club 13将转化为字符串 cK;
            if (value > 0 && value < 14)//1~13
            {
                return getSuit().ToString() + getValue().ToString();
            }
            else
            {
                throw new Exception("查无此卡！");
            }
        }
    }
}
