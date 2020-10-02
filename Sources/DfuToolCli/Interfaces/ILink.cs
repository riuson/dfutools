﻿namespace DfuToolCli.Interfaces {
    public interface ILink {
        bool IsAcceptable(IVerbOptions options);
        IVerbProcessor CreateProcessor();
    }
}
