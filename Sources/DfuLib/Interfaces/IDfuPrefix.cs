namespace DfuLib.Interfaces {
    /// <summary>
    /// The DFU prefix placed as a header file is the first part read by the software application, used
    /// to retrieve the file context, and enable valid DFU files to be recognized.The Prefix buffer is
    /// represented in Big Endian order.
    /// </summary>
    public interface IDfuPrefix {
        /// <summary>
        /// The Signature field, five-byte coded, presents the file identifier that enables valid
        /// DFU files to be recognized, and incompatible changes detected.This identifier should
        /// be updated when major changes are made to the file format.This field is set to
        /// “DfuSe”.
        /// </summary>
        string Signature { get; set; }

        /// <summary>
        /// The Version field, one-byte coded, presents the DFU format revision, The value will be
        /// incremented if extra fields are added to one of the three sections.Software exploring
        /// the file can either treat the file depending on its specified revision, or just test for valid
        /// value.
        /// </summary>
        int Version { get; set; }

        /// <summary>
        /// The DfuImageSize field, four-byte coded, presents the total DFU file length in bytes.
        /// </summary>
        uint DfuImageSize { get; set; }

        /// <summary>
        /// The Targets field, one-byte coded, presents the number of DFU image stored in the file.
        /// </summary>
        int Targets { get; set; }
    }
}
