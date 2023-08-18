    /**
     * 行動状態
     */
public enum EAct
{
    WaitingKeyInput, //キー入力待ち

    //攻撃等
    ActBegin, //開始
    Acting,      //実行中
    ActEnd,   //終了

    //移動
    MoveBegin,//開始  
    Moving,     //移動中
    MoveEnd,  //完了

    TurnEnd,  //行動の終了
};
