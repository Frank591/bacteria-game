
using System;
using System.Collections.Generic;
using System.Linq;


namespace BacteriaSurvive.BL
{
    public class GameCenter
    {
        IList<BacteriaGameStatistic> _players = new List<BacteriaGameStatistic>();
        private Random _random;

       


        public GameCenter(Random random)
        {
            _random = random;
        }

        

        public void AddPlayer(Bacteria bacteria)
        {
            _players.Add(new BacteriaGameStatistic(bacteria));
        }



        public Bacteria Play()
        {

            for (int i=0;  i< _players.Count;i++)
            {
                for (int j = i + 1; j < _players.Count; j++)
                {

                    BacteriaGameStatistic player1Statistic= _players[i];
                    BacteriaGameStatistic player2Statistic = _players[j];

                    Bacteria player1 = player1Statistic.Player;
                    Bacteria player2 = player2Statistic.Player;

                    RoundResults roundResult = PlayRound(player1, player2);

                    switch (roundResult)
                    {
                        case RoundResults.Draw:
                            break;
                        case RoundResults.Player1Win:
                            player1Statistic.GamesStatistic++;
                            player2Statistic.GamesStatistic--;
                            break;
                        case RoundResults.Player2Win:
                            player1Statistic.GamesStatistic--;
                            player2Statistic.GamesStatistic++;
                            break;
                    }
                }
            }


            int maxStatisitc = _players.Max(stat=>stat.GamesStatistic);
            IList<BacteriaGameStatistic> maxStatisticElements =
                _players.Where(stat => stat.GamesStatistic == maxStatisitc).ToList();
            int winnerIndex = _random.Next(0, maxStatisticElements.Count - 1);
            return maxStatisticElements[winnerIndex].Player;
        }


        
        


        private RoundResults PlayRound( Bacteria player1, Bacteria player2)
        {
            EBacteriaAttributes player1ThrowResult = MakeThrow(player1);
            EBacteriaAttributes player2ThrowResult = MakeThrow(player2);


            //правила. потом переписать нормально на матрицу
            if (player1ThrowResult == EBacteriaAttributes.Paper)
            {

                switch (player2ThrowResult)
                {
                        case EBacteriaAttributes.Paper:
                            return RoundResults.Draw;
                        case EBacteriaAttributes.Scissors:
                            return RoundResults.Player2Win;
                        case EBacteriaAttributes.Stone:
                            return RoundResults.Player1Win;

                }
            }
            if (player1ThrowResult == EBacteriaAttributes.Stone)
            {

                switch (player2ThrowResult)
                {
                    case EBacteriaAttributes.Paper:
                        return RoundResults.Player2Win;
                    case EBacteriaAttributes.Scissors:
                        return RoundResults.Player1Win;
                    case EBacteriaAttributes.Stone:
                        return RoundResults.Draw;
                }
            }
            if (player1ThrowResult == EBacteriaAttributes.Scissors)
            {

                switch (player2ThrowResult)
                {
                    case EBacteriaAttributes.Paper:
                        return RoundResults.Player1Win;
                    case EBacteriaAttributes.Scissors:
                        return RoundResults.Draw;
                    case EBacteriaAttributes.Stone:
                        return RoundResults.Player2Win;
                }
            }

            throw new ApplicationException("C3B10734-6536-4FD2-BE6E-4B57D9CBC355: неизвестная комбинация ");
        }

        
        private EBacteriaAttributes MakeThrow(Bacteria player)
        {
            
            IList<BacteriaAttribute> attributes=new List<BacteriaAttribute>();
            attributes.Add(new BacteriaAttribute(EBacteriaAttributes.Paper,  player.PaperProbability));
            attributes.Add(new BacteriaAttribute(EBacteriaAttributes.Stone, player.StoneProbability));
            attributes.Add(new BacteriaAttribute(EBacteriaAttributes.Scissors, player.ScissorsProbability));



            KeyValuePair<EBacteriaAttributes,int> maxTrowResult= new KeyValuePair< EBacteriaAttributes,int>( EBacteriaAttributes.Paper, 0);

            foreach (BacteriaAttribute bacteriaAttribute in attributes)
            {

                int random = _random.Next(0, 100);
                int throwResult = bacteriaAttribute.Value*random;
                if (maxTrowResult.Value < throwResult)
                {
                    maxTrowResult = new KeyValuePair<EBacteriaAttributes, int>(bacteriaAttribute.AttributeType, throwResult);
                }
            }

            return maxTrowResult.Key;
        }


    }









    internal enum RoundResults
    {
        Player1Win, Player2Win, Draw
    }


    public class BacteriaGameStatistic 
    {
        private readonly Bacteria _player;

        public BacteriaGameStatistic(Bacteria player)
        {
            _player = player;
            GamesStatistic = 0;
        }

        public int GamesStatistic { get; set; }

        public Bacteria Player
        {
            get { return _player; }
        }
    }


   
}

