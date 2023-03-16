using Kuhpik;
using Supyrb;
using System.Linq;
using UnityEngine;

public class BusinesLevelUp : GameSystem
{
    private ChangeMoneySignal changeMoneySignal = Signals.Get<ChangeMoneySignal>();
    private UpdateUIForOpenBusinesSignal updateUIForOpenBusinesSignal = Signals.Get<UpdateUIForOpenBusinesSignal>();

    public override void OnInit()
    {
        base.OnInit();

        Signals.Get<BusinesLevelUpSignal>().AddListener(OnLevelUp);
    }

    private void OnLevelUp(BusinesController busines)
    {
        var levelUpPrice = config.BaseBusinesPriceDatas.FirstOrDefault(x => x.BusinesId == busines.BusinesId).Price * player.BusinesLevelData[busines.BusinesId];

        if (player.PlayerBalance >= levelUpPrice)
        {
            changeMoneySignal.Dispatch(levelUpPrice);

            player.BusinesLevelData[busines.BusinesId]++;

            updateUIForOpenBusinesSignal.Dispatch(busines);
        }
    }
}