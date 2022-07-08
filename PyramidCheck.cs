using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PyramidCard
{ enum GameState
    { before,
        ing,
        over
    }
    class PyramidCheck<C>
    {
        public void checkParams(List<C> deck, List<C> CorrectDeck, int numOfRows, int numOfPile)    //	检验参数是否正确
        {
            if (numOfRows < 1 || numOfPile < 0 || deck == null)
                throw new Exception("初始化参数错误");

            foreach (C card in deck) {
                if (card == null) {
                    throw new Exception("初始化的牌堆中含有空卡牌");
                }
            }

            if (deck.Count() != CorrectDeck.Count()) throw new Exception("初始化的牌堆大小不正确");

            if (compareLists(deck, CorrectDeck)) throw new Exception("初始化的牌堆中含有重复的卡牌");

        }
        public bool checkNull(C card)    //检验卡牌是否是空卡牌
        {
            return card == null;
        }
        public void checkbeforegame(GameState gs)          //检验是否处于游戏开始前状态
        {
            if (gs == GameState.before) throw new Exception("游戏当前仍未开始！");
        }
        public void checkGameOver(GameState gs, String arg)         //检验是否已经游戏结束
        {
            if (gs == GameState.over) throw new Exception("游戏已经结束！");
        }
        public static bool compareLists<T>(List<T> aListA, List<T> aListB)
        {
            if (aListA == null || aListB == null || aListA.Count != aListB.Count)
                return false;
            if (aListA.Count == 0)
                return true;
            Dictionary<T, int> lookUp = new Dictionary<T, int>();
            // create index for the first list
            for (int i = 0; i < aListA.Count; i++)
            {
                int count = 0;
                if (!lookUp.TryGetValue(aListA[i], out count))
                {
                    lookUp.Add(aListA[i], 1);
                    continue;
                }
                lookUp[aListA[i]] = count + 1;
            }
            for (int i = 0; i < aListB.Count; i++)
            {
                int count = 0;
                if (!lookUp.TryGetValue(aListB[i], out count))
                {
                    // early exit as the current value in B doesn't exist in the lookUp (and not in ListA)
                    return false;
                }
                count--;
                if (count <= 0)
                    lookUp.Remove(aListB[i]);
                else
                    lookUp[aListB[i]] = count;
            }
            // if there are remaining elements in the lookUp, that means ListA contains elements that do not exist in ListB
            return lookUp.Count == 0;
        }
    }
}
