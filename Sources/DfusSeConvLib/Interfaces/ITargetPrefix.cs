namespace DfuSeConvLib.Interfaces {
    /// <summary>
    /// The target prefix record is used to describe the associated image. The Target Prefix buffer is
    /// represented in Big Endian order.
    /// </summary>
    public interface ITargetPrefix {
        /// <summary>
        /// The Signature field, 6-byte coded, fixed to “Target”.
        /// </summary>
        string Signature { get; set; }

        /// <summary>
        /// The AlternateSetting field gives the device alternate setting for which the associated
        /// image can be used.
        /// </summary>
        int AlternateSetting { get; set; }

        /// <summary>
        /// The TargetNamed field is a boolean value which indicates if the target is named or
        /// not.
        /// </summary>
        bool TargetNamed { get; set; }

        /// <summary>
        /// The TargetName field gives the target name.
        /// </summary>
        string TargetName { get; set; }

        ///// <summary>
        ///// The TargetSize field gives the whole length of the associated image excluding the
        ///// Target prefix.
        ///// </summary>
        //public uint TargetSize { get; set; } = 0;

        ///// <summary>
        ///// The NbElements field gives the number of elements in the associated image.
        ///// </summary>
        //public uint NbElements { get; set; } = 0;
    }
}
