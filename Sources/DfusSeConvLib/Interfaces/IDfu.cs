namespace DfuSeConvLib.Interfaces {
    public interface IDfu {
        IDfuPrefix Prefix { get; set; }
        IDfuImages Images { get; set; }
        IDfuSuffix Suffix { get; set; }
    }
}
