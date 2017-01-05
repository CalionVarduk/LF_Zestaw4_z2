using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF_Zestaw4_z2.DicePokerGame
{
    public enum GameState
    {
        SettingUp,
        Phase1Player1,
        Phase1Player2,
        Phase2Player1,
        Phase2Player2,
        Phase3Player1,
        Phase3Player2,
        RoundEnded,
        Finished,
        ShowingWinner,
        FinallyDone
    }
}
