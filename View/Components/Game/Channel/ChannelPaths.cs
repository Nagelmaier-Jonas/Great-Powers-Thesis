using Model.Entities.Regions;

namespace View.Components.Game.Channel;

public class ChannelPaths : Dictionary<EChannel, string>{
    public ChannelPaths(){
        this[EChannel.PanamaCanal] = "M108 558 111 560 94 579 91 577 108 558";
        this[EChannel.SuezCanal] = "M742 568 747 568 754 609 749 609 742 568";
    }
}