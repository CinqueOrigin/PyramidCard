using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace PyramidCard
{
    class PyramidChessboard
    {
        protected GameState gs;
        private List<Card> heap;    //非金字塔中的牌
        private List<Card> heapRight;
        private Card[] pileCards;   //需要显示的两张牌
        private int numOfPile;      //等于2
        protected PyramidCheck<Card> utils;
        private static int score = 0;
        private Pyramid pyramid;
        private int shuffleCount = 0;

        public PyramidChessboard() {
            gs = GameState.before;
            utils = new PyramidCheck<Card>();
            numOfPile = -1;
        }

        public int getShuffleCount() { return this.shuffleCount; }
        public void addShuffleCount() { this.shuffleCount++; }
        public void startGame(List<Card> deck, bool shuffle, int numOfRows, int numOfPile) {
            this.utils.checkParams(deck, getCorrectCards(), numOfRows, numOfPile);
            List<Card> list1 = getInitDeck(deck, shuffle);

            this.gs = GameState.ing;
            this.pyramid = new Pyramid(numOfRows);
            this.numOfPile = numOfPile;
            this.pileCards = new Card[this.numOfPile];
            this.heap = this.pyramid.initPyramidWithCards(list1);
            this.heapRight = new List<Card>();

                Card card = this.heap.First();
                this.heap.RemoveAt(0);
                this.pileCards[0] = card;
                this.pileCards[1] = null;
        }
        protected List<Card> getInitDeck(List<Card> deck, bool shuffle)
        {
            // 将deck复制一份
            List<Card> copyDeck = new List<Card>();
            foreach(Card card in deck) { copyDeck.Add(card); }


            // 选择是否洗牌
            if (shuffle)  copyDeck = getRandomDeck(copyDeck);


            return copyDeck;
        }
        private List<Card> getRandomDeck(List<Card> myList)
        {
            Random ran = new Random();
            List<Card> newList = new List<Card>();
            int index = 0;
            Card card = new Card();
            for (int i = 0; i < myList.Count; i++)
            {

                index = ran.Next(0, myList.Count - 1);
                if (index != i)
                {
                    card = myList[i];
                    myList[i] = myList[index];
                    myList[index] = card;
                }
            }
            return myList;
        }
       public void hideTwoCards(int row1, int col1, int row2, int col2) {
            if (!(pyramid.isTwoCardsTop(row1, col1, row2, col2))) throw new Exception("这两张牌不在顶上！");
            if (!(pyramid.isCardsSumValueK(row1, col1, row2, col2))) throw new Exception("这两张牌的点数和不是K");
            else
            {
                pyramid.hideTwoCards(row1, col1, row2, col2);
                addScoresSupper();
            }
        }
       public void hideOneCard(int row, int col) {
            if (!pyramid.isOneCardTop(row, col)) throw new Exception("这张牌不在顶上！");
            if (!pyramid.IsCardValueK(row, col)) throw new Exception("这张牌的点数不是K!!");
            else
            {
                pyramid.hideCard(row, col);
                addScoresCommon();
            }
        }
       public void hideTwoCardsWithPile(int IndexOfPile, int row, int col) {
            if (numOfPile == 0) throw new Exception("牌堆里没有牌了诶");
            if (!pyramid.isOneCardTop(row, col)) {
                throw new Exception("这张牌不在顶上！");
            }

            if (!(getCard(row, col).getValue() + this.getCardInPile(IndexOfPile).getValue() == 13)) throw new Exception("金字塔里的这张牌和牌堆的这张牌的点数之和不是13");
            else
            {
                changePile(IndexOfPile);
                pyramid.hideCard(row, col);
                addScoresSupper();
            }
        }
       public void hideOneCardInPile(int IndexOfPile) {
            if (numOfPile == 0) throw new Exception("牌堆里没有牌了诶");
            utils.checkNull(getCardInPile(IndexOfPile));
            if (!(getCardInPile(IndexOfPile).getValue() == 13)) throw new Exception("牌堆中的这张牌的点数不为K");
            else
            {
                changePile(IndexOfPile);
                addScoresCommon();
            }
        }
        public void hideTwoCardsInPile(int index1,int index2) {
            if (numOfPile == 0) throw new Exception("牌堆里没有牌了诶");
            utils.checkNull(getCardInPile(index1));
            utils.checkNull(getCardInPile(index2));
            if (!(getCardInPile(index1).getValue() + getCardInPile(index2).getValue() == 13)) throw new Exception("牌堆中的这两张牌的点数和不为K");
            else
            {
                changePile(index1);
                changePile(index2);
                addScoresSupper();
            }
        }
        public void changePile(int IndexOfPile) {   //消去牌堆中的牌时，从牌堆中补充牌
            if (IndexOfPile == 0)
            {
                if (this.heap.Count != 0)
                {
                    pileCards[0] = heap[0];
                    heap.RemoveAt(0);
                }
                else pileCards[0] = null;
            }
            if (IndexOfPile == 1)
                if (this.heapRight.Count != 0)
                {
                    pileCards[1] = heapRight.Last();
                    heapRight.RemoveAt(heapRight.Count - 1);
                }
                else pileCards[1] = null;
        }
        public void shufflePileCards()
        {
            if (heapRight.Count > 0||getCardInPile(1) != null) { 
            this.heap = this.heapRight;
                if (getCardInPile(1) != null) this.heap.Add(getCardInPile(1));
            List<Card> list = new List<Card>();
            this.heapRight = list;
                pileCards[0] = heap.First();
                heap.RemoveAt(0);
                pileCards[1] = null;
        }
            else throw new Exception("牌堆已经用尽");
        }

        public void transCardsInPile() {        //对应翻牌的按钮
            if(getCardInPile(1) != null)
            this.heapRight.Add(getCardInPile(1));
            if(getCardInPile(0) != null)
            this.pileCards[1] = this.pileCards[0];
            if (heap.Count != 0)
            {
                pileCards[0] = heap[0];
                heap.RemoveAt(0);
            }
            else pileCards[0] = null;
        }
       public static List<Card> getCorrectCards()
        {
            List<Card> cards = new List<Card>();

            foreach (Suit suit in Enum.GetValues(typeof(Suit))) {
                for (int i = 1; i <= 13; i++)
                {
                    cards.Add(new Card(suit, i));
                }
            }
            return cards;
        }
       public int getNumOfRows() { return pyramid.getRowNum(); }
       public int getNumOfPile() { return numOfPile; }
      public  int getRowWidth(int row) { return pyramid.getWidth(row); }
      public  Card getCard(int row, int col) { return pyramid.getCard(row, col); }
       public Card getCardInPile(int IndexOfPile) { return pileCards[IndexOfPile]; }
       public List<Card> getCardsInPile() { return pileCards.ToList<Card>(); }
       public int getScore() { return score; }
        public void addScoresCommon() { score += 10; }
        public void addScoresSupper() { score += 20; }
        public int heapCount() { return this.heap.Count; }
       public bool isGameOver() {
            bool isPyramidEmpty = true;
            List<int> TopCards = new List<int>();
            for (int i = 0; i < getNumOfRows(); i++) {  //金字塔没有牌时，判定游戏已经胜利结束
                for (int j = 0; j < getRowWidth(i); j++)
                {
                    Card card = pyramid.getCard(i, j);
                    if (card != null && pyramid.isOneCardTop(i, j))
                    {
                        isPyramidEmpty = false;
                        TopCards.Add(card.getValue());
                    }
                }
            }

            if (isPyramidEmpty) return true;

            if (this.getShuffleCount() < 2) return false; 

            if (getNumOfPile() > 0) {   //将牌堆中的牌加入Topcards

                foreach (Card c in getCardsInPile())
                {
                    if (c != null)
                        TopCards.Add(c.getValue());
                }
            }

            bool CanMove = false;

            foreach (int value in TopCards)
            {
                if (TopCards.Contains(13 - value)||value == 13) { CanMove = true; }
            }

            if (CanMove||heap.Count>0) return false;  //如果还有移动的地方，判定游戏可以继续进行

            return !(heap.Count() > 0 && getNumOfRows() > 0&&heapRight.Count>0&&shuffleCount<2);    // 如果上面的条件都不满足，那么如果牌堆的牌或者金字塔中的行数有一项为0则结束
        }
        public List<(int,int)> getTopCards()
        {
            List<(int,int)> TopCards = new List<(int, int)>();
            for (int i = 0; i < getNumOfRows(); i++)
            {  //金字塔没有牌时，判定游戏已经胜利结束
                for (int j = 0; j < getRowWidth(i); j++)
                {
                    Card card = pyramid.getCard(i, j);
                    if (card != null && pyramid.isOneCardTop(i, j))
                    {
                        (int, int) point = (i, j);
                        TopCards.Add(point);
                    }
                }
            }

            for (int i = 0; i < numOfPile; i++)
            {
                (int, int) point = (-1, i);
                if(getCardInPile(i) != null)
                TopCards.Add(point);
            }
            return TopCards;
        }
        public void changeToGameOver() {
            gs = GameState.over;
        }
    }
}
