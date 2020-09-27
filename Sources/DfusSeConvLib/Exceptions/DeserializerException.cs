using System;

namespace DfuSeConvLib.Exceptions {
    public class DeserializerException : Exception {
        public delegate DeserializerException Factory(string message, long streamPosition);

        public DeserializerException(string message, long streamPosition) :
            base($"An error occured while deserializing stream at position {streamPosition}:\n{message}") { }
    }
}
