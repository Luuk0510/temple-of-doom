using CODE_TempleOfDoom_DownloadableContent;

namespace TempleOfDoom_Game.Model.Adaptor;
public class DummyField : IField
{
    public bool CanEnter => true;
    public IPlacable Item { get; set; } = null!;

    public IField GetNeighbour(int direction) => this;
}
